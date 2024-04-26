/*
************************************************************************************************************************************
***                                                                                                                              ***
*** By Ramy Mostafa -							                                                                                 ***
*** http://www.ramymostafa.com                                                                                                   ***
*** 13/01/2009                                                                                                                   ***
*** Last Modified February 15 2009                                                                                               ***
************************************************************************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace IMailPlusLibrary
{
    public class EmailAccounts
    {
        /// <summary>
        /// The mail server domain reference
        /// </summary>
        private string _server;

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        /// <summary>
        /// The username of the mail account
        /// </summary>
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        /// <summary>
        /// The password of the mail account
        /// </summary>
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// Does the mail server require SSL
        /// </summary>
        private bool _useSSL;

        public bool UseSSL
        {
            get { return _useSSL; }
            set { _useSSL = value; }
        }
        /// <summary>
        /// Does the user wants to keep a copy of the emails retrieved on the server
        /// </summary>
        private bool _keepMessagesOnServer;

        public bool KeepMessagesOnServer
        {
            get { return _keepMessagesOnServer; }
            set { _keepMessagesOnServer = value; }
        }
        /// <summary>
        /// The Mail Server Client Type
        /// </summary>
        private MailClientType _mailClientType;

        public MailClientType MailClientType
        {
            get { return _mailClientType; }
            set { _mailClientType = value; }
        }

        /// <summary>
        /// The Mail Server Port Number
        /// </summary>
        private string _portNumber;

        public string PortNumber
        {
            get { return _portNumber; }
            set { _portNumber = value; }
        }
        /// <summary>
        /// The Sending Server Domain Reference(i.e: smtp.yourdomain.com)
        /// </summary>
        private string _sendingServer;

        public string SendingServer
        {
            get { return _sendingServer; }
            set { _sendingServer = value; }
        }
        
        public List<Entities.MailMessage> CurrentAccountMails = new List<IMailPlusLibrary.Entities.MailMessage>();

        public static EmailAccounts GetAccount(string username, List<EmailAccounts> list)
        {
            foreach (EmailAccounts account in list)
            {
                if (account.UserName == username)
                    return account;
            }
            
            return null;
        }
    }
}
