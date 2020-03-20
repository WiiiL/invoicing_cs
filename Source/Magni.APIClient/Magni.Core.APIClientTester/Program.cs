using Magni.APIClient.V2.Models;
using System;
using System.Linq;

namespace Magni.Core.APIClientTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string devEndpoint = "https://magnidev-slot2.azurewebsites.net/MagniAPI/Invoicing.asmx";
            string sandbox = "https://bo.magnifinance.com/MagniAPI/Invoicing.asmx";

            Magni.APIClient.V2.Invoicing api = new Magni.APIClient.V2.Invoicing(devEndpoint, email: "willian.pinfildi@magnifinance.com", token: "bU7iHN9HeE9wsKfm4EN-");
            //Magni.APIClient.V2.Invoicing api = new Magni.APIClient.V2.Invoicing(sandbox, email: "nikola.tesla.sandbox @magnifinance.com", token: "u55TUxfA17w2H8VvHW5h");

            //AddPartner(api);
            //GetPartnerAccessTokens(api);

            //PartnerScenario(api);
            //GetDocument(api);

            //AddPartner_Scenario_01(api);
            CreateSimpleInvoice_Scenario_01(api);
        }


        #region Scenarios

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
                UserEmail = "sb.user.one@magnifinance.com",
                UserName = "sandbox user 01",
                UserPhone = "456789456"
            };

            var response = api.AddPartner(newPartner);

            if(response.Type == APIClient.V2.Models.ResponseType.Success)
            {
                string password = "adsasda9s8dahjoahhd";
                string partnerTaxId = "999999990";

                var partnerTokens = api.GetPartnerAccessTokens(password, partnerTaxId);
            }
        }

        /// <summary>
        /// Scenario description:
        /// 1. Create Invoice
        /// 2. Correct invoice with Credit Note
        /// 3. Create new Invoice
        /// </summary>
        private static void CreateSimpleInvoice_Scenario_01(Magni.APIClient.V2.Invoicing api)
        {
            var client = new APIClient.V2.Models.Client()
            {
                Address = "Av. Sidónio Pais",
                City = "Lisboa",
                CountryCode = "PT",
                Name = "Consumidor Final",
                NIF = "999999990",
                PhoneNumber = "956758481",
                PostCode = "1500-244"
            };

            var invoice = new APIClient.V2.Models.SimplifiedInvoice()
            {
                Currency = "EUR",
                Date = DateTime.Now,
                Description = "API Client Test",
                DueDate = DateTime.Now.AddDays(30),
                EuroRate = 0,
                ExternalId = null,
                Id = 0,
                Retention = 0,
                Series = null,
                TaxExemptionReasonCode = null
            };

            invoice.Lines.Add(new APIClient.V2.Models.APIInvoicingProduct()
            {
                Code = "999",
                CostCenter = null,
                Description = "General Expense",
                ProductDiscount = 0,
                Quantity = 1,
                TaxValue = 23,
                Type = APIClient.V2.Models.ProductType.S,
                Unit = "API Activation",
                UnitPrice = 10
            });

            var response = api.InvoiceSimplifiedCreate(client, invoice, isToClose: false, sentTo: "tiago.vieira@magnifinance.com");
            

            if (response.Type == APIClient.V2.Models.ResponseType.Success)
            {
                var creditNote = new APIClient.V2.Models.CreditNote()
                {
                    Currency = "EUR",
                    Date = DateTime.Now,
                    Description = "API Client Test",
                    DueDate = DateTime.Now.AddDays(30),
                    EuroRate = 0,
                    ExternalId = null,
                    Id = response.Document.DocumentId,
                    Retention = 0,
                    Series = null,
                    TaxExemptionReasonCode = null
                };

                creditNote.Lines.Add(invoice.Lines.First());

                var creditNoteResponse = api.CreditNoteCreate(client, creditNote, isToClose: false, sentTo: "tiago.vieira@magnifinance.com");

                if(creditNoteResponse.Type == APIClient.V2.Models.ResponseType.Success)
                {
                    invoice.Lines.First().UnitPrice = 8;
                    response = api.InvoiceSimplifiedCreate(client, invoice, isToClose: false, sentTo: "tiago.vieira@magnifinance.com");
                }
            }
        }

        #endregion

        private static void AddPartner(Magni.APIClient.V2.Invoicing api)
        {
            var newPartner = new APIClient.V2.Models.PartnerInformation() {
                CompanyAddress = "Morada",
                CompanyCity = "Lisboa",
                CompanyCountry = "PT",
                CompanyLegalName = "Emp Fix Lda",
                CompanyPostCode = "1500-244",
                CompanyTaxId = "999999990",
                UserEmail = "sb.user.one@magnifinance.com",
                UserName = "sandbox user 01",
                UserPhone = "456789456"
            };

            var response = api.AddPartner(newPartner);
        }

        private APIGetPartnerAccessTokensResponse GetPartnerAccessTokens(Magni.APIClient.V2.Invoicing api, string password, string partnerTaxId)
        {
            return api.GetPartnerAccessTokens(password, partnerTaxId);
        }

        private static void GetDocument(Magni.APIClient.V2.Invoicing api)
        {
            var response = api.DocumentGet(1);
        }
    }
}
