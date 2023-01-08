using libR6R;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography.Pkcs;

namespace Rainbow6_ReplayHandler
{
    public partial class Form1 : Form
    {
        const string SavePath = "GameReplay";
        string GameSavePath;
        MatchManager InGame, Saved;
        List<Task> UnImportantTasks = new List<Task>();

        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            if (File.Exists("conf.json"))
            {
                JObject jb = JObject.Parse(File.ReadAllText("conf.json"));
                if (Directory.Exists(jb["GameSavePath"]?.ToString()))
                {
                    GameSavePath = jb["GameSavePath"].ToString();
                }
                else
                {
                    while (true)
                        try
                        {
                            GameSavePath = PathFinder.ByRunnningGame();
                            break;
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "No RainbowSix instance is runnning!")
                                MessageBox.Show(language.GAME_INSTANCE_NOT_FOUND, language.GAME_INSTANCE_NOT_FOUND_TITLE);
                        }
                }
            }
            else
            {
                while (true)
                    try
                    {
                        GameSavePath = PathFinder.ByRunnningGame();
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "No RainbowSix instance is runnning!")
                            MessageBox.Show(language.GAME_INSTANCE_NOT_FOUND, language.GAME_INSTANCE_NOT_FOUND_TITLE);
                    }
            }
            {
                JObject jb = new JObject();
                jb.Add("GameSavePath", GameSavePath);
                File.WriteAllText("conf.json", jb.ToString());
            }

            InGame = new MatchManager(GameSavePath);
            Saved = new MatchManager(SavePath);
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            InvokeBgTask(InGame.UpdateAsync);
        }

        bool ErrhanLock = false;

        private void InvokeBgTask(Func<Task> action)
        {
            Task.Run(() =>
            {
                var task = action.Invoke();
                lock (UnImportantTasks)
                {
                    UnImportantTasks.Add(task);
                }
            });
        }
        private void InvokeBgTask(Action action)
        {
            Task.Run(() =>
            {
                lock (UnImportantTasks)
                {
                    UnImportantTasks.Add(Task.Run(action));
                }
            });
        }

        private void tasklistErrHandler_Tick(object sender, EventArgs e)
        {
            List<Task> task_finished = new List<Task>();
            if (ErrhanLock) return;
            ErrhanLock = true;
            lock (UnImportantTasks)
            {
                foreach (var task in UnImportantTasks)
                {
                    try
                    {
                        if (task.IsCompleted || task.IsFaulted)
                        {
                            task_finished.Add(task);
                            task.Wait();
                        }
                    }
                    catch (Exception err)
                    {
                        if (err.InnerException is null)
                            MessageBox.Show(language.TASK_EXCEPTION + "\n" + err.Message + "\n" + err.StackTrace, language.TASK_EXCEPTION_TITLE);
                        else
                            MessageBox.Show(language.TASK_EXCEPTION + "\n" + err.InnerException.Message + "\n" + err.InnerException.StackTrace, language.TASK_EXCEPTION_TITLE);
                    }
                }
                foreach (var task in task_finished)
                {
                    UnImportantTasks.Remove(task);
                }
            }
            ErrhanLock = false;
        }

        private void OnNewInGameSaveDetected(MatchReplay r)
        {
            listBox1.Items.Add(ReplayToString(r));
        }

        private void OnNewSavedSaveDetected(MatchReplay r)
        {
            listBox2.Items.Add(ReplayToString(r));
        }

        private void OnInGameSaveRemoved(MatchReplay r)
        {
            listBox1.Items.Remove(ReplayToString(r));
        }

        private void OnSavedSaveRemoved(MatchReplay r)
        {
            listBox2.Items.Remove(ReplayToString(r));
        }

        private string ReplayToString(MatchReplay match)
        {
            string gamemode, matchtype, map = "";
            switch (match.GameMode)
            {
                case GameMode.BOMB:
                    gamemode = language.GAMEMODE_BOMB;
                    break;
                case GameMode.HOSTAGE:
                    gamemode = language.GAMEMODE_HOSTAGE;
                    break;
                case GameMode.SECURE_AREA:
                    gamemode = language.GAMEMODE_AREA;
                    break;
                case GameMode.UnKnown:
                    gamemode = language.GAMEMODE_UNKNOWN;
                    break;
                default:
                    gamemode = language.GAMEMODE_UNKNOWN;
                    break;
            }
            switch (match.MatchType)
            {
                case libR6R.MatchType.QuickMatch:
                    matchtype = language.GAMETYPE_QUICKMATCH;
                    break;
                case libR6R.MatchType.Ranked:
                    matchtype = language.GAMETYPE_RANKED;
                    break;
                case libR6R.MatchType.UnRanked:
                    matchtype = language.GAMETYPE_UNRANKED;
                    break;
                case libR6R.MatchType.UnKnown:
                    matchtype = language.GAMETYPE_UNKNOWN;
                    break;
                case libR6R.MatchType.CUSTOM_LOCAL:
                    matchtype = language.GAMETYPE_CUSTOM_LOCAL;
                    break;
                case libR6R.MatchType.CUSTOM_ONLINE:
                    matchtype = language.GAMETYPE_CUSTOM_ONLINE;
                    break;
                default:
                    matchtype = language.GAMETYPE_UNKNOWN;
                    break;
            }
            switch (match.Map)
            {
                case Map.CLUB_HOUSE:
                    map = language.MAP_CLUB_HOUSE;
                    break;
                case Map.KAFE_DOSTOYEVSKY:
                    map = language.MAP_KAFE_DOSTOYEVSKY;
                    break;
                case Map.KANAL:
                    map = language.MAP_KANAL;
                    break;
                case Map.YACHT:
                    map = language.MAP_YACHT;
                    break;
                case Map.PRESIDENTIAL_PLANE:
                    map = language.MAP_PRESIDENTIAL_PLANE;
                    break;
                case Map.CONSULATE:
                    map = language.MAP_CONSULATE;
                    break;
                case Map.BARTLETT_U:
                    map = language.MAP_BARTLETT_U;
                    break;
                case Map.COASTLINE:
                    map = language.MAP_COASTLINE;
                    break;
                case Map.TOWER:
                    map = language.MAP_TOWER;
                    break;
                case Map.VILLA:
                    map = language.MAP_VILLA;
                    break;
                case Map.FORTRESS:
                    map = language.MAP_FORTRESS;
                    break;
                case Map.HEREFORD_BASE:
                    map = language.MAP_HEREFORD_BASE;
                    break;
                case Map.THEME_PARK:
                    map = language.MAP_THEME_PARK;
                    break;
                case Map.OREGON:
                    map = language.MAP_OREGON;
                    break;
                case Map.HOUSE:
                    map = language.MAP_HOUSE;
                    break;
                case Map.CHALET:
                    map = language.MAP_CHALET;
                    break;
                case Map.SKYSCRAPER:
                    map = language.MAP_SKYSCRAPER;
                    break;
                case Map.BORDER:
                    map = language.MAP_BORDER;
                    break;
                case Map.FAVELA:
                    map = language.MAP_FAVELA;
                    break;
                case Map.BANK:
                    map = language.MAP_BANK;
                    break;
                case Map.OUTBACK:
                    map = language.MAP_OUTBACK;
                    break;
                case Map.EMERALD_PLAINS:
                    map = language.MAP_EMERALD_PLAINS;
                    break;
                case Map.STADIUM_BRAVO:
                    map = language.MAP_STADIUM_BRAVO;
                    break;
                case Map.NIGHTHAVEN_LABS:
                    map = language.MAP_NIGHTHAVEN_LABS;
                    break;
                default:
                    map = language.MAP_UNKNOWN;
                    break;
            }

            return matchtype + "\t" + map + "\t" + gamemode + "\t" + match.HostTotalKill + "/" + match.HostTotalDeath + "\t" + match.MatchTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private string ReplayToDirname(MatchReplay match)
        {
            string gamemode, matchtype, map = "";
            switch (match.GameMode)
            {
                case GameMode.BOMB:
                    gamemode = language.GAMEMODE_BOMB;
                    break;
                case GameMode.HOSTAGE:
                    gamemode = language.GAMEMODE_HOSTAGE;
                    break;
                case GameMode.SECURE_AREA:
                    gamemode = language.GAMEMODE_AREA;
                    break;
                case GameMode.UnKnown:
                    gamemode = language.GAMEMODE_UNKNOWN;
                    break;
                default:
                    gamemode = language.GAMEMODE_UNKNOWN;
                    break;
            }
            switch (match.MatchType)
            {
                case libR6R.MatchType.QuickMatch:
                    matchtype = language.GAMETYPE_QUICKMATCH;
                    break;
                case libR6R.MatchType.Ranked:
                    matchtype = language.GAMETYPE_RANKED;
                    break;
                case libR6R.MatchType.UnRanked:
                    matchtype = language.GAMETYPE_UNRANKED;
                    break;
                case libR6R.MatchType.UnKnown:
                    matchtype = language.GAMETYPE_UNKNOWN;
                    break;
                case libR6R.MatchType.CUSTOM_LOCAL:
                    matchtype = language.GAMETYPE_CUSTOM_LOCAL;
                    break;
                case libR6R.MatchType.CUSTOM_ONLINE:
                    matchtype = language.GAMETYPE_CUSTOM_ONLINE;
                    break;
                default:
                    matchtype = language.GAMETYPE_UNKNOWN;
                    break;
            }
            switch (match.Map)
            {
                case Map.CLUB_HOUSE:
                    map = language.MAP_CLUB_HOUSE;
                    break;
                case Map.KAFE_DOSTOYEVSKY:
                    map = language.MAP_KAFE_DOSTOYEVSKY;
                    break;
                case Map.KANAL:
                    map = language.MAP_KANAL;
                    break;
                case Map.YACHT:
                    map = language.MAP_YACHT;
                    break;
                case Map.PRESIDENTIAL_PLANE:
                    map = language.MAP_PRESIDENTIAL_PLANE;
                    break;
                case Map.CONSULATE:
                    map = language.MAP_CONSULATE;
                    break;
                case Map.BARTLETT_U:
                    map = language.MAP_BARTLETT_U;
                    break;
                case Map.COASTLINE:
                    map = language.MAP_COASTLINE;
                    break;
                case Map.TOWER:
                    map = language.MAP_TOWER;
                    break;
                case Map.VILLA:
                    map = language.MAP_VILLA;
                    break;
                case Map.FORTRESS:
                    map = language.MAP_FORTRESS;
                    break;
                case Map.HEREFORD_BASE:
                    map = language.MAP_HEREFORD_BASE;
                    break;
                case Map.THEME_PARK:
                    map = language.MAP_THEME_PARK;
                    break;
                case Map.OREGON:
                    map = language.MAP_OREGON;
                    break;
                case Map.HOUSE:
                    map = language.MAP_HOUSE;
                    break;
                case Map.CHALET:
                    map = language.MAP_CHALET;
                    break;
                case Map.SKYSCRAPER:
                    map = language.MAP_SKYSCRAPER;
                    break;
                case Map.BORDER:
                    map = language.MAP_BORDER;
                    break;
                case Map.FAVELA:
                    map = language.MAP_FAVELA;
                    break;
                case Map.BANK:
                    map = language.MAP_BANK;
                    break;
                case Map.OUTBACK:
                    map = language.MAP_OUTBACK;
                    break;
                case Map.EMERALD_PLAINS:
                    map = language.MAP_EMERALD_PLAINS;
                    break;
                case Map.STADIUM_BRAVO:
                    map = language.MAP_STADIUM_BRAVO;
                    break;
                case Map.NIGHTHAVEN_LABS:
                    map = language.MAP_NIGHTHAVEN_LABS;
                    break;
                default:
                    map = language.MAP_UNKNOWN;
                    break;
            }

            return matchtype + "_" + map + "_" + gamemode + "_" + match.MatchTime.ToString("yyyy-MM-dd_HH.mm.ss");
        }

        private MatchReplay[] GetSelectedReplays(ListBox view, MatchManager man)
        {
            var result = new MatchReplay[view.SelectedItems.Count];
            int i = 0;
            foreach (var item in view.SelectedIndices)
            {
                result[i++] = man.Replays[(int)item];
            }
            return result;
        }
        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                if (File.Exists(targetFilePath)) continue;
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        private void savePermanentlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InGameCopyToSave();
        }

        private void InGameCopyToSave()
        {
            var r = GetSelectedReplays(listBox1, InGame);
            SyncTask(() =>
            {
                foreach (var replay in r)
                {
                    CopyDirectory(replay.DirPath, Path.Combine(SavePath, ReplayToDirname(replay)), true);
                }
                Saved.UpdateAsync().Wait();
            });
        }
        private void SaveCopyToInGame()
        {
            var r = GetSelectedReplays(listBox2, Saved);
            SyncTask(() =>
            {
                foreach (var replay in r)
                {
                    CopyDirectory(replay.DirPath, Path.Combine(GameSavePath, replay.GetCorrectSaveDirName()), true);
                }
                InGame.UpdateAsync().Wait();
            });
        }

        private void DeleteSelected(ListBox view, MatchManager man)
        {
            var r = GetSelectedReplays(view, man);
            foreach (var replay in r)
            {
                Directory.Delete(replay.DirPath, true);
            }
            InvokeBgTask(man.UpdateAsync);
        }

        private void OpenSelectdFolder(ListBox view, MatchManager man)
        {
            var r = GetSelectedReplays(view, man);
            foreach (var replay in r)
            {
                Process.Start("explorer.exe", replay.DirPath);
            }
        }

        private void SyncTask(Action action)
        {
            var wait = new Wait();
            InvokeBgTask(() =>
            {
                action.Invoke();
                while (!wait.Shown) Thread.Sleep(0);
                wait.Close();
            }
            );
            wait.ShowDialog();
        }

        private void InGameRclickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0) e.Cancel = true;
        }

        private void SavesRckickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (listBox2.SelectedItems.Count == 0) e.Cancel = true;
        }

        private void copyToGameCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCopyToInGame();
        }

        private void moveToSaveUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InGameCopyToSave();
            DeleteSelected(listBox1, InGame);
        }

        private void deleteDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelected(listBox1, InGame);
        }

        private void openDirectoryOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectdFolder(listBox1, InGame);
        }

        private void removeRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelected(listBox2, Saved);
        }

        private void gamefswatcher_Changed(object sender, FileSystemEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(SavePath);
            InvokeBgTask(InGame.UpdateAsync);
            InvokeBgTask(Saved.UpdateAsync);
            InGame.NewMatchFound += OnNewInGameSaveDetected;
            InGame.MatchRemoved += OnInGameSaveRemoved;

            Saved.NewMatchFound += OnNewSavedSaveDetected;
            Saved.MatchRemoved += OnSavedSaveRemoved;
        }
    }
}