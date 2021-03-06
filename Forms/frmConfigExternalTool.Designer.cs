﻿namespace AWC.Forms
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
            this.tsNavBar = new System.Windows.Forms.ToolStrip();
            this.tsbtnSaveConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnAddRow = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRemoveRow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvExternalToolConfig = new System.Windows.Forms.DataGridView();
            this.colEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWindowEventTyp = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colExecuteEventtyp = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colStartCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEditCommand = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tsNavBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExternalToolConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // tsNavBar
            // 
            this.tsNavBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSaveConfig,
            this.toolStripSeparator1,
            this.tsbtnAddRow,
            this.tsbtnRemoveRow,
            this.toolStripSeparator2});
            this.tsNavBar.Location = new System.Drawing.Point(0, 0);
            this.tsNavBar.Name = "tsNavBar";
            this.tsNavBar.Size = new System.Drawing.Size(644, 25);
            this.tsNavBar.TabIndex = 0;
            this.tsNavBar.Text = "toolStrip1";
            // 
            // tsbtnSaveConfig
            // 
            this.tsbtnSaveConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSaveConfig.Image = global::AWC.Res.Icons.save_icon_24;
            this.tsbtnSaveConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSaveConfig.Name = "tsbtnSaveConfig";
            this.tsbtnSaveConfig.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSaveConfig.Text = "Save";
            this.tsbtnSaveConfig.Click += new System.EventHandler(this.tsbtnSaveConfig_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnAddRow
            // 
            this.tsbtnAddRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddRow.Image = global::AWC.Res.Icons.add_green_24;
            this.tsbtnAddRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddRow.Name = "tsbtnAddRow";
            this.tsbtnAddRow.Size = new System.Drawing.Size(23, 22);
            this.tsbtnAddRow.Text = "add Row";
            this.tsbtnAddRow.Click += new System.EventHandler(this.tsbtnAddRow_Click);
            // 
            // tsbtnRemoveRow
            // 
            this.tsbtnRemoveRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRemoveRow.Image = global::AWC.Res.Icons.remove_red_24;
            this.tsbtnRemoveRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRemoveRow.Name = "tsbtnRemoveRow";
            this.tsbtnRemoveRow.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRemoveRow.Text = "remove Row";
            this.tsbtnRemoveRow.Click += new System.EventHandler(this.tsbtnRemoveRow_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // dgvExternalToolConfig
            // 
            this.dgvExternalToolConfig.AllowUserToResizeRows = false;
            this.dgvExternalToolConfig.BackgroundColor = System.Drawing.Color.White;
            this.dgvExternalToolConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExternalToolConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEnable,
            this.colProcessName,
            this.colWindowEventTyp,
            this.colExecuteEventtyp,
            this.colStartCommand,
            this.colEditCommand});
            this.dgvExternalToolConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExternalToolConfig.Location = new System.Drawing.Point(0, 25);
            this.dgvExternalToolConfig.Name = "dgvExternalToolConfig";
            this.dgvExternalToolConfig.RowHeadersWidth = 35;
            this.dgvExternalToolConfig.ShowCellErrors = false;
            this.dgvExternalToolConfig.Size = new System.Drawing.Size(644, 279);
            this.dgvExternalToolConfig.TabIndex = 1;
            this.dgvExternalToolConfig.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExternalToolConfig_CellContentClick);
            this.dgvExternalToolConfig.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvExternalToolConfig_CellPainting);
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
            // colWindowEventTyp
            // 
            this.colWindowEventTyp.HeaderText = "Window Eventtyp";
            this.colWindowEventTyp.Items.AddRange(new object[] {
            "Start",
            "End",
            "Basicdata",
            "Title",
            "Style",
            "PositionSize",
            "ExStyle",
            "Datatext"});
            this.colWindowEventTyp.Name = "colWindowEventTyp";
            // 
            // colExecuteEventtyp
            // 
            this.colExecuteEventtyp.HeaderText = "Execute Eventtyp";
            this.colExecuteEventtyp.Items.AddRange(new object[] {
            "Command",
            "Position",
            "Size",
            "Border"});
            this.colExecuteEventtyp.Name = "colExecuteEventtyp";
            // 
            // colStartCommand
            // 
            this.colStartCommand.FillWeight = 188F;
            this.colStartCommand.HeaderText = "Command";
            this.colStartCommand.Name = "colStartCommand";
            this.colStartCommand.Width = 188;
            // 
            // colEditCommand
            // 
            this.colEditCommand.FillWeight = 22F;
            this.colEditCommand.HeaderText = "";
            this.colEditCommand.Name = "colEditCommand";
            this.colEditCommand.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colEditCommand.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colEditCommand.Width = 22;
            // 
            // frmConfigExternalTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 304);
            this.Controls.Add(this.dgvExternalToolConfig);
            this.Controls.Add(this.tsNavBar);
            this.Icon = global::AWC.Res.Icons.system_config_dark_64_Icon;
            this.Name = "frmConfigExternalTool";
            this.Text = "External Tool Configuration";
            this.tsNavBar.ResumeLayout(false);
            this.tsNavBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExternalToolConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsNavBar;
        private System.Windows.Forms.DataGridView dgvExternalToolConfig;
        private System.Windows.Forms.ToolStripButton tsbtnSaveConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnAddRow;
        private System.Windows.Forms.ToolStripButton tsbtnRemoveRow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colWindowEventTyp;
        private System.Windows.Forms.DataGridViewComboBoxColumn colExecuteEventtyp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartCommand;
        private System.Windows.Forms.DataGridViewButtonColumn colEditCommand;
    }
}