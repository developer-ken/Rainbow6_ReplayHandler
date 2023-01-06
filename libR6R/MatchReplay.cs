
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;

namespace libR6R
{
    public class MatchReplay
    {
        private static bool registered = false;
        public static MatchReplay ReadFromFile(string filepath)
        {
            var jb = RawRec2Json(filepath);
            return null;
        }

        public static async Task<MatchReplay> ReadFromFileAsync(string filepath)
        {
            var jb = await RawRec2JsonAsync(filepath);
            return null;
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
            return new Task<JObject>(() => RawRec2Json(filepath));
        }
    }
}