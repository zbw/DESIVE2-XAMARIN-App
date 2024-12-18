using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    internal class PersonalDataPageViewModel : BindableObject
    {
        // Property for getting and setting the user's profile picture.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); } // Notifies that the ProfilePicture property has changed.
        }

        // Property for the list of menu items for the swipe view.
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Private backing field for the Name property.
        private string name;

        // Property for getting and setting the user's name.
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); } // Notifies that the Name property has changed.
        }

        // Private backing field for the IBAN property.
        private string iban = "";

        // Property for getting and setting the user's IBAN.
        public string IBAN
        {
            get { return iban; }
            set { iban = value; OnPropertyChanged(); } // Notifies that the IBAN property has changed.
        }

        // Command to upload the user's data.
        public ICommand UploadDataCommand { get; set; }

        // Command to open the settings page.
        public ICommand OpenSettings { get; set; }

        // Command to delete the user's account.
        public ICommand DeleteAccountCommand { get; set; }

        // Private backing field for the DeleteButtonColor property.
        private Color deleteButtonColor = Color.White;

        // Property for setting and getting the delete button's color.
        public Color DeleteButtonColor
        {
            get { return deleteButtonColor; }
            set { deleteButtonColor = value; OnPropertyChanged(); } // Notifies that the DeleteButtonColor property has changed.
        }

        // Constructor that initializes the commands and fetches the initial values.
        public PersonalDataPageViewModel()
        {
            MyMenu = SwipeViewMenu.GetMenus(); // Fetch the menu items for the swipe view.
            OpenSettings = new Command(OpenSettingsCommand); // Command to open the settings page.
            UploadDataCommand = new Command(UploadData); // Command to upload the user's data.

            GetValuesAsync(); // Fetch the user's IBAN and name asynchronously.
        }

        // Asynchronous method to upload the user's IBAN and Name.
        async void UploadData()
        {
            if (await Database.UploadIban(Name, IBAN, Preferences.Get("loginToken", null))) // Upload the IBAN to the database.
            {
                await App.Current.MainPage.DisplayAlert("Erfolg", "Ihre persönlichen Daten wurden erfolgreich hochgeladen.", "Okay"); // Display success message.
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fehler", "Bei der Übermittlung Ihrer Eingabe ist ein Fehler aufgetreten! Bitte überprüfen Sie ihre Eingaben.", "Okay"); // Display error message.
            }
        }

        // Asynchronous method to open the settings page.
        async void OpenSettingsCommand()
        {
            Navigator.PreviousPage.Push(Previous.PersonalData); // Push the current page to the navigation stack.
            await Navigator.ShellGoTo("Einstellungen"); // Navigate to the settings page.
        }

        // Asynchronous method to fetch the IBAN and Name from the database.
        async void GetValuesAsync()
        {
            string res = await Database.GetIban(Preferences.Get("loginToken", null)); // Fetch the IBAN.
            if (res != "-1" && res != "-2")
            {
                if (res != null)
                    IBAN = res; // Set the IBAN property if a valid result is returned.
                else
                    IBAN = ""; // Set the IBAN property to an empty string if no result is found.
            }

            res = await Database.GetName(Preferences.Get("loginToken", null)); // Fetch the Name.
            if (res != "-1" && res != "-2")
            {
                if (res != null)
                    Name = res; // Set the Name property if a valid result is returned.
                else
                    Name = ""; // Set the Name property to an empty string if no result is found.
            }
        }
    }

}
