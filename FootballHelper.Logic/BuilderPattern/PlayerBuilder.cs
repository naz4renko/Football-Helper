using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic.BuilderPattern
{
    public class PlayerBuilder
    {
        private int id = 1;
        private string name = "Ваня";
        private string nationality = "Россия";
        private DateOnly birth = new DateOnly(1990, 1, 1);
        private string position = "нападающий";

        private static int count = 0;
        private bool idWasSet = false;

        public PlayerBuilder WithID(int id)
        {
            this.id = id;
            this.idWasSet = true;
            return this;
        }

        public PlayerBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public PlayerBuilder WithNationality(string nationality)
        {
            this.nationality = nationality;
            return this;
        }

        public PlayerBuilder WithBirth(DateOnly birth)
        {
            this.birth = birth;
            return this;
        }

        public PlayerBuilder WithPosition(string position)
        {
            this.position = position;
            return this;
        }

        public Player Build()
        {
            if (!idWasSet)
            {
                id = ++count;
            }
            return new Player(id, name, nationality, birth, position);
        }
    }
}
