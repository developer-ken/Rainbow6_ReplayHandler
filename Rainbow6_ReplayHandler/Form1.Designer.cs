namespace Rainbow6_ReplayHandler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.SavesRckickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToGameCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.详细信息DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.InGameRclickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.savePermanentlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unSelectUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasklistErrHandler = new System.Windows.Forms.Timer(this.components);
            this.gamefswatcher = new System.IO.FileSystemWatcher();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SavesRckickMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.InGameRclickMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamefswatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // textBox3
            // 
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            // 
            // listBox2
            // 
            resources.ApplyResources(this.listBox2, "listBox2");
            this.listBox2.ContextMenuStrip = this.SavesRckickMenu;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // SavesRckickMenu
            // 
            resources.ApplyResources(this.SavesRckickMenu, "SavesRckickMenu");
            this.SavesRckickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToGameCToolStripMenuItem,
            this.详细信息DToolStripMenuItem,
            this.exportEToolStripMenuItem,
            this.removeRToolStripMenuItem});
            this.SavesRckickMenu.Name = "SavesRckickMenu";
            this.SavesRckickMenu.Opening += new System.ComponentModel.CancelEventHandler(this.SavesRckickMenu_Opening);
            // 
            // copyToGameCToolStripMenuItem
            // 
            resources.ApplyResources(this.copyToGameCToolStripMenuItem, "copyToGameCToolStripMenuItem");
            this.copyToGameCToolStripMenuItem.Name = "copyToGameCToolStripMenuItem";
            this.copyToGameCToolStripMenuItem.Click += new System.EventHandler(this.copyToGameCToolStripMenuItem_Click);
            // 
            // 详细信息DToolStripMenuItem
            // 
            resources.ApplyResources(this.详细信息DToolStripMenuItem, "详细信息DToolStripMenuItem");
            this.详细信息DToolStripMenuItem.Name = "详细信息DToolStripMenuItem";
            // 
            // exportEToolStripMenuItem
            // 
            resources.ApplyResources(this.exportEToolStripMenuItem, "exportEToolStripMenuItem");
            this.exportEToolStripMenuItem.Name = "exportEToolStripMenuItem";
            this.exportEToolStripMenuItem.Click += new System.EventHandler(this.exportEToolStripMenuItem_Click);
            // 
            // removeRToolStripMenuItem
            // 
            resources.ApplyResources(this.removeRToolStripMenuItem, "removeRToolStripMenuItem");
            this.removeRToolStripMenuItem.Name = "removeRToolStripMenuItem";
            this.removeRToolStripMenuItem.Click += new System.EventHandler(this.removeRToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // listBox1
            // 
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.ContextMenuStrip = this.InGameRclickMenu;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // InGameRclickMenu
            // 
            resources.ApplyResources(this.InGameRclickMenu, "InGameRclickMenu");
            this.InGameRclickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savePermanentlyToolStripMenuItem,
            this.unSelectUToolStripMenuItem,
            this.openDirectoryOToolStripMenuItem,
            this.detailsDToolStripMenuItem,
            this.导出EToolStripMenuItem,
            this.deleteDToolStripMenuItem});
            this.InGameRclickMenu.Name = "InGameRclickMenu";
            this.InGameRclickMenu.Opening += new System.ComponentModel.CancelEventHandler(this.InGameRclickMenu_Opening);
            // 
            // savePermanentlyToolStripMenuItem
            // 
            resources.ApplyResources(this.savePermanentlyToolStripMenuItem, "savePermanentlyToolStripMenuItem");
            this.savePermanentlyToolStripMenuItem.Name = "savePermanentlyToolStripMenuItem";
            this.savePermanentlyToolStripMenuItem.Click += new System.EventHandler(this.savePermanentlyToolStripMenuItem_Click);
            // 
            // unSelectUToolStripMenuItem
            // 
            resources.ApplyResources(this.unSelectUToolStripMenuItem, "unSelectUToolStripMenuItem");
            this.unSelectUToolStripMenuItem.Name = "unSelectUToolStripMenuItem";
            this.unSelectUToolStripMenuItem.Click += new System.EventHandler(this.moveToSaveUToolStripMenuItem_Click);
            // 
            // openDirectoryOToolStripMenuItem
            // 
            resources.ApplyResources(this.openDirectoryOToolStripMenuItem, "openDirectoryOToolStripMenuItem");
            this.openDirectoryOToolStripMenuItem.Name = "openDirectoryOToolStripMenuItem";
            this.openDirectoryOToolStripMenuItem.Click += new System.EventHandler(this.openDirectoryOToolStripMenuItem_Click);
            // 
            // detailsDToolStripMenuItem
            // 
            resources.ApplyResources(this.detailsDToolStripMenuItem, "detailsDToolStripMenuItem");
            this.detailsDToolStripMenuItem.Name = "detailsDToolStripMenuItem";
            // 
            // 导出EToolStripMenuItem
            // 
            resources.ApplyResources(this.导出EToolStripMenuItem, "导出EToolStripMenuItem");
            this.导出EToolStripMenuItem.Name = "导出EToolStripMenuItem";
            this.导出EToolStripMenuItem.Click += new System.EventHandler(this.导出EToolStripMenuItem_Click);
            // 
            // deleteDToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteDToolStripMenuItem, "deleteDToolStripMenuItem");
            this.deleteDToolStripMenuItem.Name = "deleteDToolStripMenuItem";
            this.deleteDToolStripMenuItem.Click += new System.EventHandler(this.deleteDToolStripMenuItem_Click);
            // 
            // tasklistErrHandler
            // 
            this.tasklistErrHandler.Enabled = true;
            this.tasklistErrHandler.Interval = 500;
            this.tasklistErrHandler.Tick += new System.EventHandler(this.tasklistErrHandler_Tick);
            // 
            // gamefswatcher
            // 
            this.gamefswatcher.EnableRaisingEvents = true;
            this.gamefswatcher.IncludeSubdirectories = true;
            this.gamefswatcher.NotifyFilter = ((System.IO.NotifyFilters)((System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.DirectoryName)));
            this.gamefswatcher.SynchronizingObject = this;
            this.gamefswatcher.Created += new System.IO.FileSystemEventHandler(this.gamefswatcher_Created);
            this.gamefswatcher.Deleted += new System.IO.FileSystemEventHandler(this.gamefswatcher_Deleted);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "r6r.zip";
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            this.saveFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.SavesRckickMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.InGameRclickMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gamefswatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GroupBox groupBox2;
        private ListBox listBox2;
        private GroupBox groupBox1;
        private ListBox listBox1;
        private System.Windows.Forms.Timer tasklistErrHandler;
        private FileSystemWatcher gamefswatcher;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private ContextMenuStrip InGameRclickMenu;
        private ToolStripMenuItem savePermanentlyToolStripMenuItem;
        private ToolStripMenuItem deleteDToolStripMenuItem;
        private ToolStripMenuItem detailsDToolStripMenuItem;
        private ToolStripMenuItem openDirectoryOToolStripMenuItem;
        private ToolStripMenuItem unSelectUToolStripMenuItem;
        private ContextMenuStrip SavesRckickMenu;
        private ToolStripMenuItem copyToGameCToolStripMenuItem;
        private ToolStripMenuItem removeRToolStripMenuItem;
        private ToolStripMenuItem exportEToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem 详细信息DToolStripMenuItem;
        private ToolStripMenuItem 导出EToolStripMenuItem;
        private Label label1;
    }
}