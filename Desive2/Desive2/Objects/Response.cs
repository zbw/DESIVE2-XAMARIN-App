using Android.Transitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.Objects
{
    /// <summary>
    /// Represents the response received from an API call.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the status of the response.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the body of the response containing additional data.
        /// </summary>
        [JsonProperty("body")]
        public Body Body { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class with a given status and body.
        /// </summary>
        /// <param name="status">The status code of the response.</param>
        /// <param name="body">The body of the response containing relevant data.</param>
        public Response(int status, Body body)
        {
            this.Status = status;
            this.Body = body;
        }
    }

    /// <summary>
    /// Represents the body of the response containing user-specific and system data.
    /// </summary>
    public class Body
    {
        /// <summary>
        /// Gets or sets the message from the server.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the version of the application.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the ID of the application user.
        /// </summary>
        [JsonProperty("idAppUser")]
        public int IdAppUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has set a password.
        /// </summary>
        [JsonProperty("hasSetPassword")]
        public bool HasSetPassword { get; set; }

        /// <summary>
        /// Gets or sets the token for the user's session.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the name of the application user.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the CleverPush channel ID.
        /// </summary>
        [JsonProperty("CleverPushChannelId")]
        public string CleverPushChannelId { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating whether the user receives push notifications.
        /// </summary>
        [JsonProperty("receivesPush")]
        public bool ReceivesPush { get; set; }

        /// <summary>
        /// Gets or sets the ID for the uploaded data.
        /// </summary>
        [JsonProperty("idUpload")]
        public int IdUpload { get; set; }

        /// <summary>
        /// Gets or sets the user's IBAN.
        /// </summary>
        [JsonProperty("iban")]
        public string Iban { get; set; }

        /// <summary>
        /// Gets or sets the number of entries associated with the user.
        /// </summary>
        [JsonProperty("numOfEntries")]
        public int NumOfEntries { get; set; }

        /// <summary>
        /// Gets or sets the survey state, indicating which surveys have been completed.
        /// </summary>
        [JsonProperty("surveys")]
        public SurveyState Survey { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Body"/> class with the provided details.
        /// </summary>
        /// <param name="message">The message from the server.</param>
        /// <param name="name">The user's name.</param>
        /// <param name="receivesPush">Indicates whether the user receives push notifications.</param>
        /// <param name="channelId">The CleverPush channel ID.</param>
        /// <param name="version">The version of the application.</param>
        /// <param name="upload">The ID of the uploaded data.</param>
        /// <param name="iban">The user's IBAN.</param>
        /// <param name="numEntries">The number of entries associated with the user.</param>
        /// <param name="idAppUser">The ID of the application user.</param>
        /// <param name="hasSetPassword">Indicates whether the user has set a password.</param>
        /// <param name="token">The session token for the user.</param>
        /// <param name="survey1">Indicates whether the user has completed the first survey.</param>
        /// <param name="survey">The state of the surveys.</param>
        public Body(string message, string name, bool receivesPush, string channelId, string version, int upload, string iban, int numEntries, int idAppUser, bool hasSetPassword, string token, bool survey1, SurveyState survey)
        {
            this.CleverPushChannelId = channelId;
            this.Message = message;
            this.IdAppUser = idAppUser;
            this.HasSetPassword = hasSetPassword;
            this.Token = token;
            this.Version = version;
            this.Iban = iban;
            this.IdUpload = upload;
            this.NumOfEntries = numEntries;
            this.Name = name;
            this.Survey = survey;
            this.ReceivesPush = receivesPush;
        }
    }

    /// <summary>
    /// Represents the state of the surveys, indicating which surveys have been completed.
    /// </summary>
    public class SurveyState
    {
        /// <summary>
        /// Gets or sets a value indicating whether survey 1 has been completed.
        /// </summary>
        [JsonProperty("survey1")]
        public bool Survey1 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether survey 2 has been completed.
        /// </summary>
        [JsonProperty("survey2")]
        public bool Survey2 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether survey 3 has been completed.
        /// </summary>
        [JsonProperty("survey3")]
        public bool Survey3 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether survey 4 has been completed.
        /// </summary>
        [JsonProperty("survey4")]
        public bool Survey4 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the end of the study has been reached.
        /// </summary>
        [JsonProperty("end_of_study_reached")]
        public bool EndOfStudy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyState"/> class with the provided survey completion states.
        /// </summary>
        /// <param name="survey1">Indicates whether survey 1 has been completed.</param>
        /// <param name="survey2">Indicates whether survey 2 has been completed.</param>
        /// <param name="survey3">Indicates whether survey 3 has been completed.</param>
        /// <param name="survey4">Indicates whether survey 4 has been completed.</param>
        /// <param name="endOfStudy">Indicates whether the end of the study has been reached.</param>
        public SurveyState(bool survey1, bool survey2, bool survey3, bool survey4, bool endOfStudy)
        {
            Survey1 = survey1;
            Survey2 = survey2;
            Survey3 = survey3;
            Survey4 = survey4;
            EndOfStudy = endOfStudy;
        }
    }

}