using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models
{
    public enum DocumentType
    {
        /// <summary>
        /// Invoice or recipe
        /// </summary>
        InvoiceRecipe = 'T',
        /// <summary>
        /// Invoice
        /// </summary>
        Invoice = 'I',
        /// <summary>
        /// Simplefied Invoice
        /// </summary>
        SimplifiedInvoice = 'S',
        /// <summary>
        /// Credit Note
        /// </summary>
        CreditNote = 'C',
        /// <summary>
        /// Debit Note
        /// </summary>
        DebitNote = 'D'
    }
}
