using Desive2.Objects;
using Desive2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desive2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleChoicePage
    {
        // Lists to track answered questions, questions with entry fields, and skip logic
        List<bool> questionsAnswered = new List<bool>();
        List<bool> hasEntry = new List<bool>();
        bool skipsNextQuestion = false;
        bool skippedPreviousQuestion = false;
        string answer = "";
        List<bool> hasSkipQuestion = new List<bool>();

        // Default constructor
        public SingleChoicePage()
        {
            InitializeComponent();  // Initializes the page's components
            SetDarkMode();  // Sets the dark mode theme if applicable
        }

        // Constructor with a SingleAnswerQuestion parameter to initialize specific data
        public SingleChoicePage(SingleAnswerQuestion question)
        {
            this.BindingContext = new SingleChoiceViewModel(question);  // Sets the BindingContext to the provided ViewModel
            hasEntry = question.HasEntry;  // Sets entry visibility for questions with additional input fields
            hasSkipQuestion = question.SkipsNextQuestion;  // Tracks questions that skip to the next one
            InitializeComponent();  // Initializes the page's components
            SetDarkMode();  // Sets the dark mode theme if applicable

            // Checks if the previous question was skipped and updates the flag
            if (SurveyContent.SkippedPrevoiusQuestion)
            {
                skippedPreviousQuestion = true;
                SurveyContent.SkippedPrevoiusQuestion = false;
            }
        }

        // Sets dark mode styling for the page
        private void SetDarkMode()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                OSAppTheme theme = Application.Current.RequestedTheme;  // Gets the current app theme
                if (theme == OSAppTheme.Dark)
                {
                    // Adjusts the color of radio button text to white for dark theme
                    var answers = slAnswers.Children.Where(x => x is RadioButton).ToList();
                    for (int i = 0; i < answers.Count; i++)
                    {
                        var rb = (RadioButton)answers[i];
                        rb.TextColor = Color.White;
                    }
                }
            }
        }

        // Retrieves the selected answer from the radio buttons
        private string GetAnswer()
        {
            var answers = rbGroup.Children.Where(x => x is RadioButton).ToList();  // Gets all radio buttons in the group
            for (int i = 0; i < answers.Count; i++)
            {
                var rb = (RadioButton)answers[i];
                // If the question has no skip logic
                if (!hasSkipQuestion.Contains(true))
                {
                    if (hasEntry.Count > 0)
                    {
                        // If entry is visible and an option is selected
                        if (rb.IsChecked)
                        {
                            if (hasEntry[i])
                            {
                                if (!string.IsNullOrEmpty(entry.Text))
                                    return rb.Content.ToString() + " " + entry.Text;  // Returns the answer with the entry text
                                else
                                    questionsAnswered.Add(false);  // Adds a flag for unanswered question
                            }
                            else
                                return rb.Content.ToString();  // Returns the selected radio button text
                        }
                    }
                    else if (rb.IsChecked)
                        return rb.Content.ToString();  // Returns the selected radio button text if no entry
                }
                else
                {
                    // Skip question logic
                    if (rb.IsChecked && hasSkipQuestion[i])
                    {
                        skipsNextQuestion = true;
                        return rb.Content.ToString();  // Skips the next question
                    }
                    else if (rb.IsChecked)
                    {
                        skipsNextQuestion = false;
                        return rb.Content.ToString();  // No skip, just return the answer
                    }
                }
            }
            questionsAnswered.Add(false);  // Marks question as unanswered
            return "";
        }

        // Handles the click event of the continue button
        private void Button_Clicked(object sender, EventArgs e)
        {
            btnContinue.IsEnabled = false;  // Disables the continue button to prevent multiple clicks
            answer = GetAnswer();  // Retrieves the answer

            // Checks if any required questions are unanswered
            if (questionsAnswered.Contains(false))
            {
                SurveyContent.SingleChoice.Remove(question.Text);  // Removes the unanswered question from the answers
                questionsAnswered = new List<bool>();  // Resets the answered list
                App.Current.MainPage.DisplayAlert("Achtung", "Eine oder mehrere Pflichtfragen sind nicht beantwortet worden. Bitte beantworten Sie diese zuerst, um fortzufahren!", "Okay");  // Displays an alert
            }
            else
            {
                // If the next question is skipped, set the flag and navigate
                if (skipsNextQuestion)
                {
                    SurveyContent.SkippedPrevoiusQuestion = true;
                    SurveyContent.SurveyCount++;  // Increments the survey count
                    SurveyContent.GoToNextQuestion(SurveyContent.SurveyType, SurveyContent.SurveySection, this);  // Navigates to the next question
                }
                else
                {
                    SurveyContent.SkippedPrevoiusQuestion = false;  // Resets the skip flag
                    SurveyContent.GoToNextQuestion(SurveyContent.SurveyType, SurveyContent.SurveySection, this);  // Navigates to the next question
                }
            }
            btnContinue.IsEnabled = true;  // Re-enables the continue button
        }

        // Handles the tap gesture event (back navigation)
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnBackButtonPressed();  // Handles back button press
        }

        // Handles the back button press event
        protected override bool OnBackButtonPressed()
        {
            if (skippedPreviousQuestion)
                SurveyContent.SurveyCount--;  // Decrements the survey count if a previous question was skipped
            SurveyContent.GoToPrevious(SurveyContent.SurveyType, SurveyContent.SurveySection);  // Navigates to the previous question
            return true;  // Prevents the default back button behavior
        }

        // Handles the radio button checked change event
        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (hasEntry.Count > 0)
            {
                var answers = rbGroup.Children.Where(x => x is RadioButton).ToList();  // Gets all radio buttons in the group
                for (int i = 0; i < answers.Count; i++)
                {
                    var rb = (RadioButton)answers[i];

                    // If the radio button is checked and has an entry, show the entry field
                    if (rb.IsChecked && hasEntry[i])
                    {
                        entry.IsVisible = true;  // Makes the entry field visible
                        entry.Focus();  // Focuses on the entry field
                    }
                    else
                    {
                        entry.IsVisible = false;  // Hides the entry field if not checked
                        entry.Unfocus();  // Removes focus from the entry field
                    }
                }
            }
        }

        // Adds the selected answer to the SurveyContent dictionary
        public void GetAnswers()
        {
            SurveyContent.SingleChoice.Add(question.Text, answer);  // Adds the answer to the dictionary
        }
    }

}