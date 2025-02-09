using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace FootballHelper.WPF.ViewModel
{
    public class CreatePlayerViewModel : ViewModelBase
    {
        private IRepository repo;
        private MainWindowViewModel mvVM;
        private Club club;

        private string name;
        private string nationality;
        private DateTime birth;
        private string position;

        private string _buttonText = "Подтвердить";
        private bool _isButtonEnabled = true;
        private Brush buttonTextColor;

        public CreatePlayerViewModel(Club club, IRepository repo, MainWindowViewModel mvVM)
        {
            this.club = club;
            this.repo = repo;
            this.mvVM = mvVM;

            birth = DateTime.Now;
            ButtonTextColor = Brushes.Black;
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

        public DateTime Birth
        {
            get => birth;
            set
            {
                if (birth == value) return;
                birth = value;
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

        public ICommand CreatePlayer
                => new RelayCommand((_) => CreatePlayerImpl());

        private void CreatePlayerImpl()
        {
            if (CheckString(Name) && CheckString(Nationality) && CheckString(Position))
            {
                Player player = new Player(-1, Name, Nationality, DateOnly.FromDateTime(Birth), Position);
                int plID = repo.InsertPlayer(player);
                repo.InsertPlayerInClub(club, plID);

                ButtonText = "Успешно";
                ButtonTextColor = Brushes.Green;
                IsButtonEnabled = false;
            }
            else
            {
                ButtonTextColor = Brushes.Red;
                ButtonText = "Ошибка!";
            }
        }

        public ICommand ToClub
                => new RelayCommand(async (_) => await ToClubImplAsync());

        private async Task ToClubImplAsync()
        {
            Club myClub = await repo.UpdateClubAsync(club);
            mvVM.Content = new ClubViewModel(repo, myClub, mvVM);
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

        public bool CheckString(string s)
        {
            if (s == null)
                return false;

            string pattern = @"^[а-яА-Я\s\-]+$";
            return Regex.IsMatch(s, pattern);
        }
    }
}
