using Newtonsoft.Json.Linq;

namespace libR6R
{
    public class Player
    {
        public ulong Id { get; private set; }
        public string Name { get; private set; }
        public string HeroName { get; private set; }
        public ulong Hero { get; private set; }
        public Team Team { get; private set; }
        public ulong RoleImage { get; private set; }
        public ulong RolePortrait { get; private set; }

        public int Killed;
        public int Kills;

        public Player(JObject jb, Team[] teams)
        {
            Id = jb.Value<ulong>("id");
            Name = jb.Value<string>("username");
            HeroName = jb.Value<string>("heroName");
            Hero = jb.Value<ulong>("heroName");
            Team = teams[jb.Value<int>("teamIndex")];
            RoleImage = jb.Value<ulong>("roleImage");
            RolePortrait = jb.Value<ulong>("rolePortrait");
            Team.AddPlayer(this);
        }

        public Player(ulong id, string name, string heroName, ulong hero, Team team, ulong roleImage, ulong rolePortrait, int killed, int kills)
        {
            Id = id;
            Name = name;
            HeroName = heroName;
            Hero = hero;
            Team = team;
            RoleImage = roleImage;
            RolePortrait = rolePortrait;
            Killed = killed;
            Kills = kills;
        }
    }
}
