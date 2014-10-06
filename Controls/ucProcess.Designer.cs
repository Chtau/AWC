namespace AWC.Controls
{
    partial class ucProcess
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProcessName = new System.Windows.Forms.TextBox();
            this.txtProcessID = new System.Windows.Forms.TextBox();
            this.txtProcessThreads = new System.Windows.Forms.TextBox();
            this.txtProcessStarttime = new System.Windows.Forms.TextBox();
            this.txtProcessSessionID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chbPrcPrioBoost = new System.Windows.Forms.CheckBox();
            this.chbPrcResponding = new System.Windows.Forms.CheckBox();
            this.gbPrcStartParams = new System.Windows.Forms.GroupBox();
            this.txtPrcFilename = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtParams = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.gbPrcStartParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Process Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Process ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Process Threads:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Process Starttime:";
            // 
            // txtProcessName
            // 
            this.txtProcessName.BackColor = System.Drawing.Color.White;
            this.txtProcessName.Location = new System.Drawing.Point(141, 10);
            this.txtProcessName.Name = "txtProcessName";
            this.txtProcessName.ReadOnly = true;
            this.txtProcessName.Size = new System.Drawing.Size(127, 20);
            this.txtProcessName.TabIndex = 4;
            // 
            // txtProcessID
            // 
            this.txtProcessID.BackColor = System.Drawing.Color.White;
            this.txtProcessID.Location = new System.Drawing.Point(141, 35);
            this.txtProcessID.Name = "txtProcessID";
            this.txtProcessID.ReadOnly = true;
            this.txtProcessID.Size = new System.Drawing.Size(127, 20);
            this.txtProcessID.TabIndex = 5;
            // 
            // txtProcessThreads
            // 
            this.txtProcessThreads.BackColor = System.Drawing.Color.White;
            this.txtProcessThreads.Location = new System.Drawing.Point(141, 61);
            this.txtProcessThreads.Name = "txtProcessThreads";
            this.txtProcessThreads.ReadOnly = true;
            this.txtProcessThreads.Size = new System.Drawing.Size(127, 20);
            this.txtProcessThreads.TabIndex = 6;
            // 
            // txtProcessStarttime
            // 
            this.txtProcessStarttime.BackColor = System.Drawing.Color.White;
            this.txtProcessStarttime.Location = new System.Drawing.Point(141, 87);
            this.txtProcessStarttime.Name = "txtProcessStarttime";
            this.txtProcessStarttime.ReadOnly = true;
            this.txtProcessStarttime.Size = new System.Drawing.Size(127, 20);
            this.txtProcessStarttime.TabIndex = 7;
            // 
            // txtProcessSessionID
            // 
            this.txtProcessSessionID.BackColor = System.Drawing.Color.White;
            this.txtProcessSessionID.Location = new System.Drawing.Point(141, 113);
            this.txtProcessSessionID.Name = "txtProcessSessionID";
            this.txtProcessSessionID.ReadOnly = true;
            this.txtProcessSessionID.Size = new System.Drawing.Size(127, 20);
            this.txtProcessSessionID.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Process Session ID:";
            // 
            // chbPrcPrioBoost
            // 
            this.chbPrcPrioBoost.AutoSize = true;
            this.chbPrcPrioBoost.Location = new System.Drawing.Point(18, 141);
            this.chbPrcPrioBoost.Name = "chbPrcPrioBoost";
            this.chbPrcPrioBoost.Size = new System.Drawing.Size(128, 17);
            this.chbPrcPrioBoost.TabIndex = 10;
            this.chbPrcPrioBoost.Text = "Process Priority Boost";
            this.chbPrcPrioBoost.UseVisualStyleBackColor = true;
            // 
            // chbPrcResponding
            // 
            this.chbPrcResponding.AutoSize = true;
            this.chbPrcResponding.BackColor = System.Drawing.Color.Transparent;
            this.chbPrcResponding.Enabled = false;
            this.chbPrcResponding.Location = new System.Drawing.Point(185, 141);
            this.chbPrcResponding.Name = "chbPrcResponding";
            this.chbPrcResponding.Size = new System.Drawing.Size(83, 17);
            this.chbPrcResponding.TabIndex = 11;
            this.chbPrcResponding.Text = "Responding";
            this.chbPrcResponding.UseVisualStyleBackColor = false;
            // 
            // gbPrcStartParams
            // 
            this.gbPrcStartParams.Controls.Add(this.txtDomain);
            this.gbPrcStartParams.Controls.Add(this.label9);
            this.gbPrcStartParams.Controls.Add(this.txtUser);
            this.gbPrcStartParams.Controls.Add(this.label8);
            this.gbPrcStartParams.Controls.Add(this.txtParams);
            this.gbPrcStartParams.Controls.Add(this.label7);
            this.gbPrcStartParams.Controls.Add(this.txtPrcFilename);
            this.gbPrcStartParams.Controls.Add(this.label6);
            this.gbPrcStartParams.Location = new System.Drawing.Point(274, 10);
            this.gbPrcStartParams.Name = "gbPrcStartParams";
            this.gbPrcStartParams.Size = new System.Drawing.Size(262, 145);
            this.gbPrcStartParams.TabIndex = 12;
            this.gbPrcStartParams.TabStop = false;
            this.gbPrcStartParams.Text = "Process Start Infos";
            // 
            // txtPrcFilename
            // 
            this.txtPrcFilename.BackColor = System.Drawing.Color.White;
            this.txtPrcFilename.Location = new System.Drawing.Point(142, 28);
            this.txtPrcFilename.Name = "txtPrcFilename";
            this.txtPrcFilename.ReadOnly = true;
            this.txtPrcFilename.Size = new System.Drawing.Size(100, 20);
            this.txtPrcFilename.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Filename:";
            // 
            // txtParams
            // 
            this.txtParams.BackColor = System.Drawing.Color.White;
            this.txtParams.Location = new System.Drawing.Point(142, 54);
            this.txtParams.Name = "txtParams";
            this.txtParams.ReadOnly = true;
            this.txtParams.Size = new System.Drawing.Size(100, 20);
            this.txtParams.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Arguments:";
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Location = new System.Drawing.Point(142, 80);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(100, 20);
            this.txtUser.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "User:";
            // 
            // txtDomain
            // 
            this.txtDomain.BackColor = System.Drawing.Color.White;
            this.txtDomain.Location = new System.Drawing.Point(142, 106);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.ReadOnly = true;
            this.txtDomain.Size = new System.Drawing.Size(100, 20);
            this.txtDomain.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Domain:";
            // 
            // ucProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbPrcStartParams);
            this.Controls.Add(this.chbPrcResponding);
            this.Controls.Add(this.chbPrcPrioBoost);
            this.Controls.Add(this.txtProcessSessionID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtProcessStarttime);
            this.Controls.Add(this.txtProcessThreads);
            this.Controls.Add(this.txtProcessID);
            this.Controls.Add(this.txtProcessName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ucProcess";
            this.Size = new System.Drawing.Size(550, 190);
            this.gbPrcStartParams.ResumeLayout(false);
            this.gbPrcStartParams.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProcessName;
        private System.Windows.Forms.TextBox txtProcessID;
        private System.Windows.Forms.TextBox txtProcessThreads;
        private System.Windows.Forms.TextBox txtProcessStarttime;
        private System.Windows.Forms.TextBox txtProcessSessionID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chbPrcPrioBoost;
        private System.Windows.Forms.CheckBox chbPrcResponding;
        private System.Windows.Forms.GroupBox gbPrcStartParams;
        private System.Windows.Forms.TextBox txtPrcFilename;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtParams;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.Label label9;
    }
}
