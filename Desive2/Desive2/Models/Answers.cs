using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Desive2.Models
{
    public class Answers
    {
        // Represents the ID of the question being answered.
        [JsonProperty("QuestionID")]
        public string QuestionID { get; set; }

        // Stores the text of the answer.
        [JsonProperty("Answer_Text")]
        public string Answer { get; set; }
    }
}

