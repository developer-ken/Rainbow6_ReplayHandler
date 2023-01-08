namespace Rainbow6_ReplayHandler
{
    public partial class Wait : Form
    {
        public bool Shown = false;
        public Wait()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void Wait_Load(object sender, EventArgs e)
        {
            Shown = true;
        }
    }
}
