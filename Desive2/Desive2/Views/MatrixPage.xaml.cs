using Desive2.Models;
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
    public partial class MatrixPage : ContentPage
    {
        List<string> answersQuestion1 = new List<string>();  // List to store answers for question 1
        List<bool> questionsAnswered = new List<bool>();  // List to track if the questions have been answered
        bool skippedPreviousQuestion = false;  // Flag to check if the previous question was skipped
        Dictionary<string, string> answer = new Dictionary<string, string>();  // Dictionary to store answers

        public MatrixPage()
        {
            InitializeComponent();  // Initializes the page components
            SetDarkMode();  // Applies dark mode if needed
        }

        public MatrixPage(MatrixQuestion question)
        {
            this.BindingContext = new MatrixViewModel(question);  // Sets the binding context to MatrixViewModel with the given question
            InitializeComponent();  // Initializes the page components
            SetDarkMode();  // Applies dark mode if needed
            if (SurveyContent.SkippedPrevoiusQuestion)
            {
                skippedPreviousQuestion = true;  // Sets flag if previous question was skipped
                SurveyContent.SkippedPrevoiusQuestion = false;  // Resets the skipped question flag
            }
        }

        private void SetDarkMode()
        {
            if (Device.RuntimePlatform == Device.iOS)  // Checks if the platform is iOS
            {
                OSAppTheme theme = Application.Current.RequestedTheme;  // Gets the current theme of the application
                if (theme == OSAppTheme.Dark)  // If the theme is dark
                {
                    var sl = questionSet.Children.Where(x => x is StackLayout).ToList();  // Gets all StackLayouts inside the questionSet
                    foreach (var s in sl)
                    {
                        if (s is StackLayout)
                        {
                            var answers = ((StackLayout)s).Children.Where(x => x is RadioButton).ToList();  // Gets all RadioButtons inside the StackLayout
                            for (int i = 0; i < answers.Count; i++)
                            {
                                var rb = (RadioButton)answers[i];
                                rb.TextColor = Color.White;  // Sets the text color of RadioButton to white for dark mode
                            }
                        }
                    }
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            btnContinue.IsEnabled = false;  // Disables the continue button
            answer = GetQuestionsAnswer();  // Gets the answers for the questions

            //if (!SurveyContent.Matrix.ContainsKey(question.Text))
            //    SurveyContent.Matrix.Add(question.Text, answer);  // (Commented) Adds the question's answers to SurveyContent

            ValidateQuestionOne();  // Validates if the first question is answered

            if (questionsAnswered.Contains(false))  // If there are any unanswered questions
            {
                //SurveyContent.Matrix.Remove(question.Text);  // (Commented) Removes the current question's answer from SurveyContent
                questionsAnswered = new List<bool>();  // Resets the questionsAnswered list
                await App.Current.MainPage.DisplayAlert("Achtung", "Eine oder mehrere Pflichtfragen sind nicht beantwortet worden. Bitte beantworten Sie diese zuerst, um fortzufahren!", "Okay");  // Shows an alert
            }
            else
            {
                SurveyContent.SkippedPrevoiusQuestion = false;  // Resets the skipped previous question flag
                SurveyContent.GoToNextQuestion(SurveyContent.SurveyType, SurveyContent.SurveySection, this);  // Moves to the next question
            }
            btnContinue.IsEnabled = true;  // Enables the continue button again
        }

        private Dictionary<string, string> GetQuestionsAnswer()
        {
            Dictionary<string, string> answers = new Dictionary<string, string>();  // Creates a dictionary to store answers
            var sl = questionSet.Children.Where(x => x is StackLayout).ToList();  // Gets all StackLayouts inside the questionSet
            foreach (var s in sl)
            {
                if (s is StackLayout)
                {
                    string key = "";  // Key for the answer (question)
                    string value = "";  // Value for the answer (selected answer)
                    var children = ((StackLayout)s).Children.ToList();  // Gets all children of the StackLayout
                    foreach (var child in children)
                    {
                        if (child is Label)
                        {
                            var lbl = child as Label;
                            key = lbl.Text;  // Sets the key to the text of the label (question)
                        }
                        else if (child is StackLayout)
                        {
                            var rbCollection = child as StackLayout;
                            var rb = rbCollection.Children.Where(x => x is RadioButton).ToList();  // Gets all RadioButtons in the StackLayout
                            foreach (var r in rb)
                            {
                                var radioButton = r as RadioButton;
                                if (radioButton.IsChecked)
                                    value = radioButton.Content.ToString();  // Sets the value to the content of the selected RadioButton
                            }
                        }
                        if (key != "" && value != "")
                            answers.Add(key, value);  // Adds the question and answer to the dictionary
                    }
                }
            }
            return answers;  // Returns the dictionary of answers
        }

        private void ValidateQuestionOne()
        {
            var sl = questionSet.Children.Where(x => x is StackLayout).ToList();  // Gets all StackLayouts inside the questionSet
            foreach (var s in sl)
            {
                if (s is StackLayout)
                {
                    var children = ((StackLayout)s).Children.ToList();  // Gets all children of the StackLayout
                    foreach (var child in children)
                    {
                        if (child is StackLayout)
                        {
                            var rbCollection = child as StackLayout;
                            var rb = rbCollection.Children.Where(x => x is RadioButton).ToList();  // Gets all RadioButtons in the StackLayout
                            validateMatrix(rb);  // Validates the radio buttons
                        }
                    }
                }
            }
        }

        private void validateMatrix(List<View> answers)
        {
            for (int i = 0; i < answers.Count; i++)
            {
                var rb = (RadioButton)answers[i];
                if (rb.IsChecked)
                    return;  // Returns if the radio button is checked

                if (i + 1 == answers.Count)  // If it's the last radio button and not checked
                {
                    questionsAnswered.Add(false);  // Adds a 'false' to indicate the question wasn't answered
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnBackButtonPressed();  // Calls the OnBackButtonPressed method
        }

        protected override bool OnBackButtonPressed()
        {
            if (skippedPreviousQuestion)  // If the previous question was skipped
                SurveyContent.SurveyCount--;  // Decreases the survey count
            SurveyContent.GoToPrevious(SurveyContent.SurveyType, SurveyContent.SurveySection);  // Goes to the previous question
            return true;  // Prevents default back button behavior
        }

        public void GetAnswers()
        {
            SurveyContent.Matrix.Add(question.Text, answer);  // Adds the current question's answer to the survey matrix
        }
    }

}