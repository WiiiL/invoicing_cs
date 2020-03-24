using Magni.APIClient.V2.Models.BulkQueue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Magni.Core.APIClientTester.Scenarios
{
    public static class BulkInvoiceCreate
    {
        public static void BulkInvoiceCreate_Scenario_01(Magni.APIClient.V2.Invoicing api)
        {
            var documentsToProcess = new List<QueueItem>();

            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenA", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenA", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenB", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenA", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenC", isClosed: false, emailToSend: null));

            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenA", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenB", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenB", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenB", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenA", isClosed: false, emailToSend: null));

            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenC", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenA", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenC", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenB", isClosed: false, emailToSend: null));
            documentsToProcess.Add(new QueueItem(BuildClient(), BuildDocument(series: null), "tokenC", isClosed: false, emailToSend: null));

            api.EnqueueDocuments(documentsToProcess);

            bool wait = true;
            while (wait)
            {
                Task.Delay(10000);
            }
        }

        public static APIClient.V2.Models.Client BuildClient(string nif = "999999990")
        {
            var client = new APIClient.V2.Models.Client()
            {
                Address = "Av. Sidónio Pais",
                City = "Lisboa",
                CountryCode = "PT",
                Name = "Consumidor Final",
                NIF = nif,
                PhoneNumber = "111111112",
                PostCode = "1500-244"
            };

            return client;
        }

        /// <summary>
        /// Build a new document instance (Invoice)
        /// </summary>
        /// <param name="documentDaysPassed">Instead of the invoice datetime, pass the number of days to be added to the invoice date</param>
        public static APIClient.V2.Models.SimplifiedInvoice BuildDocument(string series = null, int documentDaysPassed = 60, decimal unitPrice = 10)
        {
            if(documentDaysPassed > 0)
            {
                // subtract days to date
                documentDaysPassed = documentDaysPassed * -1;
            }

            DateTime date = DateTime.Now.AddDays(documentDaysPassed);

            var invoice = new APIClient.V2.Models.SimplifiedInvoice()
            {
                Currency = "EUR",
                Date = date,
                Description = "API Client Test",
                DueDate = date.AddDays(30),
                EuroRate = 0,
                ExternalId = null,
                Id = 0,
                Retention = 0,
                Series = series,
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
                UnitPrice = unitPrice
            });

            return invoice;
        }
    }
}
