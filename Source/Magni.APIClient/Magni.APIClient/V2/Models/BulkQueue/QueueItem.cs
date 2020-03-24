using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models.BulkQueue
{
    public class QueueItem
    {
        /// <summary>
        /// Client information
        /// </summary>
        public Client Client { get; set; }
        /// <summary>
        /// Purchase information</param>
        /// </summary>
        public Document Document { get; set; }
        /// <summary>
        /// Client unique token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Should the document be closed (true) in which case it cannot be later changed and a PDF and document number are generated or should the document be created as a draft (false) in which case no PDF or document number are generated.
        /// </summary>
        public bool IsClosed { get; set; }
        /// <summary>
        /// Upon closing the document an email can be sent with a notification of the document to an email address.
        /// </summary>
        public string EMailToSend { get; set; }

        public QueueItem()
        {
        }

        public QueueItem(Client client, Document document, string token, bool isClosed, string emailToSend)
        {
            this.Client = client;
            this.Document = document;
            this.Token = token;
            this.IsClosed = IsClosed;
            this.EMailToSend = emailToSend;
        }
    }
}
