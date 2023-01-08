namespace libR6R
{
    public class MatchReplay : IEquatable<MatchReplay>
    {
        public Dictionary<int, RoundReplay> Rounds = new Dictionary<int, RoundReplay>();
        public Dictionary<string, Player> Players = new Dictionary<string, Player>();
        public int HostTotalKill = 0, HostTotalDeath = 0;
        public DateTime MatchTime { get; private set; } = DateTime.Now;
        public MatchType MatchType { get; private set; } = MatchType.UnKnown;
        public GameMode GameMode { get; private set; } = GameMode.UnKnown;
        public Player RecPlayer { get; private set; }
        public string MapName { get; private set; }
        public ulong RecPlayerId { get; private set; } = 0;
        public Map Map { get; private set; } = 0;
        public int RoundsPerMatch { get; private set; } = -1;
        public int RoundsPerMatchOvertime { get; private set; } = -1;
        public string DirPath;

        public MatchReplay(string replaydir)
        {
            DirPath = replaydir;
            _FromDirectoryAsync(replaydir).Wait();
        }

        private MatchReplay() { }

        public bool IsRoundContained(string file)
        {
            foreach (var rr in Rounds.Values)
            {
                if (Path.Equals(file, rr.SourceFile)) return true;
            }
            return false;
        }

        private async Task _FromDirectoryAsync(string replaydir)
        {
            var files = Directory.GetFiles(replaydir);
            List<Task<RoundReplay>> rounds = new List<Task<RoundReplay>>();
            foreach (var file in files)
            {
                if (Path.GetExtension(file).ToLower() == ".rec")
                {
                    if (IsRoundContained(file)) continue;
                    var round = RoundReplay.ReadFromFileAsync(file, Players);
                    if (!rounds.Contains(round))
                        rounds.Add(round);
                }
            }
            foreach (var task in rounds)
            {
                await task;
                var round = task.Result;
                await round.DumpRawjsonAsync();
                if (Rounds.Values.Contains(round)) continue;
                Rounds.Add(round.RoundNumber + round.OvertimeRoundNumber, round);

                HostTotalKill += round.HostKills;
                HostTotalDeath += round.HostDeaths;

                if (round.MatchTime < MatchTime) MatchTime = round.MatchTime;

                if (MatchType == MatchType.UnKnown) MatchType = round.MatchType;
                else if (MatchType != round.MatchType)
                    throw new InvalidOperationException("Multiple rounds not happed in the same MatchType, which is invalid.");

                if (GameMode == GameMode.UnKnown) GameMode = round.GameMode;
                else if (GameMode != round.GameMode)
                    throw new InvalidOperationException("Multiple rounds not happed in the same GameMode, which is invalid.");

                if (MapName is null) MapName = round.MapName;
                else if (MapName != round.MapName)
                    throw new InvalidOperationException("Multiple rounds not happed in the same Map, which is invalid.");

                if (Map == 0) Map = round.Map;
                else if (Map != round.Map)
                    throw new InvalidOperationException("Multiple rounds not happed in the same Map, which is invalid.");

                if (RecPlayerId == 0)
                {
                    RecPlayerId = round.RecPlayerId;
                }
                else if (RecPlayerId != round.RecPlayerId)
                    throw new InvalidOperationException("Multiple rounds not from a same Player, which is invalid.");

                if (RoundsPerMatch == -1) RoundsPerMatch = round.RoundsPerMatch;
                else if (RoundsPerMatch != round.RoundsPerMatch)
                    throw new InvalidOperationException("Multiple rounds have different round count configurations, which is invalid.");

                if (RoundsPerMatchOvertime == -1) RoundsPerMatchOvertime = round.RoundsPerMatchOvertime;
                else if (RoundsPerMatchOvertime != round.RoundsPerMatchOvertime)
                    throw new InvalidOperationException("Multiple rounds have different extra round count configurations, which is invalid.");

            }

            foreach (var player in Players)
            {
                if (player.Value.Id == RecPlayerId)
                {
                    RecPlayer = player.Value;
                    break;
                }
            }
        }

        public static async Task<MatchReplay> FromDirectoryAsync(string replaydir)
        {
            var item = new MatchReplay();
            item.DirPath = replaydir;
            await item._FromDirectoryAsync(replaydir);
            return item;
        }

        public string GetCorrectSaveDirName()
        {
            if (Rounds.Count == 0) return "";
            var fname = Path.GetFileNameWithoutExtension(Rounds.First().Value.SourceFile);
            return fname.Substring(0, fname.Length - 4);
        }

        public async Task<bool> UpdateAsync()
        {
            var cnt = Rounds.Count;
            await _FromDirectoryAsync(DirPath);
            return cnt < Rounds.Count;
        }

        public bool IsRelatedPath(string path)
        {
            return path.StartsWith(DirPath);
        }

        public bool Equals(MatchReplay? other)
        {
            if (this.Rounds.Count == 0) return false;
            return this.Rounds.First().Value.MatchId.Equals(other?.Rounds.First().Value.MatchId);
        }
    }
}
