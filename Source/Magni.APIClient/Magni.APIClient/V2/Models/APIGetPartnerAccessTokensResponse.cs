using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models
{
    public class APIGetPartnerAccessTokensResponse : APIResponse
    {
        public List<APIAccessTokenBase> AccessTokens { get; set; }

        internal APIGetPartnerAccessTokensResponse(Invoicing_v2.Response response) : base(response)
        {
            this.AccessTokens = new List<APIAccessTokenBase>();
        }
    }

    public class APIAccessTokenBase
    {
        public string AccessToken { get; set; }
        public string Description { get; set; }
    }
}
