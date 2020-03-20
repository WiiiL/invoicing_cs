using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models
{
    /// <summary>
    /// This object represents an Recipe / Invoice / Simplified Invoice / Credit Note / Debit Note
    /// </summary>
    public class Document
    {
        /// <summary>
        /// text(1) - required
        /// Type of the document to be created. 
        /// Possible types are “T” for Fatura/Recibo, “I” for Fatura, “S” for Fatura Simplificada, “C” for credit note and “D” for Debit Note.
        /// </summary>
        public DocumentType Type { get; protected set; }
        /// <summary>
        /// date - Document date. This date must be posterior to the previously closed document date for this series
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// date - Date in which payment is due. Must be posterior to Date.
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// text(300) - Description to be included in the document.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// text(30)
        /// Document series (sequence) in which the document will be created. 
        /// Series have a unique name per document type. If not sent, the default series for that type of document in that subscription will be used.
        /// </summary>
        public string Series { get; set; }
        /// <summary>
        /// text(3) - Required if there is at least one line with 0% tax. Justifies why 0% tax was used.
        /// (Valid valus M01, M02, M03, M04, M05, M06, M07, M08, M09, M10, M11, M12, M013, M14, M15, M16, M99)
        /// </summary>
        public string TaxExemptionReasonCode { get; set; }
        /// <summary>
        /// text(3) - Currency to be used additionally to EUR. (ISO 4217)
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// decimal - Number by which to multiply the amounts to get the alternative currency amount.
        /// </summary>
        public decimal EuroRate { get; set; }
        /// <summary>
        /// decimal - Percentage value of document total which will be to tax authority.
        /// </summary>
        public decimal Retention { get; set; }
        /// <summary>
        /// text(50) - Your identifier for this document. Used to prevent document duplication. 
        /// Uniqueness is verified in combination with the Client information.
        /// </summary>
        public string ExternalId { get; set; }
        /// <summary>
        /// integer - Your identifier for this document. 
        /// Used to prevent document duplication. Uniqueness is verified in combination with the Client information.
        /// </summary>
        public int Id { get; set; }

        public List<APIInvoicingProduct> Lines { get; set; }

        public Document()
        {
            this.Lines = new List<APIInvoicingProduct>();
        }
    }

    public class APIInvoicingProduct
    {
        /// <summary>
        /// text(60) - Identifies the product.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// text(200) - Description of the product.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// decimal - Amount per unit.
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// decimal - Number of units.
        /// </summary>
        public decimal Quantity{ get; set; }
        /// <summary>
        /// text(50) - Unit name.
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// text(1) - “S” for services, “P” for product.
        /// </summary>
        public ProductType Type { get; set; }
        /// <summary>
        /// decimal - Percentage of tax
        /// </summary>
        public decimal TaxValue { get; set; }
        /// <summary>
        /// Percentage of discount
        /// </summary>
        public decimal ProductDiscount { get; set; }
        /// <summary>
        /// Cost center to be used for the line
        /// </summary>
        public string CostCenter { get; set; }
    }

    public enum ProductType
    {
        /// <summary>
        ///  Services
        /// </summary>
        S,
        /// <summary>
        /// Product
        /// </summary>
        P
    }
}
