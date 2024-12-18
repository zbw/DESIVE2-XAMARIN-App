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
    public class SurveyPageViewModel : BindableObject
    {
        // Property to get and set the profile picture of the user.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; } // Retrieves the current profile picture.
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); } // Sets a new profile picture and notifies property change.
        }

        // Command to return to the previous page or a survey list.
        public ICommand ReturnCommand { get; set; }

        // Property to store the title of the page.
        public string Title { get; set; }

        // List of surveys available on the page.
        public List<Survey> Survey { get; set; }

        // List of swipe menu items for the user interface.
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Constructor to initialize the ViewModel.
        public SurveyPageViewModel()
        {
            Title = "Umfrage beantworten"; // Sets the page title to "Answer Survey".
            ReturnCommand = new Command(Return); // Initializes the return command.
            MyMenu = SwipeViewMenu.GetMenus(); // Initializes the swipe menu.
            string id = Preferences.Get("idUser", null); // Retrieves the user ID from preferences.
                                                         // Survey = Database.GetCurrentSurvey(id); // Fetches the current survey based on the user ID (currently commented out).
        }

        // Method to navigate back to the survey list page.
        async void Return()
        {
            await Navigator.ShellGoTo("Umfragen"); // Navigates to the survey list page.
        }
    }

}