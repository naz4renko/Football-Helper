using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic
{
    public class ClubStatisticsPerMatch
    {
        public int ID { get; private set; }
        public Club Club { get; private set; }
        public Match Match { get; private set; }
        public int Possession { get; private set; }
        public int Corners { get; private set; }
        public int Offsides { get; private set; }

        public ClubStatisticsPerMatch(int iD, Club club, Match match, int possession, int corners, int offsides)
        {
            ID = iD;
            Club = club;
            Match = match;
            Possession = possession;
            Corners = corners;
            Offsides = offsides;
        }
    }
}
