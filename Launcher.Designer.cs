namespace FEMAIDE0._0
{
    partial class Launcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.LaunchTrueFXButton = new System.Windows.Forms.Button();
            this.LaunchCantourButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LaunchTrueFXButton
            // 
            this.LaunchTrueFXButton.BackColor = System.Drawing.SystemColors.Control;
            this.LaunchTrueFXButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LaunchTrueFXButton.BackgroundImage")));
            this.LaunchTrueFXButton.Font = new System.Drawing.Font("Segoe Print", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchTrueFXButton.ForeColor = System.Drawing.Color.Orange;
            this.LaunchTrueFXButton.Location = new System.Drawing.Point(-6, -1);
            this.LaunchTrueFXButton.Name = "LaunchTrueFXButton";
            this.LaunchTrueFXButton.Size = new System.Drawing.Size(329, 150);
            this.LaunchTrueFXButton.TabIndex = 0;
            this.LaunchTrueFXButton.Text = "Launch True FX";
            this.LaunchTrueFXButton.UseVisualStyleBackColor = false;
            this.LaunchTrueFXButton.Click += new System.EventHandler(this.LaunchTrueFXButton_Click);
            // 
            // LaunchCantourButton
            // 
            this.LaunchCantourButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LaunchCantourButton.BackgroundImage")));
            this.LaunchCantourButton.Font = new System.Drawing.Font("Sitka Display", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchCantourButton.ForeColor = System.Drawing.Color.SkyBlue;
            this.LaunchCantourButton.Location = new System.Drawing.Point(-6, 126);
            this.LaunchCantourButton.Name = "LaunchCantourButton";
            this.LaunchCantourButton.Size = new System.Drawing.Size(329, 163);
            this.LaunchCantourButton.TabIndex = 1;
            this.LaunchCantourButton.Text = "Launch Cantour";
            this.LaunchCantourButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-27, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 6);
            this.label1.TabIndex = 2;
            this.label1.Text = "Top Picture By: worradmu\r\nBottom Picture By: Idea go.\r\nWebsite: www.freedigitalpi" +
    "ctures.net\r\n";
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 244);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LaunchCantourButton);
            this.Controls.Add(this.LaunchTrueFXButton);
            this.MaximumSize = new System.Drawing.Size(330, 300);
            this.MinimumSize = new System.Drawing.Size(330, 300);
            this.Name = "Launcher";
            this.Text = "Launcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LaunchTrueFXButton;
        private System.Windows.Forms.Button LaunchCantourButton;
        private System.Windows.Forms.Label label1;
    }
}

