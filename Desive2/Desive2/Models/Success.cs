using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Desive2.Models
{
    /// <summary>
    /// Represents a success response with a status and a message.
    /// </summary>
    public class Success
    {
        /// <summary>
        /// Gets or sets the status of the success response.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the message of the success response.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Success"/> class with the specified status and message.
        /// </summary>
        /// <param name="status">The status of the success response.</param>
        /// <param name="message">The message of the success response.</param>
        public Success(string status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}

