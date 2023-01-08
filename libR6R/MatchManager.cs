namespace libR6R
{
    public class MatchManager
    {
        string _savepath;
        public List<MatchReplay> Replays = new List<MatchReplay>();

        public string RecordPlayerName { get; set; }

        public delegate void MatchListChange(MatchReplay match);
        public event MatchListChange NewMatchFound;
        public event MatchListChange MatchRemoved;
        public event MatchListChange MatchChanged;

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

        public static bool IsDirOccupied(string dir)
        {
            if (!Directory.Exists(dir)) return false;
            var flist = Directory.GetFiles(dir);
            foreach (var file in flist)
            {
                if (IsFileOccupied(file)) return true;
            }
            return false;
        }

        public static bool IsFileOccupied(string fname)
        {
            try
            {
                if (!File.Exists(fname)) return false;
                var fs = File.OpenWrite(fname);
                fs.Close();
                fs.Dispose();
                return false;
            }
            catch
            {
                return true;
            }
        }

        public async Task UpdateAsync()
        {
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
        }
    }
}
