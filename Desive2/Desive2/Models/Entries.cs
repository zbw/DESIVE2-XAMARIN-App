using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.Models
{
    public class Entries
    {
        // The message associated with the entry.
        [JsonProperty("message")]
        public string Message { get; set; }

        // The number of entries recorded.
        [JsonProperty("numOfEntries")]
        public int NumOfEntries { get; set; }

        // Constructor to initialize an Entries instance with a message and number of entries.
        public Entries(string message, int numEntries)
        {
            this.Message = message;
            this.NumOfEntries = numEntries;
        }
    }
}

