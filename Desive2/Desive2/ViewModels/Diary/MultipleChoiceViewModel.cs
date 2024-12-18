using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Desive2.ViewModels.Diary
{
    /// <summary>
    /// ViewModel for managing the data and logic related to a MultipleChoiceQuestion in a survey.
    /// </summary>
    public class MultipleChoiceViewModel : BindableObject
    {
        /// <summary>
        /// Gets or sets the multiple choice question associated with this view model.
        /// </summary>
        public MultipleChoiceQuestion Question { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of answers, where the key is the answer text, 
        /// and the value indicates whether the answer is selected.
        /// </summary>
        public Dictionary<string, bool> Answers { get; set; }

        /// <summary>
        /// Gets or sets additional meta information for the question.
        /// </summary>
        public string MetaText { get; set; }

        /// <summary>
        /// Gets or sets the text of the question.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets any additional text for the question.
        /// </summary>
        public string Addition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the question requires an entry (i.e., a text input).
        /// </summary>
        public bool HasEntry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entry option is checked.
        /// </summary>
        public bool IsEntryChecked { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the user continues to the next step.
        /// </summary>
        public ICommand Continue { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the user selects an answer.
        /// </summary>
        public ICommand Select { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleChoiceViewModel"/> class with the provided question.
        /// Populates the answers and additional information from the question.
        /// </summary>
        /// <param name="question">The multiple choice question.</param>
        public MultipleChoiceViewModel(MultipleChoiceQuestion question)
        {
            Answers = new Dictionary<string, bool>();
            Question = question;

            // Add answers to the dictionary, associating the answer text with whether it has an entry.
            for (int i = 0; i < Question.Answers.Count; i++)
            {
                Answers.Add(Question.Answers[i], Question.HasEntry[i]);
            }

            // Set the metadata, question text, and additional text from the question.
            MetaText = Question.MetaText;
            QuestionText = Question.QuestionText;
            Addition = Question.Addition;
        }
    }

}
