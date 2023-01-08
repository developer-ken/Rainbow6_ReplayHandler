using Newtonsoft.Json.Linq;

namespace libR6R.Events
{
    public class KillEvent : Event
    {
        public bool Headshot { get; protected set; }
        public KillEvent(JObject jb, Dictionary<string, Player> playerlist)
        {
            if (jb.Value<string>("type") != "KILL")
                throw new InvalidOperationException("Provided json is not a valid kill event.");
            Type = EventType.Kill;
            var fromplayer = jb.Value<string>("username");
            var targetplayer = jb.Value<string>("target");
            Headshot = jb.Value<bool>("headshot");

            FromPlayer = playerlist.ContainsKey(fromplayer) ? playerlist[fromplayer] : Eval(() =>
            {
                var pl = new Player(0, fromplayer, "", 0, null, 0, 0, 0, 0);
                lock (playerlist) playerlist.Add(fromplayer, pl);
                return pl;
            });
            TargetPlayer = playerlist.ContainsKey(targetplayer) ? playerlist[targetplayer] : Eval(() =>
            {
                var pl = new Player(0, targetplayer, "", 0, null, 0, 0, 0, 0);
                lock (playerlist) playerlist.Add(targetplayer, pl);
                return pl;
            });

            FromPlayer.Kills++;
            TargetPlayer.Killed++;
        }

        private static T Eval<T>(Func<T> f)
        {
            return f();
        }
    }
}
