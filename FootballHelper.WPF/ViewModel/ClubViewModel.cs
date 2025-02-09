using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using FootballHelper.Logic;

namespace FootballHelper.WPF.ViewModel
{
    public class ClubViewModel : ViewModelBase
    {
        private IRepository repo;
        private Club club;
        private List<Player> playerList;
        private readonly MainWindowViewModel mvVM;
        private ObservableCollection<PlayerShortViewModel> players;
        private PlayerShortViewModel selectedPlayer;
        
        public ClubViewModel(IRepository repo, Club club, MainWindowViewModel mvVM)
        {
            this.repo = repo;
            this.club = club;
            this.mvVM = mvVM;

            IEnumerable<PlayerShortViewModel> playersVMs =
                from player in club.Players
                select new PlayerShortViewModel(player);
            players = new ObservableCollection<PlayerShortViewModel>(playersVMs);
        }

        public string Name
        {
            get => club.Name;
        }

        public ObservableCollection<PlayerShortViewModel> Players
        {
            get => players;
            set => players = value;
        }

        public PlayerShortViewModel SelectedPlayer
        {
            get => selectedPlayer;
            set
            {
                if (selectedPlayer == value) return;
                selectedPlayer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisible));
            }
        }
        public ICommand AddPlayer
        {
            get
            {
                return new RelayCommand((_) => AddPlayerImpl());
            }
        }
        private void AddPlayerImpl()
        {
            mvVM.Content = new CreatePlayerViewModel(club, repo, mvVM);
        }
        public ICommand ShowPlayer
        {
            get
            {
                return new RelayCommand(
                    async (_) => await ShowPlayerImplAsync(),
                    (_) => CanShowPlayer());
            }
        }

        private async Task ShowPlayerImplAsync()
        {
            Player player = selectedPlayer.Player;
            var showPlayer = new PlayerViewModel(club,repo, player, mvVM);
            await showPlayer.InitializeAsync();
            mvVM.Content = showPlayer;
        }

        public ICommand DeletePlayer
        {
            get
            {
                return new RelayCommand(
                    async (_) => await DeletePlayerImplAsync());
            }
        }

        public async Task DeletePlayerImplAsync()
        {
            Player player = selectedPlayer.Player;
            repo.DeletePlayer(player);

            Club myClub = await repo.UpdateClubAsync(club);
            mvVM.Content = new ClubViewModel(repo, myClub, mvVM);
        }

        private bool CanShowPlayer()
        {
            return selectedPlayer != null;
        }
        public bool IsVisible => CanShowPlayer();
    }
}
