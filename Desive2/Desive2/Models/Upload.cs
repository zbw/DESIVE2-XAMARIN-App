using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.Models
{
    /// <summary>
    /// Represents an upload response containing a message and an ID for the upload.
    /// </summary>
    public class Upload
    {
        /// <summary>
        /// Gets or sets the message associated with the upload.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the ID of the uploaded item.
        /// </summary>
        [JsonProperty("idUpload")]
        public int IdUpload { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Upload"/> class with the specified message and upload ID.
        /// </summary>
        /// <param name="message">The message associated with the upload.</param>
        /// <param name="upload">The ID of the uploaded item.</param>
        public Upload(string message, int upload)
        {
            this.Message = message;
            this.IdUpload = upload;
        }
    }
}

