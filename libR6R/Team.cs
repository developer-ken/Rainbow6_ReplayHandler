using Newtonsoft.Json.Linq;

namespace libR6R
{
    public class Team
    {
        public string Name;
        public int Score = 0;
        public List<Player> PlayerList = new List<Player>(5);
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
            PlayerList.Add(p);
        }
    }
}
