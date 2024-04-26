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

namespace IMailPlusLibrary.Entities
{
    public abstract class MailMessage
    {
        #region Properties
        /// <summary>
        /// The Message Size
        /// </summary>
        protected long _size;

        public long Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// The Sender (Email and\or Name)
        /// </summary>
        protected string _from;

        public string From
        {
            get 
            {
                if (String.IsNullOrEmpty(_from))
                    _from = GetHeaderValue(EmailHeaders.From);
    
                return _from; 
            }

            set { _from = value; }
        }

        /// <summary>
        /// The Reciever (Email and\or Name)
        /// </summary>
        protected string _to;

        public string To
        {
            get 
            {
                if (String.IsNullOrEmpty(_to))
                    _to = GetHeaderValue(EmailHeaders.To);
                return _to; 
            }
            set { _to = value; }
        }

        /// <summary>
        /// Email Date - as received in the message header
        /// </summary>
        protected string _date;

        public string Date
        {
            get 
            {
                if (String.IsNullOrEmpty(_date))
                    _date = GetHeaderValue(EmailHeaders.Date);
                return _date; 
            }
            set { _date = value; }
        }
        /// <summary>
        /// Email Number
        /// </summary>
        protected string _number;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }
        /// <summary>
        /// Message Id as recieved in the header
        /// </summary>
        protected string _messageId;

        public string MessageId
        {
            get 
            {
                if (String.IsNullOrEmpty(_messageId))
                    _messageId = GetHeaderValue(EmailHeaders.Message_Id);
                return _messageId; 
            }
            set { _messageId = value; }
        }
        

        /// <summary>
        /// Complete Message as Received Including Headers
        /// </summary>
        protected string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// The Email Message in HTML
        /// </summary>
        protected string _htmlMessage;

        public string HtmlMessage
        {
            get { return _htmlMessage; }
            set { _htmlMessage = value; }
        }
        /// <summary>
        /// The Email Message in Plain Text
        /// </summary>
        protected string _textMessage;

        public string TextMessage
        {
            get { return _textMessage; }
            set { _textMessage = value; }
        }
        /// <summary>
        /// The Received Date
        /// </summary>
        protected string _received;

        public string Received
        {
            get 
            {
                if (String.IsNullOrEmpty(_received))
                    _received = GetHeaderValue(EmailHeaders.Received);
                return _received; 
            }
            set { _received = value; }
        }


        /// <summary>
        /// Email Mime Version
        /// </summary>
        protected string _mimeVersion;

        public string MimeVersion
        {
            get 
            {
                if (String.IsNullOrEmpty(_mimeVersion))
                    _mimeVersion = GetHeaderValue(EmailHeaders.MIME_Version);
                
                return _mimeVersion; 
            }
            set { _mimeVersion = value; }
        }
        /// <summary>
        /// Is the complete message retrieved or not
        /// </summary>
        protected bool _retrieved;

        public bool Retrieved
        {
            get 
            {               
                return _retrieved; 
            }
            set { _retrieved = value; }
        }
        /// <summary>
        /// The Email Subject
        /// </summary>
        protected string _subject;

        public string Subject
        {
            get 
            {
                if (String.IsNullOrEmpty(_subject))
                    _subject = GetHeaderValue(EmailHeaders.Subject);
                return _subject; 
            }
            set { _subject = value; }
        }

        /// <summary>
        /// Email Content Transfer Encoding
        /// </summary>
        protected string _contentTransferEncoding;

        public string ContentTransferEncoding
        {
            get 
            {
                if (String.IsNullOrEmpty(_contentTransferEncoding))
                    _contentTransferEncoding = GetHeaderValue(EmailHeaders.Content_Transfer_Encoding);
                return _contentTransferEncoding; 
            }
            set { _contentTransferEncoding = value; }
        }

        /// <summary>
        /// The Message in Bytes
        /// </summary>
        public byte[] MessageBytes
        {
            get
            {
                ASCIIEncoding enc = new ASCIIEncoding();

                return enc.GetBytes(Message);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method gets the header value from the message according the given Enumerator
        /// </summary>
        /// <param name="headerName">The header Enumerator is turned to string then a ":" is added to it to find its corresponding value in the message also underscores are changed to dashes</param>
        public abstract string GetHeaderValue(EmailHeaders headerName);

        /// <summary>
        /// This method gets the header value from the provided message according the given Enumerator
        /// </summary>
        /// <param name="headerName">The header Enumerator is turned to string then a ":" is added to it to find its corresponding value in the message also underscores are changed to dashes</param>
        /// <param name="message">The message to load the header from</param>
        public abstract string GetHeaderValue(EmailHeaders headerName, string message);
        

        /// <summary>
        /// This method gets the HTML and Plain Text body of the message
        /// </summary>
        /// <param name="message">The message string to get the body from</param>
        public abstract void GetMessageBody(string []messages);
        

        /// <summary>
        /// This method loads the message details from the provided string
        /// </summary>
        /// <param name="message">the message as received from the server</param>
        public abstract void Load(string message);
        #endregion

        

        

    }
}
