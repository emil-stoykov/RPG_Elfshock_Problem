using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Elfshock.DataRpg
{
    public class CharacterCustomizer
    {
        private int classPick;

        private int bonusStrength;

        private int bonusAgility;

        private int bonusIntelligence;

        private int extraPoints;

        public int ExtraPoints
        {
            get
            {
                return extraPoints;
            }
            set
            {
                extraPoints = value;
            }
        }

        public int BonusStrength
        {
            get
            {
                return bonusStrength;
            }
            set
            {
                bonusStrength = value;
            }
        }

        public int BonusAgility
        {
            get
            {
                return bonusAgility;
            }
            set
            {
                bonusAgility = value;
            }
        }

        public int BonusIntelligence
        {
            get
            {
                return bonusIntelligence;
            }
            set
            {
                bonusIntelligence = value;
            }
        }

        public int ClassPick
        {
            get
            {
                return classPick;
            }
            set
            {
                if (value >= 1 && value <= 3)
                {
                    classPick = value;
                }
                else
                {
                    throw new InvalidOperationException("Pick a number between 1 and 3.");
                }
            }
        }

        public CharacterCustomizer()
        {
            this.ExtraPoints = 3;
            this.BonusStrength = 0;
            this.BonusIntelligence = 0;
            this.BonusAgility = 0;
        }

        public void AddExtraPoints()
        {
            int inputPoints = 0;
            string[] pointNames = new string[] { "Strength", "Agility", "Intelligence" };
            for (int i = 0; i < 3; i++)
            {
                if (this.ExtraPoints > 0)
                { 
                    Console.WriteLine("Remaining points: " + this.ExtraPoints);
                    Console.WriteLine($"Add to {pointNames[i]}:");

                    if (int.TryParse(Console.ReadLine(), out inputPoints))
                    {
                        if (inputPoints > this.ExtraPoints)
                        {
                            inputPoints = this.ExtraPoints;
                        }
                        this.ExtraPoints -= inputPoints;

                        switch (i)
                        {            
                            case 0: this.BonusStrength += inputPoints; break;
                            case 1: this.BonusAgility += inputPoints; break;
                            case 2: this.BonusIntelligence += inputPoints; break;
                        }
                    }
                    
                }
            }
        }

    }
}
