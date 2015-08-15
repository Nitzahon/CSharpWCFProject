using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WcfChatClient
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            Username = "";
        }

        public string Username { get; set; }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxUserName.Text))
            {
                MessageBox.Show("You must enter username");
            }
            else
            {
                Username = textBoxUserName.Text;
                this.Hide();
            }
        }
    }
}
