using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.Models
{
    public class IBAN
    {
        // Message associated with the IBAN data.
        [JsonProperty("message")]
        public string Message { get; set; }

        // The IBAN (International Bank Account Number).
        [JsonProperty("iban")]
        public string Iban { get; set; }

        // Constructor to initialize an IBAN instance with a message and IBAN value.
        public IBAN(string message, string iban)
        {
            this.Message = message;
            this.Iban = iban;
        }
    }
}
