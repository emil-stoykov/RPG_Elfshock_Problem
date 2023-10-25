namespace Entities.Entities
{
    public class Enemy : Entity
    {
        private Random rand;

        public Enemy() : base()
        {
            rand = new Random();
            Strength = rand.Next(1, 3);
            Agility = rand.Next(1, 3);
            Intelligence = rand.Next(1, 3);
            Range = 1;
            Symbol = '!';
            Setup();
        }

    }
}
