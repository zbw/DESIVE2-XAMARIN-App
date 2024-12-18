using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    // ViewModel for managing multiple-choice questions in surveys.
    public class MultipleChoiceViewModel : BindableObject
    {
        // The MultipleChoiceQuestion property holds the question data.
        public MultipleChoiceQuestion Question { get; set; }

        // Dictionary mapping answer options to a boolean indicating whether the entry is checked.
        public Dictionary<string, bool> Answers { get; set; }

        // Meta information for the multiple-choice question.
        public string MetaText { get; set; }

        // Main question text.
        public string QuestionText { get; set; }

        // Additional text related to the question.
        public string Addition { get; set; }

        // Boolean indicating if the entry is checked.
        public bool IsEntryChecked { get; set; }

        // Command to proceed to the next action.
        public ICommand Continue { get; set; }

        // Command to handle the selection of an answer.
        public ICommand Select { get; set; }

        // Default constructor initializing the multiple-choice question with data from SurveyLibraries.
        public MultipleChoiceViewModel()
        {
            // Using the current survey question based on the SurveyCount.
            Question = (MultipleChoiceQuestion)SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyContent.SurveyCount];
        }

        // Constructor allowing the passing of a specific MultipleChoiceQuestion.
        public MultipleChoiceViewModel(MultipleChoiceQuestion question)
        {
            Question = question;
            Answers = new Dictionary<string, bool>();

            // Populate the Answers dictionary with the question's answer options and their checked status.
            for (int i = 0; i < Question.Answers.Count; i++)
            {
                Answers.Add(Question.Answers[i], Question.HasEntry[i]);
            }

            // Extracting metadata from the multiple-choice question.
            MetaText = Question.MetaText;
            QuestionText = Question.QuestionText;
            Addition = Question.Addition;
        }
    }

}
