namespace FEMAIDE0._0
{
    partial class TrueFXLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrueFXLogin));
            this.LoginButton = new System.Windows.Forms.Button();
            this.UsernameField = new System.Windows.Forms.TextBox();
            this.PasswordField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DontHaveATrueFXAccountLink = new System.Windows.Forms.LinkLabel();
            this.BackToLauncherButton = new System.Windows.Forms.Button();
            this.ErrorMessageTrueFXLoginIn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.Aqua;
            this.LoginButton.Font = new System.Drawing.Font("Segoe Script", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.Location = new System.Drawing.Point(129, 223);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 34);
            this.LoginButton.TabIndex = 0;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // UsernameField
            // 
            this.UsernameField.BackColor = System.Drawing.SystemColors.Window;
            this.UsernameField.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.UsernameField.Location = new System.Drawing.Point(157, 55);
            this.UsernameField.Name = "UsernameField";
            this.UsernameField.Size = new System.Drawing.Size(100, 26);
            this.UsernameField.TabIndex = 1;
            // 
            // PasswordField
            // 
            this.PasswordField.Location = new System.Drawing.Point(157, 104);
            this.PasswordField.Name = "PasswordField";
            this.PasswordField.Size = new System.Drawing.Size(100, 26);
            this.PasswordField.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(64, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(64, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // DontHaveATrueFXAccountLink
            // 
            this.DontHaveATrueFXAccountLink.AutoSize = true;
            this.DontHaveATrueFXAccountLink.Location = new System.Drawing.Point(50, 183);
            this.DontHaveATrueFXAccountLink.Name = "DontHaveATrueFXAccountLink";
            this.DontHaveATrueFXAccountLink.Size = new System.Drawing.Size(232, 20);
            this.DontHaveATrueFXAccountLink.TabIndex = 5;
            this.DontHaveATrueFXAccountLink.TabStop = true;
            this.DontHaveATrueFXAccountLink.Text = "I Don\'t Have A TrueFX Account";
            this.DontHaveATrueFXAccountLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DontHaveATrueFXAccountLink_LinkClicked);
            // 
            // BackToLauncherButton
            // 
            this.BackToLauncherButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BackToLauncherButton.BackgroundImage")));
            this.BackToLauncherButton.Font = new System.Drawing.Font("Segoe Print", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackToLauncherButton.Location = new System.Drawing.Point(11, 3);
            this.BackToLauncherButton.Name = "BackToLauncherButton";
            this.BackToLauncherButton.Size = new System.Drawing.Size(332, 46);
            this.BackToLauncherButton.TabIndex = 6;
            this.BackToLauncherButton.Text = "Back To Launcher";
            this.BackToLauncherButton.UseVisualStyleBackColor = true;
            this.BackToLauncherButton.Click += new System.EventHandler(this.BackToLauncherButton_Click);
            // 
            // ErrorMessageTrueFXLoginIn
            // 
            this.ErrorMessageTrueFXLoginIn.AutoSize = true;
            this.ErrorMessageTrueFXLoginIn.ForeColor = System.Drawing.Color.Red;
            this.ErrorMessageTrueFXLoginIn.Location = new System.Drawing.Point(12, 158);
            this.ErrorMessageTrueFXLoginIn.Name = "ErrorMessageTrueFXLoginIn";
            this.ErrorMessageTrueFXLoginIn.Size = new System.Drawing.Size(0, 20);
            this.ErrorMessageTrueFXLoginIn.TabIndex = 7;
            // 
            // TrueFXLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(355, 284);
            this.Controls.Add(this.ErrorMessageTrueFXLoginIn);
            this.Controls.Add(this.BackToLauncherButton);
            this.Controls.Add(this.DontHaveATrueFXAccountLink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PasswordField);
            this.Controls.Add(this.UsernameField);
            this.Controls.Add(this.LoginButton);
            this.Name = "TrueFXLogin";
            this.Text = "TrueFXLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.TextBox UsernameField;
        private System.Windows.Forms.TextBox PasswordField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel DontHaveATrueFXAccountLink;
        private System.Windows.Forms.Button BackToLauncherButton;
        private System.Windows.Forms.Label ErrorMessageTrueFXLoginIn;
    }
}