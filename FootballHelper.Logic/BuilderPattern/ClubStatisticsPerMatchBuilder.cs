using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic.BuilderPattern
{
    public class ClubStatisticsPerMatchBuilder
    {
        private int id = 1;
        private Club club = new ClubBuilder().Build();
        private Match match = new MatchBuilder().Build();
        private int possession = 50;
        private int corners = 5;
        private int offsides = 2;

        private static int count = 0;
        private bool idWasSet = false;

        public ClubStatisticsPerMatchBuilder WithID(int id)
        {
            this.id = id;
            this.idWasSet = true;
            return this;
        }

        public ClubStatisticsPerMatchBuilder WithClub(Club club)
        {
            this.club = club;
            return this;
        }

        public ClubStatisticsPerMatchBuilder WithMatch(Match match)
        {
            this.match = match;
            return this;
        }

        public ClubStatisticsPerMatchBuilder WithPossession(int possession)
        {
            this.possession = possession;
            return this;
        }

        public ClubStatisticsPerMatchBuilder WithCorners(int corners)
        {
            this.corners = corners;
            return this;
        }

        public ClubStatisticsPerMatchBuilder WithOffsides(int offsides)
        {
            this.offsides = offsides;
            return this;
        }

        public ClubStatisticsPerMatch Build()
        {
            if (!idWasSet)
            {
                id = ++count;
            }
            return new ClubStatisticsPerMatch(id, club, match, possession, corners, offsides);
        }
    }
}
