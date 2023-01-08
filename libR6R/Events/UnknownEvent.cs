using Newtonsoft.Json.Linq;

namespace libR6R.Events
{
    public class UnknownEvent : Event
    {
        public JObject RawJson;
        public UnknownEvent(JObject rawJson)
        {
            RawJson = rawJson;
            Type = EventType.UnKnown;
        }
    }
}
