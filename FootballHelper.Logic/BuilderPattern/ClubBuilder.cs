using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic.BuilderPattern
{
    public class ClubBuilder
    {
        private int id = 1;
        private string name = "Клуб";
        private List<Player> players = new List<Player> { };

        private static int count = 0;
        private bool idWasSet = false;

        public ClubBuilder WithID(int id)
        {
            this.id = id;
            this.idWasSet = true;
            return this;
        }

        public ClubBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public ClubBuilder WithPlayers(List<Player> players)
        {
            this.players = players;
            return this;
        }

        public Club Build()
        {
            if (!idWasSet)
            {
                id = ++count;
            }
            return new Club(id, name, players);
        }
    }
}
