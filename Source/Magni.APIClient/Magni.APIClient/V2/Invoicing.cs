using Invoicing_v2;
using Magni.APIClient.V2.Models;
using Magni.APIClient.V2.Models.BulkQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magni.APIClient.V2
{
    public class Invoicing
    {
        public string Endpoint { get; set; }
        public string EMail { get; set; }
        public string Token { get; set; }

        private QueueEngine queueEngine;

        public QueueEngine QueueEngine
        {
            get
            {
                if(this.queueEngine == null)
                {
                    this.queueEngine = new QueueEngine();
                }

                return this.queueEngine;
            }
        }


        public Invoicing(string endpoint, string email, string token)
        {
            this.Endpoint = endpoint;
            this.EMail = email;
            this.Token = token;
        }


        #region API Methods

        public APIAddPartnerResponse AddPartner(PartnerInformation newPartner)
        {
            Invoicing_v2.InvoicingSoapClient client = GetAPIClient();

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
            Invoicing_v2.InvoicingSoapClient client = GetAPIClient();
            
            var serviceResponse = client.DocumentGetAsync(GetAuthenticationCredentials(), documentId).Result;

            return DocumentGetAPIResponse(serviceResponse);
        }
       
        public APIGetPartnerAccessTokensResponse GetPartnerAccessTokens(string password, string partnerTaxId)
        {
            Invoicing_v2.InvoicingSoapClient client = GetAPIClient();

            var serviceResponse = client.GetPartnerAccessTokensAsync(GetAPISpecialAuthenticationCredentials(password), partnerTaxId).Result;

            return GetPartnerAccessTokenAPIResponse(serviceResponse);
        }

        /// <summary>
        /// Create simplified invoice
        /// </summary>
        /// <param name="client">Client identification</param>
        /// <param name="invoice">Purchase information</param>
        /// <param name="isToClose">Should the document be closed (true) in which case it cannot be later changed and a PDF and document number are generated or should the document be created as a draft (false) in which case no PDF or document number are generated.</param>
        /// <param name="sentTo">Upon closing the document an email can be sent with a notification of the document to an email address.</param>
        public APIDocumentCreateResponse InvoiceSimplifiedCreate(Models.Client client, SimplifiedInvoice invoice, bool isToClose, string sentTo)
        {
            Invoicing_v2.InvoicingSoapClient apiClient = GetAPIClient();
            Invoicing_v2.Client invoiceClient = ToInvoicingClient(client);

            DocumentIn document = ToInvoicingDocument(invoice);

            var serviceResponse = apiClient.DocumentCreateAsync(GetAuthenticationCredentials(), invoiceClient, document, isToClose, sentTo).Result;

            return new APIDocumentCreateResponse(serviceResponse);
        }

        /// <summary>
        /// Create Credit Note
        /// </summary>
        /// <param name="client">Client identification</param>
        /// <param name="invoice">Purchase information</param>
        /// <param name="isToClose">Should the document be closed (true) in which case it cannot be later changed and a PDF and document number are generated or should the document be created as a draft (false) in which case no PDF or document number are generated.</param>
        /// <param name="sentTo">Upon closing the document an email can be sent with a notification of the document to an email address.</param>
        public APIDocumentCreateResponse CreditNoteCreate(Models.Client client, CreditNote invoice, bool isToClose, string sentTo)
        {
            Invoicing_v2.InvoicingSoapClient apiClient = GetAPIClient();

            Invoicing_v2.Client invoiceClient = ToInvoicingClient(client);

            DocumentIn document = ToInvoicingDocument(invoice);

            var serviceResponse = apiClient.DocumentCreateAsync(GetAuthenticationCredentials(), invoiceClient, document, isToClose, sentTo).Result;

            return new APIDocumentCreateResponse(serviceResponse);
        }


        public void EnqueueDocuments(List<QueueItem> items)
        {
            this.QueueEngine.AddToQueue(items);
        }

        #endregion

        #region API Methods Async

        public Task<APIAddPartnerResponse> AddPartnerAsync(PartnerInformation newPartner)
        {
            Invoicing_v2.InvoicingSoapClient client = GetAPIClient();

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
            Invoicing_v2.InvoicingSoapClient client = GetAPIClient();

            return Task.Run<APIDocumentGetResponse>(async () => {
                var serviceResponse = await client.DocumentGetAsync(GetAuthenticationCredentials(), documentId);

                return DocumentGetAPIResponse(serviceResponse);
            });
        }

        public Task<APIGetPartnerAccessTokensResponse> GetPartnerAccessTokensAsync(string password, string partnerTaxId)
        {
            Invoicing_v2.InvoicingSoapClient client = GetAPIClient();

            return Task.Run<APIGetPartnerAccessTokensResponse>(async () => {
                var serviceResponse = await client.GetPartnerAccessTokensAsync(GetAPISpecialAuthenticationCredentials(password), partnerTaxId);

                return GetPartnerAccessTokenAPIResponse(serviceResponse);
            });
        }

        #endregion

        #region Transformers

        private APIAddPartnerResponse AddPartnerAPIResponse(AddPartnerResponse serviceResponse)
        {
            return new APIAddPartnerResponse(serviceResponse.Body.ResponseAddPartner);
        }

        private APIDocumentGetResponse DocumentGetAPIResponse(DocumentGetResponse serviceResponse)
        {
            var apiResponse = new APIDocumentGetResponse(serviceResponse.Body.Response);

            if (serviceResponse.Body.Response.Object != null)
            {
                apiResponse.Document = new Models.DocumentGetOut()
                {
                    DocumentNumber = serviceResponse.Body.Response.Object.DocumentNumber,
                    DownloadUrl = serviceResponse.Body.Response.Object.DownloadUrl,
                    ErrorMessage = serviceResponse.Body.Response.Object.ErrorMessage
                };
            }

            return apiResponse;
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


        private Invoicing_v2.Client ToInvoicingClient(Models.Client client)
        {
            return new Invoicing_v2.Client()
            {
                Name = client.Name,
                NIF = client.NIF,
                Address = client.Address,
                City = client.City,
                PostCode = client.PostCode,
                CountryCode = client.CountryCode,
                PhoneNumber = client.PhoneNumber
            };
        }

        private DocumentIn ToInvoicingDocument(Document invoice)
        {
            var document = new DocumentIn()
            {
                Type = Convert.ToChar(invoice.Type).ToString(),
                Date = invoice.Date.ToString(Constants.Format.DateTime.ShortDate),
                DueDate = invoice.DueDate.ToString(Constants.Format.DateTime.ShortDate),
                Description = invoice.Description,
                Serie = invoice.Series,
                TaxExemptionReasonCode = invoice.TaxExemptionReasonCode,
                Currency = invoice.Currency,
                EuroRate = invoice.EuroRate,
                //Retention = invoice.Retention,
                ExternalId = invoice.ExternalId,
                Id = invoice.Id
            };

            document.Lines = new System.Collections.Generic.List<Invoicing_v2.APIInvoicingProduct>();
#warning Retention missing from contract ???

            document.Lines = invoice.Lines.Select(product => new Invoicing_v2.APIInvoicingProduct()
            {
                Code = product.Code,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Quantity = product.Quantity,
                Unit = product.Unit,
                Type = product.Type.ToString(),
                TaxValue = product.TaxValue,
                ProductDiscount = product.ProductDiscount,
                CostCenter = product.CostCenter
            })
            .ToList();

            return document;
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

        private SpecialAuthentication GetAPISpecialAuthenticationCredentials(string password)
        {
            return new Invoicing_v2.SpecialAuthentication()
            {
                Email = this.EMail,
                Token = this.Token,
                Password = password
            };
        }

        private InvoicingSoapClient GetAPIClient()
        {
            if (string.IsNullOrEmpty(this.Endpoint))
            {
                return new Invoicing_v2.InvoicingSoapClient(Invoicing_v2.InvoicingSoapClient.EndpointConfiguration.InvoicingSoap12);
            }

            return new Invoicing_v2.InvoicingSoapClient(Invoicing_v2.InvoicingSoapClient.EndpointConfiguration.InvoicingSoap12, this.Endpoint);
        }
    }
}
