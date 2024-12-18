using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    // ViewModel for managing matrix-style survey questions.
    public class MatrixViewModel : BindableObject
    {
        // MatrixQuestion property holds the question data.
        public MatrixQuestion Question { get; set; }

        // Dictionary to hold the question set, mapping question text to a list of possible answers.
        public Dictionary<string, List<string>> QuestionSet { get; set; }

        // List of answers selected for the matrix.
        public List<string> Answers = new List<string>();

        // List of questions for the matrix.
        public List<string> MatrixQuestions { get; set; }

        // Meta information for the matrix question.
        public string MetaText { get; set; }

        // Main question text.
        public string QuestionText { get; set; }

        // Additional text for the matrix.
        public string Addition { get; set; }

        // Ranking information for the matrix question.
        public string Ranking { get; set; }

        // Boolean indicating if the entry is checked.
        public bool IsEntryChecked { get; set; }

        // Command to proceed to the next action.
        public ICommand Continue { get; set; }

        // Default constructor initializing the matrix with data from SurveyLibraries.
        public MatrixViewModel()
        {
            MatrixQuestions = new List<string>();
            QuestionSet = new Dictionary<string, List<string>>();

            // Using the first question from SurveyOne.SectionThree as the default question.
            Question = (MatrixQuestion)SurveyLibraries.SurveyOne.SectionThree.Questions[0];

            // Populate the QuestionSet dictionary with answers from the MatrixQuestionAnswers.
            for (int i = 0; i < Question.MatrixQuestionAnswers.Count; i++)
            {
                Answers = new List<string>();
                for (int j = 0; j < Question.MatrixQuestionAnswers[i].Answers.Count; j++)
                {
                    Answers.Add(Question.MatrixQuestionAnswers[i].Answers[j]);
                }
                QuestionSet.Add(Question.MatrixQuestionAnswers[i].QuestionText, Answers);
            }

            // Extracting metadata from the matrix question.
            MetaText = Question.MetaText;
            QuestionText = Question.QuestionText;
            Addition = Question.Addition;
            Ranking = Question.Ranking;
        }

        // Constructor allowing the passing of a specific MatrixQuestion.
        public MatrixViewModel(MatrixQuestion question)
        {
            MatrixQuestions = new List<string>();
            QuestionSet = new Dictionary<string, List<string>>();
            Question = question;

            // Populate the QuestionSet dictionary with answers from the MatrixQuestionAnswers.
            for (int i = 0; i < Question.MatrixQuestionAnswers.Count; i++)
            {
                Answers = new List<string>();
                for (int j = 0; j < Question.MatrixQuestionAnswers[i].Answers.Count; j++)
                {
                    Answers.Add(Question.MatrixQuestionAnswers[i].Answers[j]);
                }
                QuestionSet.Add(Question.MatrixQuestionAnswers[i].QuestionText, Answers);
            }

            // Extracting metadata from the matrix question.
            MetaText = Question.MetaText;
            QuestionText = Question.QuestionText;
            Addition = Question.Addition;
            Ranking = Question.Ranking;
        }
    }

}
