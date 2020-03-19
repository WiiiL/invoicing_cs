using Invoicing_v2;
using Magni.APIClient.V2.Models;
using Magni.APIClient.V2.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace Magni.APIClient.V2
{
    public class Invoicing
    {
        public string Endpoint { get; set; }
        public string EMail { get; set; }
        public string Token { get; set; }

        /*
            -1 projeto que vai ser uma lead de Cliente, vai ter todos os metodos da API (objetivo é poder ser reutilizado por qualquer cliente).
            -1 projeto de console aplication, com vários exemplos:
            criar uma fatura simplificada.
            registar o parceiro, 
            buscar o token e 
            emitir a fatura.
            outros exemplos que possam fazer sentido.
            Jorge e Tiago vão reunir no fim da daily para ver de que forma o Tiago vai iniciar este projeto.
        */

        public Invoicing(string endpoint, string email, string token)
        {
            this.Endpoint = endpoint;
            this.EMail = email;
            this.Token = token;
        }


        #region API Methods

        public APIAddPartnerResponse AddPartner(PartnerInformation newPartner)
        {
            Invoicing_v2.InvoicingSoapClient client = GetClient();

            var request = new NewPartnerInformation()
            {
                UserName = newPartner.UserName,
                UserEmail = newPartner.UserEmail,
                UserPhone = newPartner.UserPhone,
                CompanyTaxId = newPartner.CompanyTaxId,
                CompanyLegalName = newPartner.CompanyLegalName,
                CompanyAddress = newPartner.CompanyAddress,
                CompanyCity = newPartner.CompanyCity,
                CompanyPostCode = newPartner.CompanyPostCode,
                CompanyCountry = newPartner.CompanyCountry
            };

            var serviceResponse = client.AddPartnerAsync(GetAuthenticationCredentials(), request).Result;

            return AddPartnerAPIResponse(serviceResponse);
        }

        public Task<APIAddPartnerResponse> AddPartnerAsync(PartnerInformation newPartner)
        {
            Invoicing_v2.InvoicingSoapClient client = GetClient();

            var request = new NewPartnerInformation()
            {
                UserName = newPartner.UserName,
                UserEmail = newPartner.UserEmail,
                UserPhone = newPartner.UserPhone,
                CompanyTaxId = newPartner.CompanyTaxId,
                CompanyLegalName = newPartner.CompanyLegalName,
                CompanyAddress = newPartner.CompanyAddress,
                CompanyCity = newPartner.CompanyCity,
                CompanyPostCode = newPartner.CompanyPostCode,
                CompanyCountry = newPartner.CompanyCountry
            };


            return Task.Run<APIAddPartnerResponse>(async () => {
                var serviceResponse = await client.AddPartnerAsync(GetAuthenticationCredentials(), request);

                return AddPartnerAPIResponse(serviceResponse);
            });
        }

        public APIDocumentGetResponse DocumentGet(int documentId)
        {
            Invoicing_v2.InvoicingSoapClient client = GetClient();
            
            var serviceResponse = client.DocumentGetAsync(GetAuthenticationCredentials(), documentId).Result;

            return DocumentGetAPIResponse(serviceResponse);
        }
        
        public Task<APIDocumentGetResponse> DocumentGetAsync(int documentId)
        {
            Invoicing_v2.InvoicingSoapClient client = GetClient();

            return Task.Run<APIDocumentGetResponse>(async () => {
                var serviceResponse = await client.DocumentGetAsync(GetAuthenticationCredentials(), documentId);

                return DocumentGetAPIResponse(serviceResponse);
            });
        }
        
        public object CreateSimplefiedInvoice(IAuthentication authentication)
        {
            return this.CreateSimplefiedInvoiceAsync().Result;
        }

        public void GetPartnerAccessTokens()
        {

        }

        private object CreateInvoice(IAuthentication authentication)
        {
            return this.CreateSimplefiedInvoiceAsync().Result;
        }

        public async Task<Object> CreateSimplefiedInvoiceAsync()
        {
            var test = new Invoicing_v2.InvoicingSoapClient(Invoicing_v2.InvoicingSoapClient.EndpointConfiguration.InvoicingSoap12);

            return test.DocumentCreateAsync(new Invoicing_v2.Authentication(), new Invoicing_v2.Client(), new Invoicing_v2.DocumentIn(), IsToClose: false, SendTo: null);
        }

        #endregion

        private APIDocumentGetResponse DocumentGetAPIResponse(DocumentGetResponse serviceResponse)
        {
            return new APIDocumentGetResponse();
        }

        private APIAddPartnerResponse AddPartnerAPIResponse(AddPartnerResponse serviceResponse)
        {
            return new APIAddPartnerResponse(serviceResponse.Body.ResponseAddPartner);
        }

        private Authentication GetAuthenticationCredentials()
        {
            return new Invoicing_v2.Authentication()
            {
                Email = this.EMail,
                Token = "bU7iHN9HeE9wsKfm4EN-"
            };
        }

        private InvoicingSoapClient GetClient()
        {
            if (string.IsNullOrEmpty(this.Endpoint))
            {
                return new Invoicing_v2.InvoicingSoapClient(Invoicing_v2.InvoicingSoapClient.EndpointConfiguration.InvoicingSoap12);
            }

            return new Invoicing_v2.InvoicingSoapClient(Invoicing_v2.InvoicingSoapClient.EndpointConfiguration.InvoicingSoap12, this.Endpoint);
        }
    }
}
