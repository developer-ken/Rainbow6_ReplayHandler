using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libR6R
{
    public class MatchManager
    {
        string _savepath;
        public List<MatchReplay> Replays = new List<MatchReplay>();

        public delegate void MatchListChange(MatchReplay match);
        public event MatchListChange NewMatchFound;
        public event MatchListChange MatchRemoved;

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
                    Replays.Add(match);
                    NewMatchFound?.Invoke(match);
                }
                else
                    throw new InvalidOperationException("Match UUID collision detected.");
            }

            {
                List<MatchReplay> toBremoved = new List<MatchReplay>();
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
    }
}
