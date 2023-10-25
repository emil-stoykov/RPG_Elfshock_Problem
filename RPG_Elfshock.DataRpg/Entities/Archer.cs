using Entities.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Archer : Entity, IPlayerCharacter
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
