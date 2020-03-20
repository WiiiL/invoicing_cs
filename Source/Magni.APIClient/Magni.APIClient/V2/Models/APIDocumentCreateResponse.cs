using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models
{
    public class APIDocumentCreateResponse : APIResponse
    {
        public DocumentCreateOut Document { get; set; }

        internal APIDocumentCreateResponse(Invoicing_v2.DocumentCreateResponse response)
            : base(response.Body.Response)
        {
            if(response.Body.Response.Object != null)
            {
                this.Document = new DocumentCreateOut()
                {

                    DocumentId = response.Body.Response.Object.DocumentId,
                    ErrorMessage = response.Body.Response.Object.ErrorMessage
                };
            }
        }
    }

    public class DocumentCreateOut
    {
        public int DocumentId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
