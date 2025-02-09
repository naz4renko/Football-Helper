using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.WPF.ViewModel
{
    public class MatchShortViewModel : ViewModelBase
    {
        private IRepository repo;
        private Match match;
        private int[] score;
        public MatchShortViewModel(IRepository repo, Match match)
        {
            this.match = match;
            this.repo = repo;
        }

        public async Task InitializeAsync()
        {
            var statsPerMatch = await repo.GetGoalsForMatchAsync(match);
            score = match.CalculateFinalScore(statsPerMatch);
        }

        public Match Match { get { return match; } }

        public string MatchInfo
        {
            get => $"{match.HomeClub.Name} {score[0]} : {score[1]} {match.AwayClub.Name} - {match.DateOfTheMatch}";
        }
    }
}
