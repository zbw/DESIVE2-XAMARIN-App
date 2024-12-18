using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    public class StartSurveyViewModel : BindableObject
    {
        // Property to get and set the profile picture of the user.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; } // Retrieves the current profile picture.
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); } // Sets a new profile picture and notifies property change.
        }

        // Command to open the survey page.
        public ICommand OpenSurveyCommand { get; set; }

        // List of swipe menu items for the user interface.
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Private backing field for the button's active state.
        private bool isButtonActive = false;

        // Property to get and set whether the button is active or not.
        public bool IsButtonActive
        {
            get => isButtonActive; // Returns the current active state of the button.
            set
            {
                isButtonActive = value; // Sets the new active state of the button.
                OnPropertyChanged(); // Notifies that the property has changed.
            }
        }

        // Private backing field for the heading text of the survey.
        private string headingText = "Umfrage";

        // Property to get and set the heading text for the survey.
        public string HeadingText
        {
            get => headingText; // Returns the current heading text.
            set
            {
                headingText = value; // Sets the new heading text.
                OnPropertyChanged(); // Notifies that the property has changed.
            }
        }

        // Private backing field for the display text (survey message).
        private string displayText = "Derzeit gibt es für Sie leider keine weiteren Umfragen. Schauen Sie zu einem späteren Zeitpunkt noch einmal vorbei! Wir werden Sie zudem benachrichtigen.";

        // Property to get and set the display text (message for the user).
        public string DisplayText
        {
            get => displayText; // Returns the current display text.
            set
            {
                displayText = value; // Sets the new display text.
                OnPropertyChanged(); // Notifies that the property has changed.
            }
        }

        // Constructor that initializes the ViewModel.
        public StartSurveyViewModel()
        {
            OpenSurveyCommand = new Command(OpenSurvey); // Initializes the OpenSurvey command.
            MyMenu = SwipeViewMenu.GetMenus(); // Initializes the swipe menu.

            SetText(); // Calls SetText method to initialize the text based on conditions.
        }

        // Method to set the appropriate text when the survey is open.
        async void SetText()
        {
            // Code commented out for survey status check.
            // if (await Database.IsSuveyOpen(Preferences.Get("idUser", null)))
            // {
            //     IsButtonActive = true;
            //     HeadingText = "Umfrage";
            //     DisplayText = "Eine neue Umfrage steht für Sie zur Verfügung! Lassen Sie sich für die Beantwortung der Fragen Zeit und beantworten Sie alle Fragen nach Ihrem Bauchgefühl. Dabei gibt es kein Richtig oder Falsch.";
            // }
        }

        // Method to open the survey page when the button is clicked.
        async void OpenSurvey()
        {
            IsButtonActive = false; // Deactivates the button after clicking.
            HeadingText = "Umfrage"; // Resets the heading text.
            DisplayText = "Derzeit gibt es für Sie leider keine weiteren Umfragen. Schauen Sie zu einem späteren Zeitpunkt noch einmal vorbei! Wir werden Sie zudem benachrichtigen."; // Sets the display text when no surveys are available.

            await Shell.Current.GoToAsync("//SurveyPage"); // Navigates to the SurveyPage.
        }
    }

}
