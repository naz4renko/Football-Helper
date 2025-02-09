using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace FootballHelper.WPF.ViewModel
{
    public class PlayerChangeStatsViewModel : ViewModelBase
    {
        private IRepository repo;
        private Match match;
        private readonly MainWindowViewModel mvVM;
        private PlayerStatisticsPerMatch plStat;

        private string _selectedYellowCardOption;
        private string _selectedRedCardOption;

        public ObservableCollection<string> YellowCardOptions { get; }
        public ObservableCollection<string> RedCardOptions { get; }

        private int goals;
        private int assists;
        private int shoots;
        private int tackling;

        private bool _isButtonEnabled = true;
        private string _buttonText = "Подтвердить";

        public PlayerChangeStatsViewModel(IRepository repo, PlayerStatisticsPerMatch plStat, Match match, MainWindowViewModel mvVM)
        {
            this.repo = repo;
            this.plStat = plStat;
            this.match = match;
            this.mvVM = mvVM;

            YellowCardOptions = new ObservableCollection<string> { "Да", "Нет" };
            RedCardOptions = new ObservableCollection<string> { "Да", "Нет" };

            SelectedYellowCardOption = "Нет";
            SelectedRedCardOption = "Нет";

            goals = plStat.Goals;
            assists = plStat.Assists;
            shoots = plStat.Shoots;
            tackling = plStat.Tackling;
        }
        public string SelectedYellowCardOption
        {
            get => _selectedYellowCardOption;
            set
            {
                if (_selectedYellowCardOption != value)
                {
                    _selectedYellowCardOption = value;
                    OnPropertyChanged(nameof(SelectedYellowCardOption));
                    OnPropertyChanged(nameof(YellowCard));
                }
            }
        }

        public string SelectedRedCardOption
        {
            get => _selectedRedCardOption;
            set
            {
                if (_selectedRedCardOption != value)
                {
                    _selectedRedCardOption = value;
                    OnPropertyChanged(nameof(SelectedRedCardOption));
                    OnPropertyChanged(nameof(RedCard));
                }
            }
        }

        public string Name
        {
            get =>  $"{plStat.Player.Name}";
        }

        public int Goals
        {
            get => goals;
            set
            {
                if (goals == value) return;
                goals = value;
                OnPropertyChanged();
            }
        }

        public int Assists
        {
            get => assists;
            set
            {
                if (assists == value) return;
                assists = value;
                OnPropertyChanged();
            }
        }

        public int Shoots
        {
            get => shoots;
            set
            {
                if (shoots == value) return;
                shoots = value;
                OnPropertyChanged();
            }
        }

        public int Tackling
        {
            get => tackling;
            set
            {
                if (tackling == value) return;
                tackling = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeStats
            => new RelayCommand(async (_) => await ChangeStatsImplAsync());

        private async Task ChangeStatsImplAsync()
        {
            PlayerStatisticsPerMatch newPlStats = new PlayerStatisticsPerMatch(plStat.ID, plStat.Player, match, Goals, Assists, Shoots, Tackling, YellowCard, RedCard);
            await repo.UpdatePlayerStatisticAsync(newPlStats);

            ButtonText = "Успешно!";
            IsButtonEnabled = false;
        }


        public ICommand ToMatch
            => new RelayCommand(async (_) => await ToMatchImplAsync());

        private async Task ToMatchImplAsync()
        {
            var workMatch = new MatchStatsViewModel(repo, match, mvVM);
            await workMatch.InitializeAsync();
            mvVM.Content = workMatch;
        }

        public bool YellowCard
        {
            get => SelectedYellowCardOption == "Да";
            set
            {
                SelectedYellowCardOption = value ? "Да" : "Нет";
                OnPropertyChanged(nameof(SelectedYellowCardOption));
            }
        }

        public bool RedCard
        {
            get => SelectedRedCardOption == "Да";
            set
            {
                SelectedRedCardOption = value ? "Да" : "Нет";
                OnPropertyChanged(nameof(SelectedRedCardOption));
            }
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
    }
}
