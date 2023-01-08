using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libR6R
{
    public class Team
    {
        public string Name;
        public int Score = 0;
        public Player[] PlayerList = new Player[5];
        private int playerindex;

        public Team(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public Team(JObject jb)
        {
            Name = jb.Value<string>("name");
            Score = jb.Value<int>("score");
        }

        public void AddPlayer(Player p)
        {
            if (playerindex >= 5)
                throw new IndexOutOfRangeException("Too many players in a team. Max 5 allowed.");
            PlayerList[playerindex++] = p;
        }
    }
}
