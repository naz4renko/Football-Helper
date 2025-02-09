using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace FootballHelper.WPF.ViewModel
{
    public class MatchChangeStatsViewModel : ViewModelBase
    {
        private IRepository repo;
        private Match match;
        private readonly MainWindowViewModel mvVM;

        private int homePossesion;
        private int homeCorner;
        private int homeOffside;

        private int awayPossesion;
        private int awayCorner;
        private int awayOffside;

        private bool _isButtonEnabled = true;
        private string _buttonText = "Подтвердить";
        private Brush buttonTextColor;

        private List<ClubStatisticsPerMatch> myClubStats;

        public MatchChangeStatsViewModel(IRepository repo, Match match, List<ClubStatisticsPerMatch> clStats, MainWindowViewModel mvVM)
        {
            this.repo = repo;
            this.match = match;
            this.mvVM = mvVM;

            myClubStats = clStats;

            homePossesion = clStats[0].Possession;
            homeCorner = clStats[0].Corners;
            homeOffside = clStats[0].Offsides;

            awayPossesion = clStats[1].Possession;
            awayCorner = clStats[1].Corners;
            awayOffside = clStats[1].Offsides;

            ButtonTextColor = Brushes.Black;
        }

        public string HomeClub
        {
            get => $"{match.HomeClub.Name}";
        }
        public string AwayClub
        {
            get => $"{match.AwayClub.Name}";
        }

        public int HomePossession
        {
            get => homePossesion;
            set
            {
                if (homePossesion == value) return;
                homePossesion = value;
                OnPropertyChanged();
            }
        }

        public int HomeCorner
        {
            get => homeCorner;
            set
            {
                if (homeCorner == value) return;
                homeCorner = value;
                OnPropertyChanged();
            }
        }

        public int HomeOffside
        {
            get => homeOffside;
            set
            {
                if (homeOffside == value) return;
                homeOffside = value;
                OnPropertyChanged();
            }
        }

        public int AwayPossession
        {
            get => awayPossesion;
            set
            {
                if (awayPossesion == value) return;
                awayPossesion = value;
                OnPropertyChanged();
            }
        }

        public int AwayCorner
        {
            get => awayCorner;
            set
            {
                if (awayCorner == value) return;
                awayCorner = value;
                OnPropertyChanged();
            }
        }

        public int AwayOffside
        {
            get => awayOffside;
            set
            {
                if (awayOffside == value) return;
                awayOffside = value;
                OnPropertyChanged();
            }
        }

        public ICommand ComfirmStats
            => new RelayCommand(async (_) => await ConfirmStatsImplAsync());

        private async Task ConfirmStatsImplAsync()
        {
            List<ClubStatisticsPerMatch> workList = new List<ClubStatisticsPerMatch>();
            ClubStatisticsPerMatch homeStats = new ClubStatisticsPerMatch(myClubStats[0].ID, myClubStats[0].Club, match, HomePossession, HomeCorner, HomeOffside);
            ClubStatisticsPerMatch awayStats = new ClubStatisticsPerMatch(myClubStats[1].ID, myClubStats[1].Club, match, AwayPossession, AwayCorner, AwayOffside);
            workList.Add(homeStats);
            workList.Add(awayStats);

            if (CheckStatistic(workList))
            {
                if (myClubStats[0].ID == -1 || myClubStats[1].ID == -1)
                {
                    int matchID = repo.InsertMatch(match);

                    Match workMatch = new Match(matchID, match.DateOfTheMatch, match.HomeClub, match.AwayClub);

                    match = workMatch;

                    ClubStatisticsPerMatch workHomeStats = new ClubStatisticsPerMatch(myClubStats[0].ID, myClubStats[0].Club, workMatch, HomePossession, HomeCorner, HomeOffside);
                    ClubStatisticsPerMatch workAwayStats = new ClubStatisticsPerMatch(myClubStats[1].ID, myClubStats[1].Club, workMatch, AwayPossession, AwayCorner, AwayOffside);

                    repo.InsertClubStatistics(workHomeStats);
                    repo.InsertClubStatistics(workAwayStats);
                }
                else
                {
                    await repo.UpdateClubStatisticsAsync(workList[0]);
                    await repo.UpdateClubStatisticsAsync(workList[1]);
                }

                ButtonText = "Успешно!";
                IsButtonEnabled = false;
                ButtonTextColor = Brushes.Green;
            }
            else
            {
                ButtonTextColor = Brushes.Red;
                ButtonText = "Ошибка!!";
            }

        }

        public ICommand ToMatch
            => new RelayCommand(async (_) => await ToMatchImplAsync());

        private async Task ToMatchImplAsync()
        {
            var workMatch = new MatchStatsViewModel(repo, match, mvVM);
            if (_buttonText == "Успешно!")
            {
                await workMatch.InitializeAsync();
                mvVM.Content = workMatch;
            }
            else
            {
                var matchVM = new MatchViewModel(repo, mvVM);
                await matchVM.InitializeAsync();
                mvVM.Content = matchVM;
            }
        }


        public bool CheckStatistic(List<ClubStatisticsPerMatch> clubStats)
        {
            bool result = true;

            if ((clubStats[0].Possession + clubStats[1].Possession) != 100)
                return false;
            if (clubStats[0].Corners < 0 || clubStats[1].Corners < 0)
                return false;
            if (clubStats[0].Offsides < 0 || clubStats[1].Offsides < 0)
                return false;

            return result;
        }

        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                if (_isButtonEnabled != value)
                {
                    _isButtonEnabled = value;
                    OnPropertyChanged(nameof(IsButtonEnabled));
                }
            }
        }
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    OnPropertyChanged(nameof(ButtonText));
                }
            }
        }

        public Brush ButtonTextColor
        {
            get => buttonTextColor;
            set
            {
                if (buttonTextColor != value)
                {
                    buttonTextColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CheckMatchStats()
        {
            bool check = true;

            if (homePossesion == 0 || awayPossesion == 0)
            {
                check = false;
            }
            if (homeCorner == 0 && awayCorner == 0)
            {
                check = false;
            }
            if (homeOffside == 0 && awayOffside == 0)
            {
                check = false;
            }

            return check;

        }

    }
}
