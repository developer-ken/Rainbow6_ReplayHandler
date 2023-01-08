
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;
using libR6R.Events;

namespace libR6R
{
    public class RoundReplay : IEquatable<RoundReplay>
    {
        private static bool registered = false;

        public string GameVersion { get; private set; }
        public int CodeVersion { get; private set; }
        public string MatchTypeStr { get; private set; }
        public DateTime MatchTime { get; private set; }
        public MatchType MatchType { get; private set; }
        public string MapName { get; private set; }
        public ulong RecPlayerId { get; private set; }
        public Map Map { get; private set; }
        public GameMode GameMode { get; private set; }
        public int RoundsPerMatch { get; private set; }
        public int RoundsPerMatchOvertime { get; private set; }
        public int RoundNumber { get; private set; }
        public int OvertimeRoundNumber { get; private set; }
        public Team[] Teams = new Team[2];
        public Dictionary<string, Player> Players;
        public List<ulong> GMSettings = new List<ulong>();
        public ulong PlaylistCategory;
        public string MatchId;
        public List<Event> Events = new List<Event>();
        public JObject RawJson;
        public string SourceFile;
        public Player? RecPlayer
        {
            get
            {
                foreach (var player in Players)
                {
                    if (player.Value.Id == RecPlayerId)
                    {
                        return player.Value;
                    }
                }
                return null;
            }
        }

        public int HostKills, HostDeaths;

