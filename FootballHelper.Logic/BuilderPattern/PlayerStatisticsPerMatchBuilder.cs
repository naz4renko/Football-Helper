using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic.BuilderPattern
{
    public class PlayerStatisticsPerMatchBuilder
    {
        private int id = 1;
        private Player player = new PlayerBuilder().Build();
        private Match match = new MatchBuilder().Build();
        private int goals = 0;
        private int assists = 0;
        private int shoots = 0;
        private int tackling = 0;
        private bool yellowCart = false;
        private bool redCart = false;

        private static int count = 0;
        private bool idWasSet = false;

        public PlayerStatisticsPerMatchBuilder WithID(int id)
        {
            this.id = id;
            this.idWasSet = true;
            return this;
        }

        public PlayerStatisticsPerMatchBuilder WithPlayer(Player player)
        {
            this.player = player;
            return this;
        }

        public PlayerStatisticsPerMatchBuilder WithMatch(Match match)
        {
            this.match = match;
            return this;
        }

        public PlayerStatisticsPerMatchBuilder WithGoals(int goals)
        {
            this.goals = goals;
            return this;
        }

        public PlayerStatisticsPerMatchBuilder WithAssists(int assists)
        {
            this.assists = assists;
            return this;
        }

        public PlayerStatisticsPerMatchBuilder WithShoots(int shoots)
        {
            this.shoots = shoots;
            return this;
        }

        public PlayerStatisticsPerMatchBuilder WithTackling(int tackling)
        {
            this.tackling = tackling;
            return this;
        }

        public PlayerStatisticsPerMatchBuilder WithYellowCart(bool yellowCart)
        {
            this.yellowCart = yellowCart;
            return this;
        }

        public PlayerStatisticsPerMatchBuilder WithRedCart(bool redCart)
        {
            this.redCart = redCart;
            return this;
        }

        public PlayerStatisticsPerMatch Build()
        {
            if (!idWasSet)
            {
                id = ++count;
            }
            return new PlayerStatisticsPerMatch(id, player, match, goals, assists, shoots, tackling, yellowCart, redCart);
        }
    }
}
