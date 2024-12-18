using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Desive2.Objects
{
    /// <summary>
    /// Represents a survey with different types of questions, such as single-choice, multiple-choice, and matrix questions.
    /// </summary>
    public class Survey
    {
        /// <summary>
        /// Gets or sets the dictionary of single-choice questions, where each key is the question identifier and each value is the selected answer.
        /// </summary>
        public Dictionary<string, string> SingleChoice { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of multiple-choice questions, where each key is the question identifier and each value is a list of selected answers.
        /// </summary>
        public Dictionary<string, List<string>> MultipleChoice { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of matrix questions, where each key is the question identifier and each value is a dictionary of answers.
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Matrix { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Survey"/> class with empty dictionaries for single-choice, multiple-choice, and matrix questions.
        /// </summary>
        public Survey()
        {
            SingleChoice = new Dictionary<string, string>();
            MultipleChoice = new Dictionary<string, List<string>>();
            Matrix = new Dictionary<string, Dictionary<string, string>>();
        }

        /// <summary>
        /// Serializes the survey into a JSON string by combining the single-choice, multiple-choice, and matrix question data.
        /// </summary>
        /// <returns>A JSON string representing the entire survey.</returns>
        public string GetJson()
        {
            // Serialize each dictionary to JSON
            string sc = JsonConvert.SerializeObject(SingleChoice);
            string mc = JsonConvert.SerializeObject(MultipleChoice);
            string matrix = JsonConvert.SerializeObject(Matrix);

            // Parse each serialized JSON string into JObject
            JObject o1 = JObject.Parse(sc);
            JObject o2 = JObject.Parse(mc);
            JObject o3 = JObject.Parse(matrix);

            // Merge the multiple-choice and matrix question JSON into the single-choice JSON
            o1.Merge(o2);
            o1.Merge(o3);

            // Return the merged result as a string
            return o1.ToString();
        }
    }


}
