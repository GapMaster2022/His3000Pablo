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

namespace IMailPlusLibrary.Entities
{
    public class POPMessage : MailMessage
    {
        #region Properties
        /// <summary>
        /// Message Boundary Id
        /// </summary>
        private string _messageBoundaryId;

        public string MessageBoundaryId
        {
            get 
            {
                if (String.IsNullOrEmpty(_messageBoundaryId))
                {
                    string contentType = GetHeaderValue(EmailHeaders.Boundary);
                    if (contentType == null || contentType == "")
                    {
                        string result = Message;
                        string headerName = "boundary=";
                        int lastindex = 0;
                        int index = result.IndexOf(String.Format(CultureInfo.InvariantCulture, "{0}", headerName), StringComparison.OrdinalIgnoreCase);
                        if (index == -1)
                        {
                            result = null;
                        }
                        else
                        {

                            result = result.Remove(0, (index + headerName.Length));
                            result = result.Replace("\"", "");
                            lastindex = result.IndexOf('\r');

                            //lastindex++;
                            result = result.Substring(0, lastindex);
                            //result = result.Remove(result.IndexOf('\r'), (result.Length - result.IndexOf('\r'))).Replace("\n", String.Empty).Trim();
                        }
                        _messageBoundaryId = result;
                    }
                    else
                    {
                        string[] details = contentType.Split('=');
                        if (details.Length > 0)
                            _messageBoundaryId = details[details.Length - 1];
                        else
                            _messageBoundaryId = "";
                    }
                }
                return _messageBoundaryId; 
            }
            set { _messageBoundaryId = value; }
        }
        /// <summary>
        /// The email received date
        /// </summary>
        private DateTime _receivedDate;

        public DateTime ReceivedDate
        {
            get 
            {
                if (_receivedDate == DateTime.MinValue)
                {
                    string messagePartReceived = Received;
                    string[] details = messagePartReceived.Split(';');
                    if (details.Length == 2)
                    {
                        string strTimeZone = details[1];//.Replace("Date:", "");
                        string[] str = strTimeZone.Split('(');
                        string zone = str[0];
                        DateTime.TryParse(zone.Trim(), out _receivedDate);
                    }
                }
                return _receivedDate; 
            }
            set { _receivedDate = value; }
        }
        /// <summary>
        /// The Email Sender IP or Domain
        /// </summary>
        private string _receivedFromHost;

        public string ReceivedFromHost
        {
            get 
            {
                if (String.IsNullOrEmpty(_receivedFromHost))
                {
                    string messagePartReceived = Received;
                    string[] details = messagePartReceived.Split(';');
                    if (details[0].Contains("by"))
                    {
                        string bystr = details[0].Replace("by", "");
                        bystr = bystr.Replace("with HTTP", "");
                        bystr = bystr.Replace("from", "");
                        bystr = bystr.Trim();
                        _receivedFromHost = bystr;
                    }
                }
                return _receivedFromHost; 
            }
            set { _receivedFromHost = value; }
        }
        /// <summary>
        /// The Email Portion of the From Field
        /// </summary>
        public string FromEmail
        {
            get
            {
                string result = From;
                string[] strFrom = result.Split('<');

                if (strFrom.Length == 2)
                {
                    result = strFrom[1].ToString().Replace(">", "");
                    result = result.Trim();
                }
                else
                {
                    result = strFrom[0].ToString().Replace(">", "");
                    result = result.Trim();
                }
                return result;
            }
        }

        /// <summary>
        /// The Name Portion of the From Field
        /// </summary>
        public string FromName
        {
            get
            {

                string result = From;
                string[] strFrom = result.Split('<');

                if (strFrom.Length > 0)
                {
                    result = strFrom[0].ToString().Replace(">", "");
                    result = result.Trim();
                }
                return result;
            }
        }

        /// <summary>
        /// The Email Portion of the To Field
        /// </summary>
        public string ToEmail
        {
            get
            {
                string result = To;
                string[] strTo = To.Split('<');

                if (strTo.Length == 2)
                {
                    result = strTo[1].ToString().Replace(">", "");
                    result = result.Trim();
                }
                else
                {
                    result = strTo[0].ToString().Replace(">", "");
                    result = result.Trim();
                }
                return result;
            }
        }

        /// <summary>
        /// The Name Portion of the To Field
        /// </summary>
        public string ToName
        {
            get
            {
                string result = To;
                string[] strTo = result.Split('<');

                if (strTo.Length > 0)
                {
                    result = strTo[0].ToString().Replace(">", "");
                    result = result.Trim();
                }
                return result;
            }
        }

        /// <summary>
        /// The Date of the message in date time format
        /// </summary>
        public DateTime OriginDate
        {
            get
            {
                DateTime dt = System.DateTime.Now;
                DateTime.TryParse(Date, out dt);
                return dt;
            }
        }
        #endregion

        #region Constructors
        public POPMessage()
        {

        }
        public POPMessage(string messageString)
        {
            Load(messageString);
        }
        #endregion

