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
    
    public class MetaMessageInfo
    {
        #region Attributes
        /// <summary>
        /// Load Meta Messages Info for POP3 Protocol
        /// </summary>
        /// <param name="messageInfo"></param>
        public void LoadPOP3(string messageInfo)
        {
            string result = messageInfo.Replace("+OK", "");
            result = result.Trim();
            string[] splittedResults = result.Split(' ');
            if (splittedResults.Length > 1)
            {
                Int32.TryParse(splittedResults[0], out NumberOfMessages);
            }
            for (int i = 2; i < splittedResults.Length; i++)
            {
                string str = splittedResults[i];
                str = str.Replace("(", "");
                str = str.Replace(")", "");
                str = str.Replace(".", "");
                if (i == 2)
                    Int64.TryParse(str, out TotalMessagesLength);
                else if (i == 3)
                    continue;
                else
                {
                    long subtractNumber = i - 2;
                    if (i == splittedResults.Length - 1)
                        str = str.Replace("\r\n", "");
                    else
                        str = str.Replace("\r\n" + subtractNumber, "");
                    long messageLength = 0;
                    Int64.TryParse(str, out messageLength);
                    MessagesLength.Add(messageLength);
                }
            }
        }

        /// <summary>
        /// Number of Messages to Retrieve
        /// </summary>
        public int NumberOfMessages = 0;

        /// <summary>
        /// Total Messages Length
        /// </summary>
        public long TotalMessagesLength = 0;

        /// <summary>
        /// A List of the sizes of each message in ascending order
        /// </summary>
        public List<long> MessagesLength = new List<long>();
        #endregion

        #region Constructors
        public MetaMessageInfo()
        {
         
        }
        public MetaMessageInfo(MailClientType type,string messageInfo)
        {
            Load(type, messageInfo);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load the meta messages info
        /// </summary>
        /// <param name="type"></param>
        /// <param name="messageInfo"></param>
        public void Load(MailClientType type,string messageInfo)
        {
            switch (type)
            {
                case MailClientType.POP3:
                    LoadPOP3(messageInfo);
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
