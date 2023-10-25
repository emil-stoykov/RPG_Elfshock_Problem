namespace Entities.Entities
{
    public class Warrior : Entity
    {
        public Warrior() : base()
        {
            Strength = 3;
            Agility = 3;
            Intelligence = 0;
            Range = 1;
            Symbol = '@';
            Pos = new int[] { 1, 1 };
        }
    }
}
