using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.ServiceModel;
using System.ServiceModel.Description;

namespace ChatHost
{
    public partial class ChatHostForm : Form
    {
        public ChatHostForm()
        {
            InitializeComponent();
        }

        ServiceHost host;

        private void ChatHostForm_Load(object sender, EventArgs e)
        {
            try
            {
                host = new ServiceHost(typeof(WcfChatServer.ChatService));
                host.Description.Behaviors.Add
                    (new ServiceMetadataBehavior { HttpGetEnabled = true });
                host.Open();
                label1.Text = "Service started...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

    }
}
