using FootballHelper.Logic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Reflection.Metadata.BlobBuilder;

namespace FootballHelper.WPF.ViewModel
{
    public class MatchViewModel : ViewModelBase
    {
        private IRepository repo;
        private MainWindowViewModel mvVM;
        private ObservableCollection<MatchShortViewModel> matches;
        private MatchShortViewModel selectedMatch;

        public MatchViewModel(IRepository repo, MainWindowViewModel mvVM)
        {
            this.repo = repo;
            this.mvVM = mvVM;

            matches = new ObservableCollection<MatchShortViewModel>();
        }

        public async Task InitializeAsync()
        {
            IEnumerable<Match> matchList = await repo.GetMatchesAsync();

            var matchVMs = matchList.Select(match => new MatchShortViewModel(repo, match)).ToArray();

            foreach (var matchVM in matchVMs)
            {
                await matchVM.InitializeAsync();
            }

            Matches = new ObservableCollection<MatchShortViewModel>(matchVMs);
        }

        public ObservableCollection<MatchShortViewModel> Matches
        {
            get => matches;
            set => matches = value;
        }

        public MatchShortViewModel SelectedMatch
        {
            get => selectedMatch;
            set
            {
                if (selectedMatch == value) return;
                selectedMatch = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public ICommand ShowStats
        {
            get
            {
                return new RelayCommand(
                    async (_) => await ShowStatsImpl(),
                    (_) => CanShowStats());
            }
        }

        private async Task ShowStatsImpl()
        {
            Match match = selectedMatch.Match;
            var workMatch = new MatchStatsViewModel(repo, match, mvVM);
            await workMatch.InitializeAsync();
            mvVM.Content = workMatch;
        }

        public ICommand AddMatch
        {
            get
            {
                return new RelayCommand(
                    async (_) => await AddMatchImplAsync());
            }
        }

        private async Task AddMatchImplAsync()
        {
            var matchCreate = new CreateMatchViewModel(repo, mvVM);
            await matchCreate.InitializeAsync();
            mvVM.Content = matchCreate;
        }

        public ICommand DeleteMatch
        {
            get
            {
                return new RelayCommand(
                    async (_) => await DeleteMtchImplAsync(),
                    (_) => CanShowStats());
            }
        }

        public async Task DeleteMtchImplAsync()
        {
            Match match = selectedMatch.Match;
            repo.DeleteMatch(match);

            var matches = new MatchViewModel(repo, mvVM);
            await matches.InitializeAsync();
            mvVM.Content = matches;
        }

        private bool CanShowStats()
        {
            return selectedMatch != null;
        }

        public bool IsVisible => CanShowStats();
    }
}
