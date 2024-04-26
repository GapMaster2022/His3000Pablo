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
    /// <summary>
    /// The Email Header Enumerator enumerate the different string header
    /// portions in order to grab their values.
    /// </summary>
    public enum EmailHeaders
    {
        /// <summary>
        /// ex in POP3: from:
        /// </summary>
        From,
        /// <summary>
        /// ex in POP3: to:
        /// </summary>
        To,
        /// <summary>
        /// ex in POP3: date:
        /// </summary>
        Date,
        /// <summary>
        /// ex in POP3: message-id:
        /// </summary>
        Message_Id,
        /// <summary>
        /// ex in POP3: subject:
        /// </summary>
        Subject,
        /// <summary>
        /// ex in POP3: Content-Transfer-Encoding:
        /// </summary>
        Content_Transfer_Encoding,
        /// <summary>
        /// ex in POP3: boundary=
        /// </summary>
        Boundary,
        /// <summary>
        /// ex in POP3: mime-version:
        /// </summary>
        MIME_Version,
        /// <summary>
        /// ex in POP3: received:
        /// </summary>
        Received,
        /// <summary>
        /// ex in POP3: content-type:
        /// </summary>
        Content_Type
    }

    /// <summary>
    /// Enumerator to identify the mail client type
    /// </summary>
    public enum MailClientType
    {
        POP3,
        IMAP
    }
}
