using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace FootballHelper.WPF.ViewModel
{
    public class CreateClubViewModel : ViewModelBase
    {
        private readonly IRepository repo;
        private readonly MainWindowViewModel mwVM;
        private string name;

        private bool _isButtonEnabled = true;
        private string _buttonText = "Подтвердить";
        private Brush buttonTextColor;

        public CreateClubViewModel(IRepository repo, MainWindowViewModel mvVM)
        {
            this.repo = repo;
            this.mwVM = mvVM;

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

        public ICommand CreateClub
            => new RelayCommand((_) => CreateClubImpl());

        private void CreateClubImpl()
        {
            if ((CheckString(name) == true))
            {
                repo.InsertClub(name);

                ButtonText = "Успешно!";
                ButtonTextColor = Brushes.Green;
                IsButtonEnabled = false;
            }
            else
            {
                ButtonText = "Некорректно!";
                ButtonTextColor = Brushes.Red;
            }
        }

        public ICommand ToTable
            => new RelayCommand(async (_) => await ToTableImplAsync());

        private async Task ToTableImplAsync()
        {
            var table = new TableViewModel(repo, mwVM);
            await table.InitializeAsync();
            mwVM.Content = table;
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

        public bool CheckString(string s)
        {
            if (s == null)
                return false;

            string pattern = @"^[а-яА-Я\s\-]+$";
            return Regex.IsMatch(s, pattern);
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
    }
}
