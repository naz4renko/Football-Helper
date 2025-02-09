using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.WPF.ViewModel
{
    public class PlayerStatsShortViewModel : ViewModelBase
    {
        private IRepository repo;
        private PlayerStatisticsPerMatch plStats;
        private Match match;

        public PlayerStatsShortViewModel(IRepository repo, PlayerStatisticsPerMatch playerStats, Match match)
        {
            this.repo = repo;
            this.match = match;

            plStats = playerStats;
        }

        public PlayerStatisticsPerMatch PlayerStatistic
        {
            get { return plStats; }
        }

        public string PlayerStats
        {
            get => $"{plStats.Player.Name} - г.{plStats.Goals} - ас.{plStats.Assists} - уд.{plStats.Shoots} - отб.{plStats.Tackling} - ж.к.{(plStats.YellowCart ? 1 : 0)} - к.к.{(plStats.RedCart ? 1 : 0)}";
        }

    }

}
