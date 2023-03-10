using System.Text.RegularExpressions;

namespace libR6R
{
    public class MatchManager
    {
        string _savepath;
        public List<MatchReplay> Replays = new List<MatchReplay>();

        public string RecordPlayerName { get; set; }

        public delegate void MatchListChange(MatchReplay match);
        public delegate void EventNoArgs();
        public event MatchListChange NewMatchFound;
        public event MatchListChange MatchRemoved;
        public event MatchListChange MatchChanged;
        public event EventNoArgs UpdateFinished;
        public bool UpdateDone = false;

        public MatchManager(string savepath)
        {
            _savepath = savepath;
        }

        public static bool IsValidMatch(string savepath)
        {
            if (!Directory.Exists(savepath)) return false;
            var flist = Directory.GetFiles(savepath);
            foreach (var file in flist)
            {
                if (Path.GetExtension(file).ToLower() == ".rec") return true;
            }
            return false;
        }

        public bool IsDumplicate(string savepath)
        {
            foreach (var match in Replays)
            {
                if (Path.Equals(match.DirPath, savepath)) return true;
            }
            return false;
        }

        public bool ClearCache()
        {
            try
            {
                foreach(var match in Replays)
                {
                    match.ClearCache();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task ReloadReplay(MatchReplay replay)
        {
            if (IsValidMatch(replay.DirPath))
            {
                string matchdir = replay.DirPath;
                Replays.Remove(replay);
                MatchRemoved?.Invoke(replay);
                var match = await MatchReplay.FromDirectoryAsync(matchdir);
                Replays.Add(match);
                NewMatchFound?.Invoke(match);
            }
        }

        public async Task UpdateAsync()
        {
            UpdateDone = false;
            var flist = Directory.GetDirectories(_savepath);
            var tlist = new List<Task<MatchReplay>>();
            foreach (var f in flist)
            {
                if (!IsValidMatch(f)) continue;
                if (IsDumplicate(f)) continue;
                tlist.Add(MatchReplay.FromDirectoryAsync(f));
            }
            await Task.WhenAll(tlist);
            foreach (var task in tlist)
            {
                var match = task.Result;
                if (!Replays.Contains(match))
                {
                    lock (Replays)
                        Replays.Add(match);
                    match.RefreshKDs();
                    NewMatchFound?.Invoke(match);
                }
            }

            {
                List<MatchReplay> toBremoved = new List<MatchReplay>();
                lock (Replays)
                {
                    foreach (var match in Replays)
                    {
                        if (!IsValidMatch(match.DirPath))
                        {
                            toBremoved.Add(match);
                            MatchRemoved?.Invoke(match);
                        }
                    }
                    foreach (var match in toBremoved)
                    {
                        Replays.Remove(match);
                    }
                }
            }

            lock (Replays)
                foreach (var match in Replays)
                {
                    if (match.RecPlayer is not null)
                    {
                        RecordPlayerName = match.RecPlayer.Name;
                    }
                    var task = match.UpdateAsync();
                    task.Wait();

                    if (RecordPlayerName is not null)
                    {
                        match.RecPlayerName = RecordPlayerName;
                    }

                    int kill = match.HostTotalKill, death = match.HostTotalDeath;

                    match.RefreshKDs();

                    if (task.Result || kill != match.HostTotalKill || death != match.HostTotalDeath)
                    {
                        MatchChanged?.Invoke(match);
                    }
                }
            UpdateDone = true;
            UpdateFinished?.Invoke();
        }
    }
}
