namespace Entities.Entities
{
    public class Mage : Entity
    {
        public Mage() : base()
        {
            Strength = 2;
            Agility = 1;
            Intelligence = 3;
            Range = 3;
            Symbol = '*';
            Pos = new int[] { 1, 1 };
        }
    }
}
