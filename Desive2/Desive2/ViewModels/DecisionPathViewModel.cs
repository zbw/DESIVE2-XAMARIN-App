using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    /// <summary>
    /// ViewModel for handling the decision path, including controlling the visibility of different UI elements based on the current question.
    /// </summary>
    public class DecisionPathViewModel : BindableObject
    {
        // Private fields for managing the profile picture and survey state.
        private bool isContinueVisible = true;
        private bool isSendVisible = false;
        private string headingText = "Was beschreibt das Bild, das Sie mit uns geteilt haben, am besten?";
        private bool isEditorVisible = false;
        private bool isRb1Visible = true;
        private bool isRb2Visible = false;
        private bool isRb3Visible = false;
        private bool isRb4Visible = false;
        private bool isRb5Visible = false;

        // Public properties that bind to the UI and change based on the user's interactions.

        /// <summary>
        /// Gets or sets the profile picture.
        /// </summary>
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the command for sending the survey.
        /// </summary>
        public ICommand SendSurveyCommand { get; set; }

        /// <summary>
        /// Gets or sets the command for continuing to the next step in the survey.
        /// </summary>
        public ICommand ContinueCommand { get; set; }

        /// <summary>
        /// Gets or sets the current question number.
        /// </summary>
        public int CurrentQuestion { get; set; }

        /// <summary>
        /// Gets or sets the current answer for the question.
        /// </summary>
        public Dictionary<string, string> CurrentAnswer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the continue button is visible.
        /// </summary>
        public bool IsContinueVisible
        {
            get { return isContinueVisible; }
            set { isContinueVisible = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the send button is visible.
        /// </summary>
        public bool IsSendVisible
        {
            get { return isSendVisible; }
            set { isSendVisible = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the heading text based on the current question.
        /// </summary>
        public string HeadingText
        {
            get
            {
                // Update heading text based on the current question number.
                if (CurrentQuestion == 1)
                {
                    headingText = "Was beschreibt das Bild, das Sie mit uns geteilt haben, am besten?";
                }
                else if (CurrentQuestion == 2)
                {
                    headingText = "Fällt Ihnen noch etwas zum Bild ein? Möchten Sie noch etwas hinzufügen? Wenn nicht, bitte einfach auf \"Absenden\" tippen.";
                }
                return headingText;
            }
            set
            {
                headingText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the editor is visible.
        /// </summary>
        public bool IsEditorVisible
        {
            get
            {
                // Show editor for specific questions.
                if (CurrentQuestion == 6)
                {
                    isEditorVisible = true;
                }
                else if (CurrentQuestion == 2)
                {
                    isEditorVisible = false;
                }
                return isEditorVisible;
            }
            set
            {
                isEditorVisible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the first radio button (Rb1) is visible.
        /// </summary>
        public bool IsRb1Visible
        {
            get
            {
                // Make radio button visible for question 1.
                if (CurrentQuestion == 1)
                {
                    isRb1Visible = true;
                }
                else
                {
                    isRb1Visible = false;
                }
                return isRb1Visible;
            }
            set
            {
                isRb1Visible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the second radio button (Rb2) is visible.
        /// </summary>
        public bool IsRb2Visible
        {
            get
            {
                // Make radio button visible for question 2.
                if (CurrentQuestion == 2)
                {
                    isRb2Visible = true;
                }
                else
                {
                    isRb2Visible = false;
                }
                return isRb2Visible;
            }
            set
            {
                isRb2Visible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the third radio button (Rb3) is visible.
        /// </summary>
        public bool IsRb3Visible
        {
            get
            {
                // Make radio button visible for question 3.
                if (CurrentQuestion == 3)
                {
                    isRb3Visible = true;
                }
                else
                {
                    isRb3Visible = false;
                }
                return isRb3Visible;
            }
            set
            {
                isRb3Visible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the fourth radio button (Rb4) is visible.
        /// </summary>
        public bool IsRb4Visible
        {
            get
            {
                // Make radio button visible for question 4.
                if (CurrentQuestion == 4)
                {
                    isRb4Visible = true;
                }
                else
                {
                    isRb4Visible = false;
                }
                return isRb4Visible;
            }
            set
            {
                isRb4Visible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the fifth radio button (Rb5) is visible.
        /// </summary>
        public bool IsRb5Visible
        {
            get
            {
                // Make radio button visible for question 5.
                if (CurrentQuestion == 5)
                {
                    isRb5Visible = true;
                }
                else
                {
                    isRb5Visible = false;
                }
                return isRb5Visible;
            }
            set
            {
                isRb5Visible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the DecisionPathViewModel and sets the initial question.
        /// </summary>
        public DecisionPathViewModel()
        {
            CurrentQuestion = 1;  // Set the starting question to 1
        }
    }

}
