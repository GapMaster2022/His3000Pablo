using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMailPlusLibrary;

namespace His.Honorarios
{
    public partial class frmCorreosCuentas : Form
    {
        public frmCorreosCuentas()
        {
            InitializeComponent();
        }

        private void frmCorreosCuentas_Load(object sender, EventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            EmailAccounts account = new EmailAccounts();
            account.KeepMessagesOnServer = chkLeaveCopy.Checked;
            account.MailClientType = MailClientType.POP3;
            account.Password = txtPassword.Text;
            account.PortNumber = txtPort.Text;
            account.Server = txtPOPServer.Text;
            account.UserName = txtEmail.Text;
            account.UseSSL = chkSSL.Checked;
            frmCorreosEntrantes.AddMailAccount(account);
            this.Close();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
