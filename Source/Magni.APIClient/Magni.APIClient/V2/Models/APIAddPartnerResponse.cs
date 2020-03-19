using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models
{
    public class APIAddPartnerResponse : APIResponse
    {
        internal APIAddPartnerResponse(Invoicing_v2.Response response)
            : base(response)
        {
        }
    }
}
