using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballHelper.Logic;

namespace FootballHelper.WPF.ViewModel
{
    public class ClubShortViewModel : ViewModelBase 
    {
        private IRepository repo;
        private Club club;

        public ClubShortViewModel(Club club, IRepository repo)
        {
            this.club = club;
            this.repo = repo;

        }

        public Club Club { get { return club; } }

        public string ClubInfo
        {
            get => $"{club.Name}";
        }
    }
}
