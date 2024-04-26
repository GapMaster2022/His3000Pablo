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
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Sockets;

namespace IMailPlusLibrary.MailClients
{
    public abstract class EmailClient : TcpClient
    {
        #region Properties
        /// <summary>
        /// States the client connectivity
        /// </summary>
        protected bool _isConnected;

        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value; }
        }
        /// <summary>
        /// If true then SSL Connection will be used other wise normal connection
        /// </summary>
        protected bool _useSSL;

        public bool UseSSL
        {
            get { return _useSSL; }
            set { _useSSL = value; }
        }
        /// <summary>
        /// Mail Server Port Number
        /// </summary>
        protected int _portNumber = 110;

        public int PortNumber
        {
            get { return _portNumber; }
            set { _portNumber = value; }
        }
        /// <summary>
        /// This is the client server communication response list
        /// </summary>
        protected List<string> _responseList = new List<string>();

        public List<string> ResponseList
        {
            get { return _responseList; }
            set { _responseList = value; }
        }
        /// <summary>
        /// This is the SSLStream Object used to work with the server
        /// </summary>
        protected System.Net.Security.SslStream inStream;
        /// <summary>
        /// This is the Email User Name
        /// </summary>
        protected string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        /// <summary>
        /// This is the Email Password
        /// </summary>
        protected string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// This is the mail server domain reference
        /// </summary>
        protected string _server;

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        #endregion

        #region Abstract Methods
        
        /// <summary>
        /// Connect the Client To The Server using the given username, password and server)
        /// </summary>
        /// <param name="server">The Mail Server domain reference</param>
        /// <param name="UserName">The Mail Account User Name</param>
        /// <param name="Password">The Mail Account Password</param>
        public abstract void Connect(string server, string UserName, string Password);
        

        /// <summary>
        /// Create the Protocol Communication Command in the appropriate format
        /// </summary>
        /// <param name="command">The command string</param>
        public abstract string CreateCommand(string command);

        /// <summary>
        /// Create the Protocol Communication Command in the appropriate format
        /// </summary>
        /// <param name="command">The command string</param>
        /// <param name="commandParameter">The commandParameter string</param>
        public abstract string CreateCommand(string command,string commandParameter);
        

        /// <summary>
        /// Disconnect the Client From the Server
        /// </summary>
        public abstract void Disconnect();
        

        /// <summary>
        /// Get the a list of email messages from the server
        /// </summary>
        public abstract List<Entities.MailMessage> GetMailList();
        

        /// <summary>
        /// Get the a list of email messages from the server from the specified id
        /// </summary>
        /// <param name="id">Message Id to start grabbing emails from server</param>
        public abstract System.Collections.Generic.List<IMailPlusLibrary.Entities.MailMessage> GetMailList(string id);
        

        

        

        
        #endregion

        #region Methods
        /// <summary>
        /// Execute the given command and return the response string
        /// </summary>
        /// <param name="command">Command to be executed</param>
        public string ExecuteCommand(string command)
        {
            return ExecuteCommand(command, null,false);
        }

        /// <summary>
        /// Execute the given command and return the response string
        /// </summary>
        /// <param name="command">Command to be executed</param>
        /// <param name="commandParameter">Command Parameter</param>
        public string ExecuteCommand(string command,string commandParameter)
        {
            return ExecuteCommand(command, commandParameter, false);
        }
        /// <summary>
        /// Execute the given command and return the response string
        /// </summary>
        /// <param name="command">Command to be executed</param>
        /// <param name="commandParameter">Command Parameter</param>
        /// <param name="isMessage">is the command for retreiving a message or not.</param>
        public string ExecuteCommand(string command, string commandParameter,bool isMessage)
        {
            string request;
            string responseString;
            if (commandParameter == null)
                request = CreateCommand(command);
            else
                request = CreateCommand(command, commandParameter);
            Write(request);
            responseString = Response(isMessage);
            return responseString;
        }
        /// <summary>
        /// Get the response string
        /// </summary>
        public string Response()
        {
            return Response(false);
        }
        /// <summary>
        /// Get the response string
        /// </summary>
        /// <param name="isMessage">Is the response retreiving a message or not</param>
        /// <returns></returns>
        public string Response(bool isMessage)
        {
            int res = 0;
            string response = "";
            if (!UseSSL)
            {
                NetworkStream stream = GetStream();
                byte[] bts = new byte[1024];
                string[] splitOptions = new string[1];
                splitOptions[0] = "\r\n";

                do
                {

                    res = stream.Read(bts, 0, bts.Length);
                    
                    response += Encoding.ASCII.GetString(bts, 0, res);
                    if (!isMessage)
                        break;
                    if (response.StartsWith("-"))
                        break;
                    string[] splitTest = response.Split(splitOptions, StringSplitOptions.RemoveEmptyEntries);
                    if (splitTest.Length != 0)
                        if (splitTest[splitTest.Length - 1] == ".")
                            break;
                } while (true);
                
            }
            else
            {
                
                byte []bts = new byte[1024];
                string[] splitOptions = new string[1];
                splitOptions[0] = "\r\n";

                do
                {

                    res = inStream.Read(bts, 0, bts.Length);
                    response += Encoding.ASCII.GetString(bts, 0, res);
                    if (!isMessage)
                        break;
                    if (response.StartsWith("-"))
                        break;
                    string[] splitTest = response.Split(splitOptions, StringSplitOptions.RemoveEmptyEntries);
                    if (splitTest.Length != 0)
                        if (splitTest[splitTest.Length - 1] == ".")
                            break;
                } while (true);
            }
            return response;
        }

        /// <summary>
        /// Write the given string to the stream
        /// </summary>
        /// <param name="message">The message to be written</param>
        public void Write(string message)
        {


            ASCIIEncoding en = new ASCIIEncoding();
            if (!UseSSL)
            {
                byte[] writeBuffer;
                writeBuffer = en.GetBytes(message);

                NetworkStream stream = GetStream();
                if (writeBuffer != null)
                    stream.Write(writeBuffer, 0, writeBuffer.Length);
            }
            else
            {
                inStream.Write(Encoding.ASCII.GetBytes(message));
            }
        }
        /// <summary>
        /// Dispose the client object
        /// </summary>
        public void Dispose()
        {
            Disconnect();

            base.Dispose(true);

            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Method to validate server certificate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        protected bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            return false;
        }

        /// <summary>
        /// Method to Select local certificate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="targetHost"></param>
        /// <param name="localCertificates"></param>
        /// <param name="remoteCertificate"></param>
        /// <param name="acceptableIssuers"></param>
        /// <returns></returns>
        protected X509Certificate SelectLocalCertificate(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
        {

            if (acceptableIssuers != null && acceptableIssuers.Length > 0 && localCertificates != null && localCertificates.Count > 0)
            {

                foreach (X509Certificate certificate in localCertificates)
                {

                    string issuer = certificate.Issuer;

                    if (Array.IndexOf(acceptableIssuers, issuer) != -1)

                        return certificate;

                }

            }

            if (localCertificates != null && localCertificates.Count > 0)
                return localCertificates[0];
            return null;

        }
        #endregion
    }
}
