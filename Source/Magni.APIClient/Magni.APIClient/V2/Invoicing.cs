using Invoicing_v2;
using Magni.APIClient.V2.Models;
using Magni.APIClient.V2.Models.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Magni.APIClient.V2
{
    public class Invoicing
    {
        public string Endpoint { get; set; }
        public string EMail { get; set; }
        public string Token { get; set; }


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

        public APIDocumentGetResponse DocumentGet(int documentId)
        {
            Invoicing_v2.InvoicingSoapClient client = GetClient();
            
            var serviceResponse = client.DocumentGetAsync(GetAuthenticationCredentials(), documentId).Result;

            return DocumentGetAPIResponse(serviceResponse);
        }
       
        public APIGetPartnerAccessTokensResponse GetPartnerAccessTokens(string password, string partnerTaxId)
        {
            Invoicing_v2.InvoicingSoapClient client = GetClient();

            var serviceResponse = client.GetPartnerAccessTokensAsync(GetSpecialAuthenticationCredentials(password), partnerTaxId).Result;

            return GetPartnerAccessTokenAPIResponse(serviceResponse);
        }


        public object CreateSimplefiedInvoice(IAuthentication authentication)
        {
            return this.CreateSimplefiedInvoiceAsync().Result;
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

        #region API Methods Async

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

        public Task<APIDocumentGetResponse> DocumentGetAsync(int documentId)
        {
            Invoicing_v2.InvoicingSoapClient client = GetClient();

            return Task.Run<APIDocumentGetResponse>(async () => {
                var serviceResponse = await client.DocumentGetAsync(GetAuthenticationCredentials(), documentId);

                return DocumentGetAPIResponse(serviceResponse);
            });
        }

        public Task<APIGetPartnerAccessTokensResponse> GetPartnerAccessTokensAsync(string password, string partnerTaxId)
        {
            Invoicing_v2.InvoicingSoapClient client = GetClient();

            return Task.Run<APIGetPartnerAccessTokensResponse>(async () => {
                var serviceResponse = await client.GetPartnerAccessTokensAsync(GetSpecialAuthenticationCredentials(password), partnerTaxId);

                return GetPartnerAccessTokenAPIResponse(serviceResponse);
            });
        }

        #endregion

        #region Transformers

        private APIAddPartnerResponse AddPartnerAPIResponse(AddPartnerResponse serviceResponse)
        {
            return new APIAddPartnerResponse(serviceResponse.Body.ResponseAddPartner);
        }

        private APIGetPartnerAccessTokensResponse GetPartnerAccessTokenAPIResponse(GetPartnerAccessTokensResponse serviceResponse)
        {
            var apiResponse = new APIGetPartnerAccessTokensResponse(serviceResponse.Body.ResponseGetPartnerAccessTokens);

            if (serviceResponse.Body.ResponseGetPartnerAccessTokens.Object != null
                && serviceResponse.Body.ResponseGetPartnerAccessTokens.Object.Any())
            {
                apiResponse.AccessTokens = serviceResponse.Body.ResponseGetPartnerAccessTokens.Object
                    .Select(token => new Models.APIAccessTokenBase()
                    {
                        AccessToken = token.AccessToken,
                        Description = token.Description
                    })
                    .ToList();
            }

            return apiResponse;
        }

        private APIDocumentGetResponse DocumentGetAPIResponse(DocumentGetResponse serviceResponse)
        {
            return new APIDocumentGetResponse();
        }


        #endregion

        private Authentication GetAuthenticationCredentials()
        {
            return new Invoicing_v2.Authentication()
            {
                Email = this.EMail,
                Token = this.Token
            };
        }

        private SpecialAuthentication GetSpecialAuthenticationCredentials(string password)
        {
            return new Invoicing_v2.SpecialAuthentication()
            {
                Email = this.EMail,
                Token = this.Token,
                Password = password
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
