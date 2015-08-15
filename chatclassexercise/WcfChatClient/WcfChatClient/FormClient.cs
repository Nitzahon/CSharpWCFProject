using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfChatClient.ServiceReference1;

namespace WcfChatClient
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
        }

        private string Username = "";
        private ChatServiceClient client;

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin loginForm = new FormLogin();
            loginForm.ShowDialog();
            if (loginForm.Username!="")
            {
                try
                {
                    Username = loginForm.Username;
                    client.NewClientConnected(Username);
                    buttonSend.Enabled = true;
                    connectToolStripMenuItem.Enabled = false;
                    labelUserName.Text = "Your username is " + Username;
                }
                catch (FaultException<UserExistsFault> ex)
                {
                    MessageBox.Show(ex.Detail.message);
                }
                catch(FaultException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            ClientCallback callback = new ClientCallback();
            callback.addMessageToTextbox += new ClientCallback.UpdateTextBoxDelegate(
                AddNewMessage);
            callback.updateUsersList += new ClientCallback.UpdateListDelegate(
                UpdateUsersListbox);
            client = new ChatServiceClient(new InstanceContext(callback));
        }

        private void AddNewMessage(string message)
        {
            textBoxConversation.Text += message + "\r\n";
        }

        private void UpdateUsersListbox(List<string> users)
        {
            listBoxUsers.DataSource = users;
        }

        private void FormClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.ClientDisconnected(Username);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxMessage.Text))
            {
                List<string> selectedUsers = new List<string>();
                foreach (var item in listBoxUsers.SelectedItems)
                {
                    selectedUsers.Add(item as string);
                }
                client.GetMessageFromClient(Username + ": " + textBoxMessage.Text,
                    selectedUsers);
            }
        }
    }
}
