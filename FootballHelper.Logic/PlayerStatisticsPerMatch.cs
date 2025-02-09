using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic
{
    public class PlayerStatisticsPerMatch
    {
        public int ID { get; private set; }
        public Player Player { get; private set; }
        public Match Match { get; private set; }
        public int Goals { get; private set; }
        public int Assists { get; private set; }
        public int Shoots { get; private set; }
        public int Tackling { get; private set; }
        public bool YellowCart { get; private set; }
        public bool RedCart { get; private set; }

        public PlayerStatisticsPerMatch(int iD, Player player, Match match, int goals, int assists, int shoots, int tackling, bool yellowCart, bool redCart)
        {
            ID = iD;
            Player = player;
            Match = match;
            Goals = goals;
            Assists = assists;
            Shoots = shoots;
            Tackling = tackling;
            YellowCart = yellowCart;
            RedCart = redCart;
        }
    }
}
