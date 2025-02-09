using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic
{
    public class Player
    {
        public int ID { get;private set; }
        public string Name { get; private set; }
        public string Nationality { get; private set; }
        public DateOnly Birth { get; private set; }
        public string Position { get; private set; }
        public Player(int iD, string name, string nationality, DateOnly birth, string position)        
        {
            ID = iD;
            Name = name;
            Nationality = nationality;
            Birth = birth;
            Position = position;
        }
    }
}
