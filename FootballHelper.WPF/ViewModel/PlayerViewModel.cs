using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using FootballHelper.Logic;
using FootballHelper.Logic.StrategyPattern;

namespace FootballHelper.WPF.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private readonly IRepository repo;

        private Player player;
        private Club currentClub;
        private readonly MainWindowViewModel mvVM;

        private Dictionary<string, double> totalPlayerStatistic;
        private Dictionary<string, double> avaragePlayerStatistic;

        private string name;
        private string nationality;
        private string position;
        private DateOnly birth;

        public PlayerViewModel(Club club, IRepository repo, Player player, MainWindowViewModel mvVM)
        {
            currentClub = club;
            this.repo = repo;
            this.player = player;
            this.mvVM = mvVM;

            name = player.Name;
            nationality = player.Nationality;
            position = player.Position;
            birth = player.Birth;

        }

        public async Task InitializeAsync()
        {
            var totalContext = new StatisticsContext(new TotalStatisticsStrategy());
            var avarageContext = new StatisticsContext(new AverageStatisticsStrategy());

            var allPlayerStats = await repo.GetAllStatisticForPlayerAsync(player);

            totalPlayerStatistic = totalContext.CalculatePlayerStatistics(player, allPlayerStats);
            avaragePlayerStatistic = avarageContext.CalculatePlayerStatistics(player, allPlayerStats);
        }

        public string Name
        {
            get => name;
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged();
            }
        }
        public string Nationality
        {
            get => nationality;
            set
            {
                if (nationality == value) return;
                nationality = value;
                OnPropertyChanged();
            }
        }
        public string Position
        {
            get => position;
            set
            {
                if (position == value) return;
                position = value;
                OnPropertyChanged();
            }
        }

        public string Birth
        {
            get => birth.ToString();
        }

        public string TotalStats
        {
            get
            {
                string totalStats = "";
                foreach (var stats in totalPlayerStatistic)
                {
                    totalStats += $"{stats.Key}: {stats.Value} \n";
                }
                return totalStats;
            }
        }

        public string AverageStats
        {
            get
            {
                string averageStats = "";
                foreach (var stats in avaragePlayerStatistic)
                {
                    averageStats += $"{stats.Key}: {stats.Value} \n";
                }
                return averageStats;
            }
        }

        public ICommand ToClub
        => new RelayCommand((_) => ToClubImpl());

        private void ToClubImpl()
        {
            mvVM.Content = new ClubViewModel(repo, currentClub, mvVM);
        }


    }
}
