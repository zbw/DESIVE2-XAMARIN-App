using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using Desive2.ViewModels.Diary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Desive2.Views.DiaryQuestions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Define the Editor class as a ContentPage
    public partial class Editor : ContentPage
    {
        // Constructor for the Editor page, initializes the component and sets the BindingContext to a new EditorViewModel
        public Editor()
        {
            InitializeComponent();
            BindingContext = new EditorViewModel();
        }

        // Override OnAppearing method to refresh the BindingContext with a new instance of EditorViewModel when the page appears
        protected override void OnAppearing()
        {
            this.BindingContext = new EditorViewModel();
        }

        // Event handler for TapGestureRecognizer, calls OnBackButtonPressed when the user taps
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

        // Event handler for the send button click, adds the editor text to SurveyContent and navigates to VoiceMailForDiary if appropriate
        private async void SendClicked(object sender, EventArgs e)
        {
            // If the editor does not already contain the question, add it with the editor text
            if (!SurveyContent.Editor.ContainsKey(question1.Text) && editorText.Text != null && editorText.Text != "")
                SurveyContent.Editor.Add(question1.Text, editorText.Text);

            // Clear the editor text field after it is added
            editorText.Text = null;

            // If a valid diary entry exists, navigate to VoiceMailForDiary
            if (FilePathHandler.IdDiaryEntry != -1)
                await Application.Current.MainPage.Navigation.PushAsync(new VoiceMailForDiary());
        }

        // Event handler for the upload button click, disables the button, uploads the survey content, and displays a confirmation message
        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Disable the upload button while the operation is in progress
            btnUpload.IsEnabled = false;

            // If the editor does not already contain the question, add it with the editor text
            if (!SurveyContent.Editor.ContainsKey(question1.Text) && editorText.Text != null && editorText.Text != "")
                SurveyContent.Editor.Add(question1.Text, editorText.Text);

            // Clear the editor text field after it is added
            editorText.Text = null;

            // If a valid diary entry exists, modify the diary entry in the database
            if (FilePathHandler.IdDiaryEntry != -1)
            {
                // Convert the survey content to JSON and upload it to the database
                string json = SurveyContent.GetDiaryJson();
                Response res = await Database.ModifyDiaryEntry(FilePathHandler.IdDiaryEntry, 0, Preferences.Get("loginToken", null), json);

                // If the upload is successful, show a confirmation message and reset the survey content
                if (res.Status == 200)
                {
                    await Application.Current.MainPage.DisplayAlert("Vielen Dank!", "Ihre Antworten wurden erfolgreich hochgeladen.", "Okay");
                    SurveyContent.Reset();

                    // Reset the previous page stack
                    Navigator.PreviousPage = new Stack<Previous>();
                }
            }

            // Re-enable the upload button
            btnUpload.IsEnabled = true;
        }
    }

}

