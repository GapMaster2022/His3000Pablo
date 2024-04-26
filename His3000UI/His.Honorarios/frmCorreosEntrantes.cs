using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMailPlusLibrary;
using IMailPlusLibrary.MailClients;

namespace His.Honorarios
{
    public partial class frmCorreosEntrantes : Form
    {
        public static List<EmailAccounts> CurrentMailAccounts = new List<EmailAccounts>();
        public static frmCorreosEntrantes MainFormRef;
        
        public static void AddMailAccount(EmailAccounts account)
        {
            CurrentMailAccounts.Add(account);
            TreeNode node = new TreeNode(account.UserName);
            node.Nodes.Add("inbox");
            MainFormRef.accountsTree.Nodes.Add(node);
        }
        
        public frmCorreosEntrantes()
        {
            InitializeComponent();
            frmCorreosEntrantes.MainFormRef = this;
        }

        private void frmCorreosEntrantes_Load(object sender, EventArgs e)
        {

        }
        private void addEmailAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCorreosCuentas frm = new frmCorreosCuentas(); 
            frm.ShowDialog();

        }

        private void accountsTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                lblSelectedUser.Text = e.Node.Text;
            }
            else if (e.Node.Level == 1)
            {
                lblSelectedUser.Text = e.Node.Parent.Text;
            }
        }
        System.Collections.Hashtable tblEmailsText = new System.Collections.Hashtable();
        System.Collections.Hashtable tblEmailsHTML = new System.Collections.Hashtable();
        private void getSelectedMailAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (accountsTree.SelectedNode != null)
            {
                try
                {
                    string username = "";
                    if (accountsTree.SelectedNode.Level == 0)
                    {
                        username = accountsTree.SelectedNode.Text;
                    }
                    else if (accountsTree.SelectedNode.Level == 1)
                    {
                        username = accountsTree.SelectedNode.Parent.Text;
                    }
                    EmailAccounts account = EmailAccounts.GetAccount(username, CurrentMailAccounts);
                    if (account == null)
                        return;

                    POPEmailClient popClient = new POPEmailClient();
                    popClient.UseSSL = account.UseSSL;
                    popClient.PortNumber = Convert.ToInt32(account.PortNumber);
                    popClient.Connect(account.Server, account.UserName, account.Password);
                    account.CurrentAccountMails = popClient.GetMailList();
                    int index = 0;
                    gridEmails.Rows.Clear();
                    tblEmailsHTML.Clear();
                    tblEmailsText.Clear();
                    foreach (IMailPlusLibrary.Entities.MailMessage message in account.CurrentAccountMails)
                    {
                        gridEmails.Rows.Add(message.Received, message.Subject, message.From, message.MessageId);
                        tblEmailsText.Add(index, message.TextMessage);
                        tblEmailsHTML.Add(index, message.HtmlMessage);
                        index++;
                    }

                    if (!account.KeepMessagesOnServer)
                        popClient.Disconnect();

                    //Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }

        private void gridEmails_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (System.Windows.Forms.DataGridViewRow rowView in gridEmails.SelectedRows)
                {
                    if (tblEmailsHTML.ContainsKey(rowView.Index))
                    {
                        System.IO.File.WriteAllText("C:\\Work\\htmlEmail.html", tblEmailsHTML[rowView.Index].ToString());
                        wbHTMLEmail.Url = new Uri("C:\\Work\\htmlEmail.html", UriKind.RelativeOrAbsolute);
                    }
                    if (tblEmailsText.ContainsKey(rowView.Index))
                    {
                        System.IO.File.WriteAllText("C:\\Work\\textEmail.html", tblEmailsText[rowView.Index].ToString());
                        wbTextEmail.Url = new Uri("C:\\Work\\textEmail.html", UriKind.RelativeOrAbsolute);

                    }

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message );
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCorreosNuevo ventana = new frmCorreosNuevo();
            ventana.ShowDialog();  
        }
    }
}
