using Entities.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data;
using RPG_Elfshock.DataRpg;
using Models;
using RpgData.MatrixField;

namespace RPG_Elfshock
{
    public class Program
    {
        public static Screens screen = Screens.MainMenu;
        public static Entity player;

        static void Main(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureHostConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
                config.AddUserSecrets<Program>();
            });

            builder.ConfigureServices((hostContext, config) =>
            {
                var connectionString = hostContext.Configuration.GetConnectionString("RpgContext");
                config.AddDbContext<RpgDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            });

            var host = builder.Build();

            while (true)
            {
                switch (screen)
                {
                    case Screens.MainMenu:
                        screen = MainMenuScreen();
                        break;
                    case Screens.CharacterSelect:
                        screen = CharacterSelectScreen();

                        Console.WriteLine("Saving character...");
                        using (var dbContext = host.Services.GetRequiredService<RpgDbContext>())
                        {
                            dbContext.Database.EnsureCreated();

                            Character character = new Character();

                            character.ClassName = player.GetType().Name;
                            character.Symbol = player.Symbol;
                            character.Mana = player.Mana;
                            character.Strength = player.Strength;
                            character.Agility = player.Agility;
                            character.Intelligence = player.Intelligence;
                            character.Range = player.Range;
                            character.Health = player.Health;
                            character.Damage = player.Damage;

                            dbContext.Characters.Add(character);
                            dbContext.SaveChanges();
                        }
                        Console.WriteLine("Character saved!");

                        break;
                    case Screens.InGame:
                        screen = InGameScreen();
                        break;
                    case Screens.Exit:
                        screen = ExitScreen();
                        break;
                }
            }
        }

        public static Screens MainMenuScreen()
        {
            Console.WriteLine("\nWelcome!\nPress any key to play.");

            Console.ReadKey();

            return Screens.CharacterSelect;
        }

        public static Screens CharacterSelectScreen()
        {
            CharacterCustomizer customizer = new CharacterCustomizer();
            Console.WriteLine("Choose character type:\r\nOptions:\r\n1) Warrior\r\n2) Archer\r\n3) Mage\r\nYour pick: ");

            while (true)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out int pick))
                    {
                        customizer.ClassPick = pick;
                        break;
                    }
                } catch
                {
                    Console.WriteLine("Try again");
                }
            }

            switch(customizer.ClassPick)
            {
                case 1:
                    player = new Warrior();
                    break;
                case 2:
                    player = new Archer();
                    break;
                case 3:
                    player = new Mage();
                    break;
            }

            Console.WriteLine("Would you like to buff up your stats before starting?\t(Limit: 3 points total)\nResponse(Y/N)");
            char response = 'x';

            while (true)
            {
                if (char.TryParse(Console.ReadLine(), out response) && (char.ToLower(response) == 'y' || char.ToLower(response) == 'n'))
                {
                    break;
                } else
                {
                    Console.WriteLine("Write a valid input (Y/N).");
                }
            }

            if (response == 'y')
            {
                customizer.AddExtraPoints();
            }
            
            player.Strength += customizer.BonusStrength;
            player.Agility += customizer.BonusAgility;
            player.Intelligence += customizer.BonusIntelligence;
            player.Setup();

            return Screens.InGame;
        }

        public static Screens InGameScreen()
        {
            Matrix matrix = new Matrix();

            matrix.PlayerSymbol = player.Symbol;
            matrix.PlacePlayer(player.Pos);

            while (player.Health > 0)
            {
                matrix.GenerateEnemyPos();
                Console.WriteLine($"\nHealth: {player.Health}\tMana: {player.Mana}");

                Console.WriteLine(matrix.DrawField(player.Symbol));
                Console.WriteLine($"Choose action:\n1)Attack\n2)Move\n");
                try
                {
                    int action = int.Parse(Console.ReadLine());

                    if (action == 1)
                    {
                        IList<Enemy> enemiesInRange = matrix.EnemiesInMatrix.Where(e => matrix.EntityInRange(e.Pos, player.Range)).ToList();

                        if (enemiesInRange.Count == 0)
                        {
                            Console.WriteLine("No available targets in your range.");
                        } 
                        else
                        {
                            for (int i = 0; i < enemiesInRange.Count; i++)
                            {
                                Console.WriteLine($"{i}) target with remaining blood {enemiesInRange[i].Health}");
                            }

                            try
                            {
                                int attackInputIdx = int.Parse(Console.ReadLine());
                                if (attackInputIdx < 0 || attackInputIdx > enemiesInRange.Count)
                                {
                                    throw new ArgumentException();
                                }

                                Enemy chosenEnemy = enemiesInRange[attackInputIdx];
                                chosenEnemy.TakeDamage(player.Damage);

                                if (chosenEnemy.Health == 0)
                                {
                                    matrix.DeleteEntityFromMatrix(chosenEnemy);
                                }

                            } 
                            catch
                            {
                                Console.WriteLine("Invalid input.");
                            }
                        }
                    }
                    else if (action == 2)
                    {
                        try
                        {
                            Console.WriteLine("Choose a direction:\nw - up\ns - down\nd - right\na - left\ne - up & right\nx - down & right\nq - up & left\nz - down & left\n");
                            char dir = Char.ToLower(Console.ReadKey().KeyChar);
                            if (player.MovementChars.Contains(dir))
                            {
                                player.Pos = matrix.CalcPlayerPos(dir);
                            } else
                            {
                                Console.WriteLine("\nInvalid Input");
                            }
                        }
                        catch 
                        {
                            Console.WriteLine("Invalid Input");
                        }
                    }
                } 
                catch
                {
                    Console.WriteLine("Invalid input!");
                }

                foreach (var enemy in matrix.EnemiesInMatrix)
                {
                    if (matrix.EntityInRange(enemy.Pos, enemy.Range))
                    {
                        player.TakeDamage(enemy.Damage);
                        if (player.Health == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        enemy.Pos = matrix.CalcEnemiesPos(enemy.Pos);
                    }
                }
            }

            return Screens.Exit;
        }
        
        public static Screens ExitScreen()
        {
            Console.WriteLine("Player died!\nPlay again? (Y/N)");
            char response = Char.ToLower(Console.ReadKey().KeyChar);

            while (true)
            {
                try
                {
                    if (response == 'y')
                    {
                        break;
                    } 
                    else if (response == 'n')
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                } 
                catch
                {
                    Console.WriteLine("\nInvalid input.");
                }
            }

            return Screens.MainMenu;
        }
    }
}