        #region Overrided Methods
        /// <summary>
        /// This method gets the header value from the message according the given Enumerator
        /// </summary>
        /// <param name="headerName">The header Enumerator is turned to string then a ":" is added to it to find its corresponding value in the message also underscores are changed to dashes</param>
        public override string GetHeaderValue(EmailHeaders header)
        {
            string headerName = GetHeaderName(header);
            string result = Message;

            int index = result.IndexOf(String.Format(CultureInfo.InvariantCulture, "\r\n{0}", headerName), StringComparison.OrdinalIgnoreCase);
            if (index == -1)
            {
                result = "";
            }
            else
            {
                result = result.Remove(0, (index + headerName.Length + 2));
                result = result.Remove(result.IndexOf('\r'), (result.Length - result.IndexOf('\r'))).Replace("\n", String.Empty).Trim();
            }

            return result;
        }

        /// <summary>
        /// This method gets the header value from the provided message according the given Enumerator
        /// </summary>
        /// <param name="headerName">The header Enumerator is turned to string then a ":" is added to it to find its corresponding value in the message also underscores are changed to dashes</param>
        /// <param name="message">The message to load the header from</param>
        public override string GetHeaderValue(EmailHeaders header, string message)
        {
            string headerName = GetHeaderName(header);
            string result = message;

            int index = result.IndexOf(String.Format(CultureInfo.InvariantCulture, "\r\n{0}", headerName), StringComparison.OrdinalIgnoreCase);
            if (index == -1)
            {
                result = "";
            }
            else
            {
                result = result.Remove(0, (index + headerName.Length + 2));
                result = result.Remove(result.IndexOf('\r'), (result.Length - result.IndexOf('\r'))).Replace("\n", String.Empty).Trim();
            }

            return result;
        }

        /// <summary>
        /// This method gets the HTML and Plain Text body of the message
        /// </summary>
        /// <param name="message">The message string to get the body from</param>
        public override void GetMessageBody(string []messages)
        {
            
            foreach (string strMessage in messages)
            {
                if (strMessage == "--\r\n.\r\n" || strMessage == "--\r\n." || strMessage == "\r\n.\r\n" || strMessage == "\r\n.")
                    continue;
                string body = strMessage;
                string result = strMessage;
                #region Content Type
                string headerName = "Content-Type:";

                int lastindex = 0;
                int index = result.IndexOf(String.Format(CultureInfo.InvariantCulture, "\r\n{0}", headerName), StringComparison.OrdinalIgnoreCase);
                if (index == -1)
                {
                    result = null;
                }
                else
                {
                    body = body.Remove(0, (index + headerName.Length + 2));
                    result = result.Remove(0, (index + headerName.Length + 2));

                    lastindex = result.IndexOf('\r');
                    if (result.IndexOf('\n') > -1)
                        lastindex++;
                    lastindex++;
                    body = body.Substring(lastindex, body.Length - lastindex);
                    //result = result.Remove(result.IndexOf('\r'), (result.Length - result.IndexOf('\r'))).Replace("\n", String.Empty).Trim();
                }

                #endregion
                #region Content Transfer
                headerName = "Content-Transfer-Encoding:";
                lastindex = 0;
                result = body;
                index = result.IndexOf(String.Format(CultureInfo.InvariantCulture, "{0}", headerName), StringComparison.OrdinalIgnoreCase);
                if (index == -1)
                {
                    result = null;
                }
                else
                {
                    body = body.Remove(0, (index + headerName.Length + 2));
                    result = result.Remove(0, (index + headerName.Length + 2));

                    lastindex = result.IndexOf('\r');
                    if (result.IndexOf('\n') > -1)
                        lastindex++;
                    lastindex++;
                    body = body.Substring(lastindex, body.Length - lastindex);
                    //result = result.Remove(result.IndexOf('\r'), (result.Length - result.IndexOf('\r'))).Replace("\n", String.Empty).Trim();
                }

                
                #endregion

                if (GetHeaderValue(EmailHeaders.Content_Type, strMessage).Contains("plain"))
                {
                    TextMessage = body;
                }
                else if((GetHeaderValue(EmailHeaders.Content_Type, strMessage).Contains("html")))
                {
                    HtmlMessage = body;
                }
            }
        }

        /// <summary>
        /// This method loads the message details from the provided string
        /// </summary>
        /// <param name="message">the message as received from the server</param>
        public override void Load(string messageString)
        {
            string message = messageString.Replace("+OK message follows", "");
            Message = message;
            string messageIdPure = MessageBoundaryId;

            int bodyIndex = message.IndexOf("--" + messageIdPure, StringComparison.CurrentCultureIgnoreCase);
            int messageLength = message.Length - bodyIndex;
            if (bodyIndex < 0)
                return;
            string bodyString = message.Substring(bodyIndex, messageLength);
            string[] splitMessageOptions = new string[1];
            splitMessageOptions[0] = "--" + messageIdPure;
            string[] messages = bodyString.Split(splitMessageOptions, StringSplitOptions.RemoveEmptyEntries);
            GetMessageBody(messages);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get Header Name Needed to extract its value from the message
        /// </summary>
        /// <param name="header">The enum used to get its name from</param>
        /// <returns></returns>
        private string GetHeaderName(EmailHeaders header)
        {
            string headerName = header.ToString();
            headerName = headerName.Replace("_", "-");
            switch (header)
            {
                case EmailHeaders.Boundary:
                    headerName += "=";
                    break;
                default:
                    headerName += ":";
                    break;
            }
            return headerName;
        }
        #endregion
    }
}
