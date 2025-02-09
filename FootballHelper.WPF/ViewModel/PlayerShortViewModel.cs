using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.WPF.ViewModel
{
    public class PlayerShortViewModel : ViewModelBase
    {
        private Player player;

        public PlayerShortViewModel(Player player)
        {
            this.player = player;
        }

        public Player Player { get { return player; } }

        public string PlayerInfo
        {
            get => $"{player.Name} - {player.Position}";
        }
    }
}
