namespace AWC.Controls
{
    partial class ucEditExecuteCommandPosition
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblXCor = new System.Windows.Forms.Label();
            this.txtXCor = new System.Windows.Forms.TextBox();
            this.txtYCor = new System.Windows.Forms.TextBox();
            this.lblYCor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblXCor
            // 
            this.lblXCor.AutoSize = true;
            this.lblXCor.Location = new System.Drawing.Point(17, 15);
            this.lblXCor.Name = "lblXCor";
            this.lblXCor.Size = new System.Drawing.Size(17, 13);
            this.lblXCor.TabIndex = 0;
            this.lblXCor.Text = "X:";
            // 
            // txtXCor
            // 
            this.txtXCor.Location = new System.Drawing.Point(85, 12);
            this.txtXCor.Name = "txtXCor";
            this.txtXCor.Size = new System.Drawing.Size(100, 20);
            this.txtXCor.TabIndex = 1;
            // 
            // txtYCor
            // 
            this.txtYCor.Location = new System.Drawing.Point(85, 47);
            this.txtYCor.Name = "txtYCor";
            this.txtYCor.Size = new System.Drawing.Size(100, 20);
            this.txtYCor.TabIndex = 3;
            // 
            // lblYCor
            // 
            this.lblYCor.AutoSize = true;
            this.lblYCor.Location = new System.Drawing.Point(17, 50);
            this.lblYCor.Name = "lblYCor";
            this.lblYCor.Size = new System.Drawing.Size(17, 13);
            this.lblYCor.TabIndex = 2;
            this.lblYCor.Text = "Y:";
            // 
            // ucEditExecuteCommandPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtYCor);
            this.Controls.Add(this.lblYCor);
            this.Controls.Add(this.txtXCor);
            this.Controls.Add(this.lblXCor);
            this.Name = "ucEditExecuteCommandPosition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblXCor;
        private System.Windows.Forms.TextBox txtXCor;
        private System.Windows.Forms.TextBox txtYCor;
        private System.Windows.Forms.Label lblYCor;
    }
}
