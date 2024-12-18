using Android.Icu.Text;
using Desive2.Objects;
using Desive2.ViewModels.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Desive2.Views.DiaryQuestions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Define the MultipleChoiceWithEditorPage class as a ContentPage
    public partial class MultipleChoiceWithEditorPage : ContentPage
    {
        // List to track whether all questions are answered
        List<bool> questionsAnswered = new List<bool>();
        // List to store the answers for each question
        List<Dictionary<string, string>> answers = new List<Dictionary<string, string>>();

        // Default constructor for the page, initializes components
        public MultipleChoiceWithEditorPage()
        {
            // Initialize components
            InitializeComponent();
        }

        // Constructor that accepts a MultipleChoiceWithEditor question, sets the BindingContext, and initializes components
        public MultipleChoiceWithEditorPage(MultpleChoiceWithEditor question)
        {
            // Set the BindingContext to a new SingleChoiceWithEditorViewModel
            this.BindingContext = new SingleChoiceWithEditorViewModel(question);
            InitializeComponent();
        }

        // Event handler for the button click, processes the answers and proceeds accordingly
        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool hasAnswer = false;

            // Get all StackLayouts from the rbGroup
            List<View> sl = rbGroup.Children.Where(x => x is StackLayout).ToList();

            // Iterate through each StackLayout
            foreach (var x in sl)
            {
                var children = (StackLayout)x;
                var s = children.Children.ToList();
                var d = s[0];
                var stackLayout = (StackLayout)d;
                var cb = (CheckBox)stackLayout.Children[0];

                // If the checkbox is checked, check if the associated editor is empty
                if (cb.IsChecked)
                {
                    var stack = (StackLayout)s[1];
                    var list = stack.Children.ToList();
                    var label = (Label)list[0];
                    var editor = (Xamarin.Forms.Editor)list[1];

                    // Display alert if the editor is empty
                    if (editor.Text == String.Empty || editor.Text == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Achtung", "Eine oder mehrere Pflichtfragen sind nicht beantwortet worden. Bitte beantworten Sie diese zuerst, um fortzufahren!", "Okay");
                        return;
                    }
                    else
                    {
                        // Add the answer to the answers list
                        Dictionary<string, string> answer = new Dictionary<string, string>();
                        answer.Add(label.Text, editor.Text);
                        answers.Add(answer);
                    }
                    hasAnswer = true;
                }
            }

            // If there are answers, add them to the SurveyContent and proceed to the next diary entry
            if (hasAnswer)
            {
                SurveyContent.MultipleChoiceWithEditor.Add(question.Text, answers);
                SurveyContent.DiaryCount++;
                SurveyContent.GoToNextDiary(SurveyContent.DiaryPath);
            }
            else
            {
                // If no answers were provided, show an alert
                await App.Current.MainPage.DisplayAlert("Achtung", "Eine oder mehrere Pflichtfragen sind nicht beantwortet worden. Bitte beantworten Sie diese zuerst, um fortzufahren!", "Okay");
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

        // Event handler for when a checkbox is checked or unchecked
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            var parent = cb.Parent;
            var sl = (StackLayout)parent;
            var stackLayout = sl.Parent;
            var s = ((StackLayout)stackLayout).Children.ToList();

            // If the checkbox is checked, make the associated editor visible
            if (cb.IsChecked)
            {
                var stackTwo = (StackLayout)s[1];
                stackTwo.IsVisible = true;
            }
            else
            {
                // If the checkbox is unchecked, hide the associated editor
                var stackTwo = (StackLayout)s[1];
                stackTwo.IsVisible = false;
            }
        }
    }

}