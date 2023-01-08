using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
