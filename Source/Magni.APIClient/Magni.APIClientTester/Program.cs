using System;
using System.Linq;

namespace Magni.APIClientTester
{
    class Program
    {
        static Program()
        {
        }

        static void Main(string[] args)
        {
            string devEndpoint = "https://magnidev-slot2.azurewebsites.net/MagniAPI/Invoicing.asmx";
            string sandbox = "https://bo.magnifinance.com/MagniAPI/Invoicing.asmx";

            Magni.APIClient.V2.Invoicing api = new Magni.APIClient.V2.Invoicing(devEndpoint, email: "willian.pinfildi@magnifinance.com", token: "bU7iHN9HeE9wsKfm4EN-");

            AddPartner_Scenario_01(api);
        }

        /// <summary>
        /// Scenario Description:
        /// 1. Add Partner
        /// 2. Get Partners Access Tokens
        /// </summary>
        private static void AddPartner_Scenario_01(Magni.APIClient.V2.Invoicing api)
        {
            var newPartner = new APIClient.V2.Models.PartnerInformation()
            {
                CompanyAddress = "Morada",
                CompanyCity = "Lisboa",
                CompanyCountry = "PT",
                CompanyLegalName = "Emp Fix Lda",
                CompanyPostCode = "1500-244",
                CompanyTaxId = "999999990",
                UserEmail = "sbuserone@magnifinance.com",
                UserName = "sandbox user 01",
                UserPhone = "456789456"
            };

            var response = api.AddPartner(newPartner);

            if (response.Type == APIClient.V2.Models.ResponseType.Success)
            {
                string password = "adsasda9s8dahjoahhd";
                string partnerTaxId = "999999990";

                var partnerTokens = api.GetPartnerAccessTokens(password, partnerTaxId);
            }
        }
    }
}
