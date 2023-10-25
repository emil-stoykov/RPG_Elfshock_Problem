using Entities.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Warrior : Entity, IPlayerCharacter
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
