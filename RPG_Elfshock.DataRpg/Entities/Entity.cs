namespace Entities.Entities
{
    public abstract class Entity
    {
        private IEnumerable<char> symbols;

        private int strength;

        private int agility;

        private int intelligence;

        private int range;

        private int health;

        private int mana;

        private int damage;

        private char symbol;

        private int[] pos;

        public Entity()
        {
            symbols = everyEntitySymbols;
            this.MovementChars = new char[] { 'w', 's', 'd', 'a', 'e', 'x', 'q', 'z' };
        }

        public IEnumerable<char> MovementChars { get; set; }

        public int[] Pos
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }

        public int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                if (value >= 0)
                {
                    strength = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Strength));
                }
            }
        }

        public int Agility
        {
            get
            {
                return agility;
            }
            set
            {
                if (value >= 0)
                {
                    agility = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Agility));
                }
            }
        }

        public int Intelligence
        {
            get
            {
                return intelligence;
            }
            set
            {
                if (value >= 0)
                {
                    intelligence = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Intelligence));
                }
            }
        }

        public int Range
        {
            get
            {
                return range;
            }
            set
            {
                if (value >= 0)
                {
                    range = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Range));
                }
            }
        }

        public char Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                if (symbols.Contains(value))
                {
                    symbol = value;
                }
                else
                {
                    throw new InvalidOperationException(nameof(Symbol));
                }
            }
        }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value > 0)
                {
                    health = value;
                } 
                else if (value <= 0)
                {
                    health = 0;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Health));
                }
            }
        }

        public int Mana
        {
            get
            {
                return mana;
            }
            set
            {
                if (value >= 0)
                {
                    mana = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Mana));
                }
            }
        }

        public int Damage
        {
            get
            {
                return damage;
            }
            set
            {
                if (value >= 0)
                {
                    damage = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Damage));
                }
            }
        }

        public void Setup()
        {
            Health = Strength * 5;
            Mana = Intelligence * 3;
            Damage = Agility * 2;
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
        }
    }
}
