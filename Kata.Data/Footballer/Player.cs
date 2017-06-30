using System.Collections.Generic;

namespace Kata.Data.Footballer
{
    public class Player : Footballer
    {
        public SortedList<int, Position> Positions { get; }

        private bool _retired;
        public bool Retired
        {
            get
            {
                return _retired;
            }
            set
            {
                if (!Retired) _retired = value;
            }
        }

        public override Team CurrentTeam => Retired ? null : base.CurrentTeam;

        public Player(string firstName, string lastName) : base(firstName, lastName)
        {
            Positions = new SortedList<int, Position>();
        }

        public void Retire()
        {
            Retired = true;
        }
    }
}