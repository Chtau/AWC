namespace AWC.Controls
{
    partial class ucEditExecuteCommandCommand
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
            this.lblApplication = new System.Windows.Forms.Label();
            this.txtApplication = new System.Windows.Forms.TextBox();
            this.txtParameter = new System.Windows.Forms.TextBox();
            this.lblParams = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblApplication
            // 
            this.lblApplication.AutoSize = true;
            this.lblApplication.Location = new System.Drawing.Point(20, 14);
            this.lblApplication.Name = "lblApplication";
            this.lblApplication.Size = new System.Drawing.Size(59, 13);
            this.lblApplication.TabIndex = 0;
            this.lblApplication.Text = "Application";
            // 
            // txtApplication
            // 
            this.txtApplication.Location = new System.Drawing.Point(23, 39);
            this.txtApplication.Name = "txtApplication";
            this.txtApplication.Size = new System.Drawing.Size(210, 20);
            this.txtApplication.TabIndex = 1;
            // 
            // txtParameter
            // 
            this.txtParameter.Location = new System.Drawing.Point(23, 96);
            this.txtParameter.Name = "txtParameter";
            this.txtParameter.Size = new System.Drawing.Size(210, 20);
            this.txtParameter.TabIndex = 3;
            // 
            // lblParams
            // 
            this.lblParams.AutoSize = true;
            this.lblParams.Location = new System.Drawing.Point(20, 71);
            this.lblParams.Name = "lblParams";
            this.lblParams.Size = new System.Drawing.Size(55, 13);
            this.lblParams.TabIndex = 2;
            this.lblParams.Text = "Parameter";
            // 
            // ucEditExecuteCommandCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtParameter);
            this.Controls.Add(this.lblParams);
            this.Controls.Add(this.txtApplication);
            this.Controls.Add(this.lblApplication);
            this.Name = "ucEditExecuteCommandCommand";
            this.Size = new System.Drawing.Size(256, 153);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblApplication;
        private System.Windows.Forms.TextBox txtApplication;
        private System.Windows.Forms.TextBox txtParameter;
        private System.Windows.Forms.Label lblParams;
    }
}
