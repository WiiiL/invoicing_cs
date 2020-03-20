using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models
{
    public class APIDocumentGetResponse : APIResponse
    {
        public DocumentGetOut Document { get; set; }

        internal APIDocumentGetResponse(Invoicing_v2.Response response)
            : base(response)
        {
        }
    }

    public class DocumentGetOut
    {
        /// <summary>
        /// The document’s sequence number in the series.
        /// </summary>
        public string DocumentNumber { get; set; }
        /// <summary>
        /// Url to download the document. The returned url should be used for you to keep a copy of the PDF and is valid for 5 minutes
        /// </summary>
        public string DownloadUrl { get; set; }
        /// <summary>
        /// Unprocessed error message.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
