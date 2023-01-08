using libR6R;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            foreach(var round in Match.Rounds)
            {
                var round_number = round.Key;
                var recplayer = round.Value.RecPlayer;
                var round_kills = recplayer.Kills;
            }
        }
    }
}
