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
    public partial class MultipleChoicePage : ContentPage
    {
        List<bool> questionsAnswered = new List<bool>();  // List to track if the questions have been answered
        MultipleChoiceQuestion mq;  // The current multiple choice question
        bool skippedPreviousQuestion = false;  // Flag to check if the previous question was skipped
        List<string> answers = new List<string>();  // List to store the answers for the multiple choice question

        public MultipleChoicePage()
        {
            InitializeComponent();  // Initializes the page components
        }

        public MultipleChoicePage(MultipleChoiceQuestion question)
        {
            this.BindingContext = new MultipleChoiceViewModel(question);  // Sets the binding context to MultipleChoiceViewModel with the given question
            mq = question;  // Stores the question in the mq variable
            InitializeComponent();  // Initializes the page components
            if (SurveyContent.SkippedPrevoiusQuestion)
            {
                skippedPreviousQuestion = true;  // Sets the flag if the previous question was skipped
                SurveyContent.SkippedPrevoiusQuestion = false;  // Resets the skipped question flag
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            btnContinue.IsEnabled = false;  // Disables the continue button
            answers = GetQuestionAnswers();  // Gets the answers for the multiple choice question

            //if (!SurveyContent.MultipleChoice.ContainsKey(question.Text))
            //    SurveyContent.MultipleChoice.Add(question.Text, answers);  // (Commented) Adds the question's answers to SurveyContent

            if (questionsAnswered.Contains(false))  // If any required questions are unanswered
            {
                SurveyContent.MultipleChoice.Remove(question.Text);  // (Commented) Removes the current question's answers from SurveyContent
                questionsAnswered = new List<bool>();  // Resets the questionsAnswered list
                App.Current.MainPage.DisplayAlert("Achtung", "Eine oder mehrere Pflichtfragen sind nicht beantwortet worden. Bitte beantworten Sie diese zuerst, um fortzufahren!", "Okay");  // Displays an alert
            }
            else
            {
                SurveyContent.GoToNextQuestion(SurveyContent.SurveyType, SurveyContent.SurveySection, this);  // Moves to the next question
            }

            btnContinue.IsEnabled = true;  // Enables the continue button again
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnBackButtonPressed();  // Calls the OnBackButtonPressed method
        }

        protected override bool OnBackButtonPressed()
        {
            if (skippedPreviousQuestion)  // If the previous question was skipped
                SurveyContent.SurveyCount--;  // Decreases the survey count
            SurveyContent.GoToPrevious(SurveyContent.SurveyType, SurveyContent.SurveySection);  // Moves to the previous question
            return true;  // Prevents the default back button behavior
        }

        private List<string> GetQuestionAnswers()
        {
            List<string> answers = new List<string>();  // Creates a list to store answers
            var views = cbGroup.Children.ToList();  // Gets all children of the cbGroup (likely a parent container)
            foreach (StackLayout sl in views)
            {
                for (int i = 0; i < sl.Children.Count; i += 2)  // Loops through the children of the StackLayout
                {
                    if (sl.Children[i] is CheckBox)  // If the child is a CheckBox
                    {
                        var cb = sl.Children[i] as CheckBox;
                        if (cb.IsChecked)  // If the checkbox is checked
                        {
                            var lbl = sl.Children[i + 1] as Label;  // Gets the label next to the checkbox
                            string answer = lbl.Text.ToString();  // Sets the answer from the label's text
                            var rb = sl.Children[i + 3] as RadioButton;  // Gets the radio button in the layout
                            if (rb.IsChecked)  // If the radio button is checked
                            {
                                var entry = sl.Children[i + 2] as Entry;  // Gets the entry field
                                if (!string.IsNullOrEmpty(entry.Text))  // If the entry is not empty
                                    answer += " " + entry.Text.ToString();  // Adds the entry text to the answer
                                else
                                    questionsAnswered.Add(false);  // Adds false to questionsAnswered if the entry is empty
                            }
                            answers.Add(answer);  // Adds the answer to the answers list
                            questionsAnswered.Add(true);  // Marks this question as answered
                        }
                    }
                }
            }
            if (answers.Count == 0)  // If no answers were selected
            {
                questionsAnswered.Add(false);  // Adds false to indicate that no answers were selected
            }

            return answers;  // Returns the list of answers
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            SetEditorActive(sender as CheckBox);  // Calls SetEditorActive when a checkbox is checked or unchecked
            if (mq.SetsInactive.Contains(true))  // Checks if there are any sets to be made inactive
            {
                SetInactive();  // Calls SetInactive to deactivate certain checkboxes
            }
        }

        private void SetEditorActive(CheckBox cb)
        {
            if (cb.IsChecked)  // If the checkbox is checked
            {
                var parent = cb.Parent as StackLayout;  // Gets the parent StackLayout
                var rb = parent.Children[3] as RadioButton;  // Gets the radio button in the layout

                if (rb.IsChecked)  // If the radio button is checked
                {
                    var entry = parent.Children[2] as Entry;  // Gets the entry field
                    entry.IsVisible = true;  // Makes the entry visible
                }
            }
            else  // If the checkbox is unchecked
            {
                var parent = cb.Parent as StackLayout;  // Gets the parent StackLayout
                var rb = parent.Children[3] as RadioButton;  // Gets the radio button in the layout

                if (rb.IsChecked)  // If the radio button is checked
                {
                    var entry = parent.Children[2] as Entry;  // Gets the entry field
                    entry.IsVisible = false;  // Hides the entry
                }
            }
        }

        private void SetInactive()
        {
            var slCollection = cbGroup.Children.Where(x => x is StackLayout).ToList();  // Gets all StackLayouts inside the cbGroup
            for (int i = 0; i < slCollection.Count; i++)  // Loops through the StackLayouts
            {
                var view = slCollection[i];
                var sl = view as StackLayout;
                var cb = sl.Children[0] as CheckBox;  // Gets the checkbox
                var lbl = sl.Children[1] as Label;  // Gets the label
                if (cb.IsChecked && mq.SetsInactive[i])  // If the checkbox is checked and the index is in the SetsInactive list
                {
                    SetCheckboxesInactive(i);  // Calls SetCheckboxesInactive to deactivate other checkboxes
                    break;
                }
                if (!cb.IsChecked && mq.SetsInactive[i])  // If the checkbox is unchecked and the index is in the SetsInactive list
                {
                    SetCheckboxesActive(i);  // Calls SetCheckboxesActive to reactivate other checkboxes
                    break;
                }
            }
        }

        private void SetCheckboxesInactive(int indexToSkip)
        {
            var slCollection = cbGroup.Children.Where(x => x is StackLayout).ToList();  // Gets all StackLayouts inside the cbGroup
            for (int i = 0; i < slCollection.Count; i++)  // Loops through the StackLayouts
            {
                if (i != indexToSkip)  // If it's not the checkbox to skip
                {
                    var view = slCollection[i];
                    var stackLayout = view as StackLayout;
                    var cb = stackLayout.Children[0] as CheckBox;  // Gets the checkbox
                    cb.IsChecked = false;  // Unchecks the checkbox
                }
            }
        }

        private void SetCheckboxesActive(int indexToSkip)
        {
            var slCollection = cbGroup.Children.Where(x => x is StackLayout).ToList();  // Gets all StackLayouts inside the cbGroup
            for (int i = 0; i < slCollection.Count; i++)  // Loops through the StackLayouts
            {
                if (i != indexToSkip)  // If it's not the checkbox to skip
                {
                    var view = slCollection[i];
                    var stackLayout = view as StackLayout;
                    var cb = stackLayout.Children[0] as CheckBox;  // Gets the checkbox
                    cb.IsEnabled = true;  // Enables the checkbox
                }
            }
        }

        public void GetAnswers()
        {
            SurveyContent.MultipleChoice.Add(question.Text, answers);  // Adds the multiple choice answers to the SurveyContent
        }
    }

}