using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Desive2.ViewModels.Diary
{
    /// <summary>
    /// ViewModel for managing the data and logic related to a SingleChoiceQuestion in a survey.
    /// </summary>
    public class SingleChoiceViewModel : BindableObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the single-answer question associated with this view model.
        /// </summary>
        public SingleAnswerQuestion Question { get; set; }

        /// <summary>
        /// Gets or sets the list of possible answers for the question.
        /// </summary>
        public List<string> Answers { get; set; }

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

        private bool _isRadioButtonChecked;

        /// <summary>
        /// Gets or sets a value indicating whether the radio button for the question is checked.
        /// </summary>
        public bool IsRadioButtonChecked
        {
            get { return _isRadioButtonChecked; }
            set
            {
                if (_isRadioButtonChecked != value)
                {
                    _isRadioButtonChecked = value;
                    OnPropertyChanged(nameof(IsRadioButtonChecked));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleChoiceViewModel"/> class with the provided question.
        /// </summary>
        /// <param name="question">The single-answer question.</param>
        public SingleChoiceViewModel(SingleAnswerQuestion question)
        {
            Answers = question.Answers;
            MetaText = question.MetaText;
            QuestionText = question.QuestionText;
            Addition = question.Addition;
        }
    }

}
