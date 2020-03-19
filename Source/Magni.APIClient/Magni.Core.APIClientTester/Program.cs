using System;

namespace Magni.Core.APIClientTester
{
    class Program
    {
        static Magni.APIClient.V2.Invoicing api;

        static void Main(string[] args)
        {
            string devEndpoint = "https://magnidev-slot2.azurewebsites.net/MagniAPI/Invoicing.asmx";

            api = new Magni.APIClient.V2.Invoicing(devEndpoint, email: "willian.pinfildi@magnifinance.com", token: "bU7iHN9HeE9wsKfm4EN-");

            AddPartner();

        }

        private static void AddPartner()
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
    }
}
