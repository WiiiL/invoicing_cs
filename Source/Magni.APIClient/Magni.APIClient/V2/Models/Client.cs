using System;
using System.Collections.Generic;
using System.Text;

namespace Magni.APIClient.V2.Models
{
    public class Client
    {
        /// <summary>
        /// text(75) - required - Client name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// text(75) - (required) - Client Tax ID. The tax id must be valid.
        /// </summary>
        public string NIF { get; set; }
        /// <summary>
        /// text(200) - Client address.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// text(50) - Client city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// text(50) - Client post code. The post code must be valid.
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// text(2) - required - Client country (ISO 3166-1 alpha-2).
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// text(50) - Client phone number.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
