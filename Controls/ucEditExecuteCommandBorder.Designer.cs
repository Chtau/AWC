namespace AWC.Controls
{
    partial class ucEditExecuteCommandBorder
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
            this.rbtnBorderless = new System.Windows.Forms.RadioButton();
            this.rbtnThinLineBorder = new System.Windows.Forms.RadioButton();
            this.rbtnSizingBorder = new System.Windows.Forms.RadioButton();
            this.rbtnOverlappedBorder = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbtnBorderless
            // 
            this.rbtnBorderless.AutoSize = true;
            this.rbtnBorderless.Location = new System.Drawing.Point(26, 19);
            this.rbtnBorderless.Name = "rbtnBorderless";
            this.rbtnBorderless.Size = new System.Drawing.Size(73, 17);
            this.rbtnBorderless.TabIndex = 0;
            this.rbtnBorderless.TabStop = true;
            this.rbtnBorderless.Text = "borderless";
            this.rbtnBorderless.UseVisualStyleBackColor = true;
            // 
            // rbtnThinLineBorder
            // 
            this.rbtnThinLineBorder.AutoSize = true;
            this.rbtnThinLineBorder.Location = new System.Drawing.Point(26, 51);
            this.rbtnThinLineBorder.Name = "rbtnThinLineBorder";
            this.rbtnThinLineBorder.Size = new System.Drawing.Size(95, 17);
            this.rbtnThinLineBorder.TabIndex = 1;
            this.rbtnThinLineBorder.TabStop = true;
            this.rbtnThinLineBorder.Text = "thin-line Border";
            this.rbtnThinLineBorder.UseVisualStyleBackColor = true;
            // 
            // rbtnSizingBorder
            // 
            this.rbtnSizingBorder.AutoSize = true;
            this.rbtnSizingBorder.Location = new System.Drawing.Point(26, 84);
            this.rbtnSizingBorder.Name = "rbtnSizingBorder";
            this.rbtnSizingBorder.Size = new System.Drawing.Size(85, 17);
            this.rbtnSizingBorder.TabIndex = 2;
            this.rbtnSizingBorder.TabStop = true;
            this.rbtnSizingBorder.Text = "sizing Border";
            this.rbtnSizingBorder.UseVisualStyleBackColor = true;
            // 
            // rbtnOverlappedBorder
            // 
            this.rbtnOverlappedBorder.AutoSize = true;
            this.rbtnOverlappedBorder.Location = new System.Drawing.Point(26, 116);
            this.rbtnOverlappedBorder.Name = "rbtnOverlappedBorder";
            this.rbtnOverlappedBorder.Size = new System.Drawing.Size(114, 17);
            this.rbtnOverlappedBorder.TabIndex = 3;
            this.rbtnOverlappedBorder.TabStop = true;
            this.rbtnOverlappedBorder.Text = "Overlapped Border";
            this.rbtnOverlappedBorder.UseVisualStyleBackColor = true;
            // 
            // ucEditExecuteCommandBorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbtnOverlappedBorder);
            this.Controls.Add(this.rbtnSizingBorder);
            this.Controls.Add(this.rbtnThinLineBorder);
            this.Controls.Add(this.rbtnBorderless);
            this.Name = "ucEditExecuteCommandBorder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtnBorderless;
        private System.Windows.Forms.RadioButton rbtnThinLineBorder;
        private System.Windows.Forms.RadioButton rbtnSizingBorder;
        private System.Windows.Forms.RadioButton rbtnOverlappedBorder;
    }
}
