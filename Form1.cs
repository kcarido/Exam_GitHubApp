using CARIDO_GitHubApp.WebHandlers;
using System;
using System.Windows.Forms;

namespace CARIDO_GitHubApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SessionWebHandler web = new SessionWebHandler();
            rtbResult.Text = web.CreateSession(tbUsername.Text, tbPassword.Text);
        }
    }
}
