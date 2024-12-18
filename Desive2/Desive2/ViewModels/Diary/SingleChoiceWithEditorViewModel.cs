using Desive2.Objects;
using Desive2.Views.DiaryQuestions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Desive2.ViewModels.Diary
{
    /// <summary>
    /// ViewModel for managing the data and logic related to a SingleChoiceWithEditor question in a survey.
    /// </summary>
    public class SingleChoiceWithEditorViewModel : BindableObject
    {
        /// <summary>
        /// Gets or sets the multiple-choice with editor question associated with this view model.
        /// </summary>
        public MultpleChoiceWithEditor Question { get; set; }

        /// <summary>
        /// Gets or sets the list of answers, where each answer is represented as a tuple containing the answer text and editor value.
        /// </summary>
        public List<Tuple<string, string>> Answers { get; set; }

        /// <summary>
        /// Gets or sets any additional meta information for the question.
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
        /// Gets or sets the command that will be executed when the user continues to the next step.
        /// </summary>
        public ICommand Continue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleChoiceWithEditorViewModel"/> class with the provided question.
        /// </summary>
        /// <param name="question">The multiple-choice with editor question.</param>
        public SingleChoiceWithEditorViewModel(MultpleChoiceWithEditor question)
        {
            Question = question;
            MetaText = Question.MetaText;
            QuestionText = Question.QuestionText;
            Addition = Question.Addition;
            Answers = Question.Answers;
        }
    }

}
