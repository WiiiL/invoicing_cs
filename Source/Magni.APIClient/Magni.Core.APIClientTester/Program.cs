using System;

namespace Magni.Core.APIClientTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string devEndpoint = "https://magnidev-slot2.azurewebsites.net/MagniAPI/Invoicing.asmx";

            Magni.APIClient.V2.Invoicing api = new Magni.APIClient.V2.Invoicing(devEndpoint, email: "willian.pinfildi@magnifinance.com", token: "bU7iHN9HeE9wsKfm4EN-");

            AddPartner(api);
            GetPartnerAccessTokens(api);
        }

        private static void AddPartner(Magni.APIClient.V2.Invoicing api)
        {
            var newPartner = new APIClient.V2.Models.PartnerInformation() {
                CompanyAddress = "Morada",
                CompanyCity = "Lisboa",
                CompanyCountry = "PT",
                CompanyLegalName = "Emp Fix Lda",
                CompanyPostCode = "1500",
                CompanyTaxId = "1",
                UserEmail = "sb-user-one@magnifinance.com",
                UserName = "sandbox user 01",
                UserPhone = "456789456"
            };

            var response = api.AddPartner(newPartner);
        }

        private static void GetPartnerAccessTokens(Magni.APIClient.V2.Invoicing api)
        {
            string password = "adsasda9s8dahjoahhd";
            string partnerTaxId = "123";

            var response = api.GetPartnerAccessTokens(password, partnerTaxId);
        }
    }
}
