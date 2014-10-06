namespace AWC.Forms
{
    partial class frmWindowDataOutput
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
            if (myWindow != null)
            {
                myWindow.Dispose();
            }

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
            this.plFilter = new System.Windows.Forms.Panel();
            this.plOutput = new System.Windows.Forms.Panel();
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.cbWindows = new System.Windows.Forms.ComboBox();
            this.plFilter.SuspendLayout();
            this.plOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // plFilter
            // 
            this.plFilter.Controls.Add(this.cbWindows);
            this.plFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.plFilter.Location = new System.Drawing.Point(0, 0);
            this.plFilter.Name = "plFilter";
            this.plFilter.Size = new System.Drawing.Size(509, 49);
            this.plFilter.TabIndex = 0;
            // 
            // plOutput
            // 
            this.plOutput.Controls.Add(this.rtxtOutput);
            this.plOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plOutput.Location = new System.Drawing.Point(0, 49);
            this.plOutput.Name = "plOutput";
            this.plOutput.Size = new System.Drawing.Size(509, 297);
            this.plOutput.TabIndex = 1;
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.BackColor = System.Drawing.Color.White;
            this.rtxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtOutput.Location = new System.Drawing.Point(0, 0);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.ReadOnly = true;
            this.rtxtOutput.Size = new System.Drawing.Size(509, 297);
            this.rtxtOutput.TabIndex = 0;
            this.rtxtOutput.Text = "";
            // 
            // cbWindows
            // 
            this.cbWindows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWindows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWindows.FormattingEnabled = true;
            this.cbWindows.Location = new System.Drawing.Point(13, 13);
            this.cbWindows.Name = "cbWindows";
            this.cbWindows.Size = new System.Drawing.Size(484, 21);
            this.cbWindows.TabIndex = 0;
            this.cbWindows.SelectedValueChanged += new System.EventHandler(this.cbWindows_SelectedValueChanged);
            // 
            // frmWindowDataOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(509, 346);
            this.Controls.Add(this.plOutput);
            this.Controls.Add(this.plFilter);
            this.Name = "frmWindowDataOutput";
            this.ShowIcon = false;
            this.Text = "Window Data Output";
            this.Load += new System.EventHandler(this.frmWindowDataOutput_Load);
            this.plFilter.ResumeLayout(false);
            this.plOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plFilter;
        private System.Windows.Forms.Panel plOutput;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private System.Windows.Forms.ComboBox cbWindows;
    }
}