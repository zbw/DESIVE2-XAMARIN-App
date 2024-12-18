using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Desive2.Models
{
    public class Login
    {
        // Unique identifier for the app user.
        [JsonProperty("idAppUser")]
        public string UserId { get; set; }

        // Indicates whether the user has set a password.
        [JsonProperty("hasSetPassword")]
        public bool HasSetPassword { get; set; }

        // Authentication token for the user session.
        [JsonProperty("token")]
        public string Token { get; set; }

        // Status code indicating the result of the login attempt.
        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
