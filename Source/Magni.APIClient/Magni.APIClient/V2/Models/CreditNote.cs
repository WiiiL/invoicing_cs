using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models
{
    public class CreditNote : Document
    {
        public CreditNote()
        {
            this.Type = DocumentType.CreditNote;
        }
    }
}
