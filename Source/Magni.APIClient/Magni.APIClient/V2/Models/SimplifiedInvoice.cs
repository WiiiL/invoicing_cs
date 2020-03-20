using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models
{
    public class SimplifiedInvoice : Document
    {
        public SimplifiedInvoice()
        {
            this.Type = DocumentType.SimplifiedInvoice;
        }
    }
}
