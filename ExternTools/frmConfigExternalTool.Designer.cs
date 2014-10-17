namespace AWC.ExternTools
{
    partial class frmConfigExternalTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigExternalTool));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dgvExternalToolConfig = new System.Windows.Forms.DataGridView();
            this.colEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventTyp = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colStartCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsbtnSaveConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExternalToolConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSaveConfig});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(547, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // dgvExternalToolConfig
            // 
            this.dgvExternalToolConfig.AllowUserToResizeRows = false;
            this.dgvExternalToolConfig.BackgroundColor = System.Drawing.Color.White;
            this.dgvExternalToolConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExternalToolConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEnable,
            this.colProcessName,
            this.colEventTyp,
            this.colStartCommand});
            this.dgvExternalToolConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExternalToolConfig.Location = new System.Drawing.Point(0, 25);
            this.dgvExternalToolConfig.Name = "dgvExternalToolConfig";
            this.dgvExternalToolConfig.ShowCellErrors = false;
            this.dgvExternalToolConfig.Size = new System.Drawing.Size(547, 247);
            this.dgvExternalToolConfig.TabIndex = 1;
            // 
            // colEnable
            // 
            this.colEnable.FillWeight = 50F;
            this.colEnable.HeaderText = "Enable";
            this.colEnable.Name = "colEnable";
            this.colEnable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colEnable.Width = 50;
            // 
            // colProcessName
            // 
            this.colProcessName.FillWeight = 120F;
            this.colProcessName.HeaderText = "Process";
            this.colProcessName.Name = "colProcessName";
            this.colProcessName.Width = 120;
            // 
            // colEventTyp
            // 
            this.colEventTyp.HeaderText = "Eventtyp";
            this.colEventTyp.Items.AddRange(new object[] {
            "Start",
            "End",
            "Basicdata",
            "Title",
            "Style",
            "PositionSize",
            "ExStyle",
            "Datatext"});
            this.colEventTyp.Name = "colEventTyp";
            // 
            // colStartCommand
            // 
            this.colStartCommand.FillWeight = 210F;
            this.colStartCommand.HeaderText = "Command";
            this.colStartCommand.Name = "colStartCommand";
            this.colStartCommand.Width = 210;
            // 
            // tsbtnSaveConfig
            // 
            this.tsbtnSaveConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSaveConfig.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSaveConfig.Image")));
            this.tsbtnSaveConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSaveConfig.Name = "tsbtnSaveConfig";
            this.tsbtnSaveConfig.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSaveConfig.Text = "Save";
            this.tsbtnSaveConfig.Click += new System.EventHandler(this.tsbtnSaveConfig_Click);
            // 
            // frmConfigExternalTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 272);
            this.Controls.Add(this.dgvExternalToolConfig);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmConfigExternalTool";
            this.Text = "frmConfigExternalTool";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExternalToolConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgvExternalToolConfig;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colEventTyp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartCommand;
        private System.Windows.Forms.ToolStripButton tsbtnSaveConfig;
    }
}