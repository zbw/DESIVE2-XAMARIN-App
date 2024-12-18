using Android.Icu.Text;
using Desive2.Models;
using Desive2.Objects;
using Desive2.ViewModels.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desive2.Views.DiaryQuestions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Define the SingleChoicePage class
    public partial class SingleChoicePage
    {
        // List to track whether all questions are answered
        List<bool> questionsAnswered = new List<bool>();
        // List to track if each option has an associated entry field
        List<bool> hasEntry = new List<bool>();

        // Default constructor for the page, initializes components and sets dark mode
        public SingleChoicePage()
        {
            InitializeComponent();
            SetDarkMode();
        }

        // Constructor that accepts a SingleAnswerQuestion and sets the BindingContext, initializes components, 
        // and determines whether the back arrow is visible
        public SingleChoicePage(SingleAnswerQuestion question)
        {
            // Set the BindingContext to a new SingleChoiceViewModel
            this.BindingContext = new SingleChoiceViewModel(question);
            hasEntry = question.HasEntry;
            InitializeComponent();
            SetDarkMode();

            // Set the visibility of the back arrow based on the diary count
            if (SurveyContent.DiaryCount == 0)
                backArrow.IsVisible = false;
            else
                backArrow.IsVisible = true;
        }

        // Method to apply dark mode styling to radio buttons on iOS
        private void SetDarkMode()
        {
            // Check if the platform is iOS
            if (Device.RuntimePlatform == Device.iOS)
            {
                // Check the current application theme
                OSAppTheme theme = Application.Current.RequestedTheme;
                if (theme == OSAppTheme.Dark)
                {
                    // Apply white text color to all radio buttons in the answers list
                    var answers = slAnswers.Children.Where(x => x is RadioButton).ToList();
                    for (int i = 0; i < answers.Count; i++)
                    {
                        var rb = (RadioButton)answers[i];
                        rb.TextColor = Color.White;
                    }
                }
            }
        }

        // Method to get the selected answer from the radio buttons
        private string GetAnswer()
        {
            // Get all radio buttons in the rbGroup
            var answers = rbGroup.Children.Where(x => x is RadioButton).ToList();

            // Iterate through the radio buttons and find the checked one
            for (int i = 0; i < answers.Count; i++)
            {
                var rb = (RadioButton)answers[i];
                if (rb.IsChecked)
                {
                    // Set the appropriate diary path based on the selected radio button
                    if (SurveyContent.DiaryCount == 0)
                    {
                        if (i == 3)
                            SurveyContent.DiaryPath = DiaryPath.PathTwo;
                        else if (i == 4)
                            SurveyContent.DiaryPath = DiaryPath.PathThree;
                        else
                        {
                            SurveyContent.DiaryPath = DiaryPath.PathOne;
                        }
                    }

                    // If the selected radio button has an associated entry, append the entry text to the answer
                    if (hasEntry.Count > 0)
                    {
                        if (hasEntry[i])
                        {
                            if (!string.IsNullOrEmpty(entry.Text))
                                return rb.Content.ToString() + " " + entry.Text;
                            else
                                break;
                        }
                    }

                    return rb.Content.ToString();
                }
            }
            // If no answer is selected, mark the question as unanswered
            questionsAnswered.Add(false);
            return "";
        }

        // Event handler for the button click, processes the answer and proceeds accordingly
        private void Button_Clicked(object sender, EventArgs e)
        {
            string answer = GetAnswer();

            // Add the answer to the SurveyContent if it's not already added
            if (!SurveyContent.SingleChoice.ContainsKey(question.Text))
                SurveyContent.SingleChoice.Add(question.Text, answer);

            // If some questions are unanswered, show an alert
            if (questionsAnswered.Contains(false))
            {
                SurveyContent.SingleChoice.Remove(question.Text);
                questionsAnswered = new List<bool>();
                App.Current.MainPage.DisplayAlert("Achtung", "Eine oder mehrere Pflichtfragen sind nicht beantwortet worden. Bitte beantworten Sie diese zuerst, um fortzufahren!", "Okay");
            }
            else
            {
                // Otherwise, increment the diary count and proceed to the next diary entry
                SurveyContent.DiaryCount++;
                SurveyContent.GoToNextDiary(SurveyContent.DiaryPath);
            }
        }

        // Event handler for the tap gesture recognizer to navigate back
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        // Override OnBackButtonPressed to navigate to the previous diary entry
        protected override bool OnBackButtonPressed()
        {
            SurveyContent.GoToPreviousDiary(SurveyContent.DiaryPath);
            return true;
        }

        // Event handler for when a radio button is checked or unchecked
        private void RadioButton_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
            // Check if there are entry fields associated with the selected radio buttons
            if (hasEntry.Count > 0)
            {
                var answers = rbGroup.Children.Where(x => x is RadioButton).ToList();
                for (int i = 0; i < answers.Count; i++)
                {
                    var rb = (RadioButton)answers[i];

                    // If the radio button is checked and has an associated entry field, show the entry field
                    if (rb.IsChecked && hasEntry[i])
                    {
                        entry.IsVisible = true;
                        entry.Focus();
                    }
                    else
                    {
                        // Otherwise, hide the entry field
                        entry.IsVisible = false;
                        entry.Unfocus();
                    }
                }
            }
        }
    }

}