        public RoundReplay(JObject jb)
        {
            RawJson = jb;
            Players = new Dictionary<string, Player>();
            GameVersion = jb["header"].Value<string>("gameVersion");
            CodeVersion = jb["header"].Value<int>("codeVersion");
            MatchTime = DateTime.Parse(jb["header"].Value<string>("timestamp"));
            MatchType = (MatchType)jb["header"]["matchType"].Value<int>("id");
            MatchTypeStr = jb["header"]["matchType"].Value<string>("name");
            MapName = jb["header"]["map"].Value<string>("name");
            Map = (Map)jb["header"]["map"].Value<ulong>("id");
            RecPlayerId = jb["header"].Value<ulong>("recordingPlayerID");
            GameMode = (GameMode)jb["header"]["gamemode"].Value<ulong>("id");
            RoundsPerMatch = jb["header"].Value<int>("roundsPerMatch");
            RoundsPerMatchOvertime = jb["header"].Value<int>("roundsPerMatchOvertime");
            RoundNumber = jb["header"].Value<int>("roundNumber");
            OvertimeRoundNumber = jb["header"].Value<int>("overtimeRoundNumber");

            Teams[0] = new Team((JObject)jb["header"]["teams"][0]);
            Teams[1] = new Team((JObject)jb["header"]["teams"][1]);

            PlaylistCategory = jb["header"].Value<ulong>("playlistCategory");
            MatchId = jb["header"].Value<string>("matchID");

            foreach (var p in jb["header"]["players"])
            {
                Player player = new Player((JObject)p, Teams);
                if (!Players.ContainsKey(player.Name))
                    Players.Add(player.Name, player);
                else
                {
                    if (Players[player.Name].Id < 1)
                        Players[player.Name] = player;
                }
            }

            foreach (var p in jb["header"]["gmSettings"])
            {
                GMSettings.Add(p.Value<ulong>());
            }

            foreach (var eve in jb["activityFeed"])
            {
                var c_event = Event.FromJson((JObject)eve, Players);
                if (c_event is not null)
                {
                    switch (c_event.Type)
                    {
                        case EventType.UnKnown:
                            break;
                        case EventType.Kill:
                            {
                                var e = (KillEvent)c_event;
                                if (e.TargetPlayer == RecPlayer) HostDeaths++;
                                else
                                if (e.FromPlayer == RecPlayer) HostKills++;
                            }
                            break;
                        case EventType.Death:
                            break;
                        case EventType.Plant:
                            break;
                        case EventType.Defuse:
                            break;
                        case EventType.LocateObjective:
                            break;
                        case EventType.BattleEyeKick:
                            break;
                        case EventType.PlayerLeave:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public RoundReplay(JObject jb, Dictionary<string, Player> players)
        {
            RawJson = jb;
            Players = players;
            GameVersion = jb["header"].Value<string>("gameVersion");
            CodeVersion = jb["header"].Value<int>("codeVersion");
            MatchTime = DateTime.Parse(jb["header"].Value<string>("timestamp"));
            MatchType = (MatchType)jb["header"]["matchType"].Value<int>("id");
            MatchTypeStr = jb["header"]["matchType"].Value<string>("name");
            MapName = jb["header"]["map"].Value<string>("name");
            Map = (Map)jb["header"]["map"].Value<ulong>("id");
            RecPlayerId = jb["header"].Value<ulong>("recordingPlayerID");
            GameMode = (GameMode)jb["header"]["gamemode"].Value<ulong>("id");
            RoundsPerMatch = jb["header"].Value<int>("roundsPerMatch");
            RoundsPerMatchOvertime = jb["header"].Value<int>("roundsPerMatchOvertime");
            RoundNumber = jb["header"].Value<int>("roundNumber");
            OvertimeRoundNumber = jb["header"].Value<int>("overtimeRoundNumber");

            Teams[0] = new Team((JObject)jb["header"]["teams"][0]);
            Teams[1] = new Team((JObject)jb["header"]["teams"][1]);

            PlaylistCategory = jb["header"].Value<ulong>("playlistCategory");
            MatchId = jb["header"].Value<string>("matchID");

            foreach (var p in jb["header"]["players"])
            {
                Player player = new Player((JObject)p, Teams);
                if (!Players.ContainsKey(player.Name))
                    Players.Add(player.Name, player);
                else
                {
                    if (Players[player.Name].Id < 1)
                        Players[player.Name] = player;
                }
            }

            foreach (var p in jb["header"]["gmSettings"])
            {
                GMSettings.Add(p.Value<ulong>());
            }

            foreach (var eve in jb["activityFeed"])
            {
                var c_event = Event.FromJson((JObject)eve, Players);
                if (c_event is not null)
                {
                    switch (c_event.Type)
                    {
                        case EventType.UnKnown:
                            break;
                        case EventType.Kill:
                            {
                                var e = (KillEvent)c_event;
                                if (e.TargetPlayer == RecPlayer) HostDeaths++;
                                else
                                if (e.FromPlayer == RecPlayer) HostKills++;
                            }
                            break;
                        case EventType.Death:
                            break;
                        case EventType.Plant:
                            break;
                        case EventType.Defuse:
                            break;
                        case EventType.LocateObjective:
                            break;
                        case EventType.BattleEyeKick:
                            break;
                        case EventType.PlayerLeave:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static RoundReplay ReadFromFile(string filepath)
        {
            var jb = RawRec2Json(filepath);
            return new RoundReplay(jb) { SourceFile = filepath };
        }

        public static async Task<RoundReplay> ReadFromFileAsync(string filepath, Dictionary<string, Player> plist = null)
        {
            var dir = Path.GetDirectoryName(filepath);
            var fname = Path.GetFileNameWithoutExtension(filepath);
            var bakedjson = Path.Combine(dir, fname + ".snapshot.json");
            JObject jb;
            if (File.Exists(bakedjson))
            {
                try
                {
                    jb = JObject.Parse(File.ReadAllText(bakedjson));
                }
                catch
                {
                    jb = await RawRec2JsonAsync(filepath);
                }
            }
            else
            {
                jb = await RawRec2JsonAsync(filepath);
            }
            if (plist != null)
                return new RoundReplay(jb, plist) { SourceFile = filepath };
            else
                return new RoundReplay(jb) { SourceFile = filepath };
        }

        public async Task DumpRawjsonAsync(string recfilepath = null)
        {
            string _recfilepath = SourceFile;
            if (recfilepath is not null)
            {
                _recfilepath = recfilepath;
            }
            if (_recfilepath is null)
            {
                throw new ArgumentNullException("Can't figure out recfilepath.");
            }
            var dir = Path.GetDirectoryName(_recfilepath);
            var fname = Path.GetFileNameWithoutExtension(_recfilepath);
            var bakedjson = Path.Combine(dir, fname + ".snapshot.json");
            if (File.Exists(bakedjson)) File.Delete(bakedjson);
            await File.WriteAllTextAsync(bakedjson, RawJson.ToString());
            File.SetAttributes(bakedjson, FileAttributes.Hidden);
        }

        private static JObject RawRec2Json(string filepath)
        {
            if (!registered)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                registered = true;
            }
            Process r6d = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(Environment.CurrentDirectory, "thirdparts/libr6-dissect/r6-dissect.exe");
            startInfo.ArgumentList.Add(filepath);
            startInfo.ArgumentList.Add("-x");
            startInfo.ArgumentList.Add("stdout");
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            startInfo.StandardOutputEncoding = Encoding.UTF8;
            r6d.StartInfo = startInfo;
            r6d.Start();
            r6d.WaitForExit();
            return JObject.Parse(r6d.StandardOutput.ReadToEnd());
        }

        private static Task<JObject> RawRec2JsonAsync(string filepath)
        {
            return Task.Run(() => RawRec2Json(filepath));
        }

        public bool Equals(RoundReplay? other)
        {
            return this.MatchId.Equals(other?.MatchId) &&
                this.RoundNumber.Equals(other?.RoundNumber) &&
                this.OvertimeRoundNumber.Equals(other?.OvertimeRoundNumber);
        }
    }
    public enum MatchType
    {
        QuickMatch = 1, Ranked = 2, UnRanked = 12, CUSTOM_LOCAL = 7, CUSTOM_ONLINE = 8, UnKnown = 0
    }
    public enum GameMode : ulong
    {
        BOMB = 327933806, HOSTAGE = 1983085217, SECURE_AREA = 2838806006, UnKnown = 0
    }

    public enum Map : ulong
    {
        CLUB_HOUSE = 837214085,
        KAFE_DOSTOYEVSKY = 1378191338,
        KANAL = 1460220617,
        YACHT = 1767965020,
        PRESIDENTIAL_PLANE = 2609218856,
        CONSULATE = 2609221242,
        BARTLETT_U = 2697268122,
        COASTLINE = 42090092951,
        TOWER = 53627213396,
        VILLA = 88107330328,
        FORTRESS = 126196841359,
        HEREFORD_BASE = 127951053400,
        THEME_PARK = 199824623654,
        OREGON = 231702797556,
        HOUSE = 237873412352,
        CHALET = 259816839773,
        SKYSCRAPER = 276279025182,
        BORDER = 305979357167,
        FAVELA = 329867321446,
        BANK = 355496559878,
        OUTBACK = 362605108559,
        EMERALD_PLAINS = 365284490964,
        STADIUM_BRAVO = 270063334510,
        NIGHTHAVEN_LABS = 378595635123,
    }
}