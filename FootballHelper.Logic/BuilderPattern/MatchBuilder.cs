using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic.BuilderPattern
{
    public class MatchBuilder
    {
        private int id = 1;
        private DateOnly dateOfTheMatch = new DateOnly(2023, 1, 1);
        private Club homeClub = new ClubBuilder().Build();
        private Club awayClub = new ClubBuilder().Build();

        private static int count = 0;
        private bool idWasSet = false;

        public MatchBuilder WithID(int id)
        {
            this.id = id;
            this.idWasSet = true;
            return this;
        }

        public MatchBuilder WithDateOfTheMatch(DateOnly date)
        {
            this.dateOfTheMatch = date;
            return this;
        }

        public MatchBuilder WithHomeClub(Club club)
        {
            this.homeClub = club;
            return this;
        }

        public MatchBuilder WithAwayClub(Club club)
        {
            this.awayClub = club;
            return this;
        }

        public Match Build()
        {
            if (!idWasSet)
            {
                id = ++count;
            }
            return new Match(id, dateOfTheMatch, homeClub, awayClub);
        }
    }
}
