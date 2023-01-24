using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libR6R
{
    internal class RateLimit
    {
        public static int TotalAsyncCounts
        {
            get
            {
                var cpucount = Environment.ProcessorCount;
                if (cpucount > 4 && cpucount < 10)
                    return cpucount / 2;
                if (cpucount < 4) return 2;
                return 5;
            }
        }
        private static int running = 0;
        public static void GetToken()
        {
            while (running >= TotalAsyncCounts)
                Thread.Sleep(0);
            running++;
        }

        public static void ReleaseToken()
        {
            running--;
        }
    }
}
