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
using System.Globalization;
using System.Net.Security;
using IMailPlusLibrary.Entities;

namespace IMailPlusLibrary.MailClients
{
    public class POPEmailClient : EmailClient
    {
        #region Overrided Methods
        /// <summary>
        /// Connect the Client To The Server using the given username, password and server)
        /// </summary>
        /// <param name="server">The Mail Server domain reference</param>
        /// <param name="UserName">The Mail Account User Name</param>
        /// <param name="Password">The Mail Account Password</param>
        public override void Connect(string server, string UserName, string Password)
        {
            try
            {
                if (_isConnected)
                {
                    return;
                }
                if (!UseSSL)
                {
                    Connect(server, PortNumber);

                    string response = Response();
                    if (!response.Trim().StartsWith("+OK"))
                    {
                        //TODO: Raise Error Event
                    }
                    else
                    {
                        ExecuteCommand("USER", UserName);
                        ExecuteCommand("PASS", Password);
                    }

                    _isConnected = true;
                }
                else
                {
                    byte[] bts;
                    int res;
                    string responseString = "";
                    ResponseList.Clear();
                    Connect(server, PortNumber);
                    inStream = new SslStream(this.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), new LocalCertificateSelectionCallback(SelectLocalCertificate));
                    inStream.AuthenticateAsClient(server);
                    bts = new byte[1024];
                    res = inStream.Read(bts, 0, bts.Length);
                    ResponseList.Add(Encoding.ASCII.GetString(bts, 0, res));
                    responseString = ExecuteCommand("USER", UserName);
                    ResponseList.Add(responseString);
                    responseString = ExecuteCommand("PASS", Password);
                    ResponseList.Add(responseString);

                    if (!responseString.Trim().StartsWith("+OK"))
                    {
                        //TODO: Raise Error Event
                    }
                    else
                        _isConnected = true;

                }
            }
            catch (Exception ex)
            {
                //TODO: Raise Error Event
            }
        }

        /// <summary>
        /// Create the Protocol Communication Command in the appropriate format
        /// </summary>
        /// <param name="command">The command string</param>
        public override string CreateCommand(string command)
        {
            return CreateCommand(command, null);
        }

        /// <summary>
        /// Create the Protocol Communication Command in the appropriate format
        /// </summary>
        /// <param name="command">The command string</param>
        /// <param name="commandParameter">The commandParameter string</param>
        public override string CreateCommand(string command, string commandParameter)
        {
            if(commandParameter == null)
                return String.Format(CultureInfo.InvariantCulture, "{0}\r\n", command);
            else
                return String.Format(CultureInfo.InvariantCulture, "{0} {1}\r\n", command, commandParameter);
        }

        /// <summary>
        /// Disconnect the Client From the Server
        /// </summary>
        public override void Disconnect()
        {
            if (!_isConnected)
                return;

            try
            {
                ExecuteCommand("QUIT");
            }
            finally
            {
                _isConnected = false;
            }
        }

        /// <summary>
        /// Get the a list of email messages from the server
        /// </summary>
        public override List<MailMessage> GetMailList()
        {
            string response = "";

            List<MailMessage> result = new List<MailMessage>();

            //SendCommand("LAST");

            string responseString = ExecuteCommand("LIST");
            MetaMessageInfo info = new MetaMessageInfo(MailClientType.POP3,responseString);

            for (int i = 1; i <= info.NumberOfMessages; i++)
            {
                responseString = ExecuteCommand("RETR", i.ToString(), true);
                POPMessage message = new POPMessage(responseString);
                message.Number = i.ToString();
                message.Retrieved = true;
                if (message.MessageBoundaryId != null)
                    result.Add(message);

            }
            return result;
        }

        /// <summary>
        /// Get the a list of email messages from the server from the specified id
        /// </summary>
        /// <param name="id">Message Id to start grabbing emails from server</param>
        public override List<MailMessage> GetMailList(string id)
        {
            string response = "";

            List<MailMessage> result = new List<MailMessage>();

            //SendCommand("LAST");

            string responseString = ExecuteCommand("LIST");
            MetaMessageInfo info = new MetaMessageInfo(MailClientType.POP3, responseString);
            int indexStart = 1;
            Int32.TryParse(id, out indexStart);
            for (int i = indexStart; i <= info.NumberOfMessages; i++)
            {
                responseString = ExecuteCommand("RETR", i.ToString(), true);
                POPMessage message = new POPMessage(responseString);
                message.Number = i.ToString();
                message.Retrieved = true;
                if (message.MessageBoundaryId != null)
                    result.Add(message);

            }
            return result;
        }
        #endregion
    }
}
