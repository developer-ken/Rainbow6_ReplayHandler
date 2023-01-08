using libR6R;

namespace Rainbow6_ReplayHandler
{
    public partial class MatchDetails : Form
    {
        MatchReplay Match;
        public MatchDetails(MatchReplay match)
        {
            InitializeComponent();
            Match = match;
        }

        private void MatchDetails_Load(object sender, EventArgs e)
        {
            foreach (var round in Match.Rounds)
            {
                var round_number = round.Key;
                var recplayer = round.Value.RecPlayer;
                var round_kills = recplayer.Kills;
            }
        }
    }
}
