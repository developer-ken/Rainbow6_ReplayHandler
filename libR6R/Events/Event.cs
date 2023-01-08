using Newtonsoft.Json.Linq;

namespace libR6R.Events
{
    public abstract class Event
    {
        public Player FromPlayer { get; protected set; }
        public Player TargetPlayer { get; protected set; }
        public EventType Type { get; protected set; }

        public static Event FromJson(JObject jb, Dictionary<string, Player> plist)
        {
            var type = jb.Value<string>("type");
            switch (type)
            {
                case "KILL":
                    {
                        return new KillEvent(jb, plist);
                    }
            }
            return new UnknownEvent(jb);
        }
    }
    public enum EventType
    {
        UnKnown, Kill, Death, Plant, Defuse, LocateObjective, BattleEyeKick, PlayerLeave
    }
}
