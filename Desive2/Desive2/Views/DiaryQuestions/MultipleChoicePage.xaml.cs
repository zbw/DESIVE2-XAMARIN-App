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
    // Define the MultipleChoicePage class as a ContentPage
    public partial class MultipleChoicePage : ContentPage
    {
        // List to track whether all questions are answered
        List<bool> questionsAnswered = new List<bool>();

        // Default constructor for the page, initializes components
        public MultipleChoicePage()
        {
            InitializeComponent();
        }

        // Constructor that accepts a MultipleChoiceQuestion object, sets the BindingContext, and initializes components
        public MultipleChoicePage(MultipleChoiceQuestion question)
        {
            this.BindingContext = new MultipleChoiceViewModel(question);
            InitializeComponent();
        }

        // Event handler for the button click, checks if all required questions are answered and proceeds accordingly
        private void Button_Clicked(object sender, EventArgs e)
        {
            // Get the list of answers for the multiple-choice question
            List<string> answer4 = GetQuestionAnswers();

            // If the question is not already answered, add it to the survey content
            if (!SurveyContent.MultipleChoice.ContainsKey(question.Text))
                SurveyContent.MultipleChoice.Add(question.Text, answer4);

            // If there are unanswered required questions, remove the question from the survey content and show a warning
            if (questionsAnswered.Contains(false))
            {
                SurveyContent.MultipleChoice.Remove(question.Text);
                questionsAnswered = new List<bool>();
                App.Current.MainPage.DisplayAlert("Achtung", "Eine oder mehrere Pflichtfragen sind nicht beantwortet worden. Bitte beantworten Sie diese zuerst, um fortzufahren!", "Okay");
            }
            else
            {
                // Increment the diary count and move to the next diary entry
                SurveyContent.DiaryCount++;
                SurveyContent.GoToNextDiary(SurveyContent.DiaryPath);
            }
        }

        // Method to get the answers from the multiple-choice question, including handling checkboxes, radio buttons, and entries
        private List<string> GetQuestionAnswers()
        {
            List<string> answers = new List<string>();
            var views = cbGroup.Children.ToList();

            // Loop through the elements in the group to check answers
            foreach (StackLayout sl in views)
            {
                for (int i = 0; i < sl.Children.Count; i += 2)
                {
                    // If the element is a checkbox, check if it's selected and add the corresponding answer
                    if (sl.Children[i] is CheckBox)
                    {
                        var cb = sl.Children[i] as CheckBox;
                        if (cb.IsChecked)
                        {
                            var lbl = sl.Children[i + 1] as Label;
                            string answer = lbl.Text.ToString();

                            // If there's a selected radio button, check if an entry is filled out
                            var rb = sl.Children[i + 3] as RadioButton;
                            if (rb.IsChecked)
                            {
                                var entry = sl.Children[i + 2] as Entry;
                                if (!string.IsNullOrEmpty(entry.Text))
                                    answer += " " + entry.Text.ToString();
                                else
                                    questionsAnswered.Add(false); // Mark question as unanswered if the entry is empty
                            }
                            answers.Add(answer);
                            questionsAnswered.Add(true); // Mark this question as answered
                        }
                    }
                }
            }

            // If no answers were provided, mark the question as unanswered
            if (answers.Count == 0)
            {
                questionsAnswered.Add(false);
            }

            return answers;
        }

        // Event handler for when a checkbox is checked or unchecked
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var cb = sender as CheckBox;

            // If the checkbox is checked, show the entry field for the corresponding radio button selection
            if (cb.IsChecked)
            {
                var parent = cb.Parent as StackLayout;
                var rb = parent.Children[3] as RadioButton;

                if (rb.IsChecked)
                {
                    var entry = parent.Children[2] as Entry;
                    entry.IsVisible = true; // Show the entry field
                }
            }
            else
            {
                // If the checkbox is unchecked, hide the entry field
                var parent = cb.Parent as StackLayout;
                var rb = parent.Children[3] as RadioButton;

                if (rb.IsChecked)
                {
                    var entry = parent.Children[2] as Entry;
                    entry.IsVisible = false; // Hide the entry field
                }
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
    }

}