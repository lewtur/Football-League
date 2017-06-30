using Kata.Data.Exceptions;

namespace Kata.Data.Footballer
{
    public class Manager : Footballer
    {
        public ManagerStyle Style;

        public Manager(string firstName, string lastName): base(firstName, lastName)
        {            
        }

        public Manager(Player player) : base(player.FirstName, player.LastName)
        {
            if (!player.Retired) throw new NonRetiredPlayerBecomingManagerException();
            Teams = player.Teams;
        }
    }
}