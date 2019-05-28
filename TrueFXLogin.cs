using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEMAIDE0._0
{
    public partial class TrueFXLogin : Form
    {
        public TrueFXLogin()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // August 15th, Tuesday, 2017
            //
            // This is the only place in the ecosystem we use the authenticate with TrueFX method. We use it nowhere else.
            //

            if ( TrueFX.Authenticate( UsernameField.Text, PasswordField.Text) )                                                                     
            {
                new TrueFXMissionConrtol().Show();
            }
            else
            {
                ErrorMessageTrueFXLoginIn.Text = "Either The Username Or Password Is Incorrect";
            }
        }

        private void BackToLauncherButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DontHaveATrueFXAccountLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.truefx.com/?page=register");
            }
            catch ( System.ComponentModel.Win32Exception )
            {
                Console.Error.WriteLine("ERROR WITHIN | CLASS: TrueFXLogin | METHOD: DontHaveATrueFXAccountLink_LinkClicked " +
                                         "| The current system does not have any default web browser installed. Link will not " +
                                         "open. ");
            }

        }
    }
}
