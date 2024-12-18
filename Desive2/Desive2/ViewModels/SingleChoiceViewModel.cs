using Desive2.Models;
using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    public class SingleChoiceViewModel : BindableObject, INotifyPropertyChanged
    {
        // Property to hold the current question for single choice selection.
        public SingleAnswerQuestion Question { get; set; }

        // List of possible answers for the single choice question.
        public List<string> Answers { get; set; }

        // Meta text associated with the question, used for additional context.
        public string MetaText { get; set; }

        // The actual question text for the single choice question.
        public string QuestionText { get; set; }

        // Additional information or context for the question.
        public string Addition { get; set; }

        // Command to handle the continuation action, typically used after an answer is selected.
        public ICommand Continue { get; set; }

        // Private backing field for the radio button's checked state.
        private bool _isRadioButtonChecked;

        // Property to get and set the checked state of the radio button.
        public bool IsRadioButtonChecked
        {
            get { return _isRadioButtonChecked; } // Returns the current checked state.
            set
            {
                if (_isRadioButtonChecked != value) // If the checked state changes.
                {
                    _isRadioButtonChecked = value; // Update the checked state.
                    OnPropertyChanged(nameof(IsRadioButtonChecked)); // Notify that the property has changed.
                }
            }
        }

        // Constructor that initializes the ViewModel with data from the survey.
        public SingleChoiceViewModel()
        {
            // Initializes the Question and other properties using data from the Survey.
            Question = (SingleAnswerQuestion)SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyContent.SurveyCount];
            Answers = Question.Answers; // Assign possible answers to the Answers list.
            MetaText = Question.MetaText; // Assign meta text to the MetaText property.
            QuestionText = Question.QuestionText; // Assign question text to the QuestionText property.
            Addition = Question.Addition; // Assign additional information to the Addition property.
        }

        // Constructor that initializes the ViewModel with a specific question object.
        public SingleChoiceViewModel(SingleAnswerQuestion question)
        {
            Answers = question.Answers; // Assign possible answers to the Answers list.
            MetaText = question.MetaText; // Assign meta text to the MetaText property.
            QuestionText = question.QuestionText; // Assign question text to the QuestionText property.
            Addition = question.Addition; // Assign additional information to the Addition property.
        }
    }

}

