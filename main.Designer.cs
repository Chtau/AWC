namespace AWC
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.niMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.showDebugWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeLogToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processCheckerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbDebugText = new System.Windows.Forms.GroupBox();
            this.rtbDebugText = new System.Windows.Forms.RichTextBox();
            this.ssInfo = new System.Windows.Forms.StatusStrip();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.configCheckerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMain.SuspendLayout();
            this.gbDebugText.SuspendLayout();
            this.SuspendLayout();
            // 
            // niMain
            // 
            this.niMain.ContextMenuStrip = this.cmsMain;
            this.niMain.Icon = ((System.Drawing.Icon)(resources.GetObject("niMain.Icon")));
            this.niMain.Text = "AWC";
            this.niMain.Visible = true;
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClose,
            this.showDebugWindowToolStripMenuItem,
            this.windowStyleToolStripMenuItem,
            this.writeLogToFileToolStripMenuItem,
            this.processCheckerToolStripMenuItem,
            this.configCheckerToolStripMenuItem});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(189, 158);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.Size = new System.Drawing.Size(188, 22);
            this.tsmiClose.Text = "Close";
            this.tsmiClose.Image = AWC.Res.Icons.close_red_24;
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // showDebugWindowToolStripMenuItem
            // 
            this.showDebugWindowToolStripMenuItem.CheckOnClick = true;
            this.showDebugWindowToolStripMenuItem.Name = "showDebugWindowToolStripMenuItem";
            this.showDebugWindowToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.showDebugWindowToolStripMenuItem.Text = "Show Debug Window";
            this.showDebugWindowToolStripMenuItem.Click += new System.EventHandler(this.showDebugWindowToolStripMenuItem_Click);
            // 
            // windowStyleToolStripMenuItem
            // 
            this.windowStyleToolStripMenuItem.Name = "windowStyleToolStripMenuItem";
            this.windowStyleToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.windowStyleToolStripMenuItem.Text = "Window Data";
            this.windowStyleToolStripMenuItem.Image = AWC.Res.Icons.window_48;
            this.windowStyleToolStripMenuItem.Click += new System.EventHandler(this.windowStyleToolStripMenuItem_Click);
            // 
            // writeLogToFileToolStripMenuItem
            // 
            this.writeLogToFileToolStripMenuItem.Name = "writeLogToFileToolStripMenuItem";
            this.writeLogToFileToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.writeLogToFileToolStripMenuItem.Text = "Write Log to File";
            this.writeLogToFileToolStripMenuItem.Image = AWC.Res.Icons.writefile_24;
            this.writeLogToFileToolStripMenuItem.Click += new System.EventHandler(this.writeLogToFileToolStripMenuItem_Click);
            // 
            // processCheckerToolStripMenuItem
            // 
            this.processCheckerToolStripMenuItem.CheckOnClick = true;
            this.processCheckerToolStripMenuItem.Name = "processCheckerToolStripMenuItem";
            this.processCheckerToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.processCheckerToolStripMenuItem.Text = "Process checker";
            this.processCheckerToolStripMenuItem.Image = AWC.Res.Icons.checker_green_24;
            this.processCheckerToolStripMenuItem.Click += new System.EventHandler(this.processCheckerToolStripMenuItem_Click);
            // 
            // gbDebugText
            // 
            this.gbDebugText.Controls.Add(this.rtbDebugText);
            this.gbDebugText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDebugText.Location = new System.Drawing.Point(0, 0);
            this.gbDebugText.Name = "gbDebugText";
            this.gbDebugText.Size = new System.Drawing.Size(532, 248);
            this.gbDebugText.TabIndex = 1;
            this.gbDebugText.TabStop = false;
            this.gbDebugText.Text = "Debug Text";
            // 
            // rtbDebugText
            // 
            this.rtbDebugText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDebugText.Location = new System.Drawing.Point(3, 16);
            this.rtbDebugText.Name = "rtbDebugText";
            this.rtbDebugText.Size = new System.Drawing.Size(526, 229);
            this.rtbDebugText.TabIndex = 0;
            this.rtbDebugText.Text = "";
            // 
            // ssInfo
            // 
            this.ssInfo.Location = new System.Drawing.Point(0, 248);
            this.ssInfo.Name = "ssInfo";
            this.ssInfo.Size = new System.Drawing.Size(532, 22);
            this.ssInfo.TabIndex = 2;
            this.ssInfo.Text = "statusStrip1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "log";
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.Title = "Save Log File";
            // 
            // configCheckerToolStripMenuItem
            // 
            this.configCheckerToolStripMenuItem.Name = "configCheckerToolStripMenuItem";
            this.configCheckerToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.configCheckerToolStripMenuItem.Text = "Config checker";
            this.configCheckerToolStripMenuItem.Image = AWC.Res.Icons.config_dark_24;
            this.configCheckerToolStripMenuItem.Click += new System.EventHandler(this.configCheckerToolStripMenuItem_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 270);
            this.Controls.Add(this.gbDebugText);
            this.Controls.Add(this.ssInfo);
            this.Name = "main";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Debug Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.main_VisibleChanged);
            this.cmsMain.ResumeLayout(false);
            this.gbDebugText.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon niMain;
        private System.Windows.Forms.ContextMenuStrip cmsMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.GroupBox gbDebugText;
        private System.Windows.Forms.RichTextBox rtbDebugText;
        private System.Windows.Forms.StatusStrip ssInfo;
        private System.Windows.Forms.ToolStripMenuItem showDebugWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeLogToFileToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem processCheckerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configCheckerToolStripMenuItem;
    }
}

