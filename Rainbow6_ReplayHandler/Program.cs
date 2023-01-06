using libR6R;

namespace Rainbow6_ReplayHandler
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MatchReplay.ReadFromFile("E:\\Steam\\steamapps\\common\\Tom Clancy's Rainbow Six Siege\\MatchReplay\\Match-2023-01-05_20-22-12-146\\Match-2023-01-05_20-22-12-146-R01.rec");
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}