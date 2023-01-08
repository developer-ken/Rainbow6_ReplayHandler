using System.Diagnostics;

namespace libR6R
{
    public static class PathFinder
    {
        public static string ByRunnningGame()
        {
            var proc = Process.GetProcessesByName("RainbowSix_BE");
            if (proc.Length == 0)
            {
                throw new Exception("No RainbowSix instance is runnning!");
            }
            var directory = Path.GetDirectoryName(proc[0].MainModule.FileName);
            return Path.Combine(directory, "MatchReplay");
        }
    }
}
