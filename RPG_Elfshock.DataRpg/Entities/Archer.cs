namespace Entities.Entities
{
    public class Archer : Entity
    {
        public Archer() : base()
        {
            Strength = 2;
            Agility = 4;
            Intelligence = 0;
            Range = 2;
            Symbol = '#';
            Pos = new int[] { 1, 1 };
        }

    }
}
