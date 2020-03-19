using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models
{
    public class PartnerInformation
    {
        /// <summary>
        /// text(75) - Name of the contact person in the partner organization.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// email - Email of the contact person in the partner organization.
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// text(50) - Phone number of the contact person in the partner organization.
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// text(20) - Partner organization’s tax id.
        /// </summary>
        public string CompanyTaxId { get; set; }
        /// <summary>
        /// text(50) - Partner organization’s legal name.
        /// </summary>
        public string CompanyLegalName { get; set; }
        /// <summary>
        /// text(200) - Partner organization’s address.
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// text(50) - Partner organization’s city.
        /// </summary>
        public string CompanyCity { get; set; }
        /// <summary>
        /// text(50) - Partner organization’s post code.
        /// </summary>
        public string CompanyPostCode { get; set; }
        /// <summary>
        /// text(2) - Partner organization’s country code. (ISO 3166-1 alpha-2).
        /// </summary>
        public string CompanyCountry { get; set; }
    }
}
