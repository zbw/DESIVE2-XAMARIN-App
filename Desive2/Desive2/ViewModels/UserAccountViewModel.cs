using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    public class UserAccountViewModel : BindableObject
    {
        // Property for getting and setting the user's profile picture.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); }
        }

        // List of menu items for swipe menu.
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Property for the user's name.
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        // Commands for navigating to various sections of the app.
        public ICommand OpenPersonalData { get; set; }
        public ICommand OpenSettings { get; set; }
        public ICommand OpenLicenses { get; set; }
        public ICommand LogoutCommand { get; set; }

        // Property for displaying the number of submitted entries.
        private string numInfos = "Anzahl eingereichter Infos: ";
        public string NumInfos
        {
            get { return numInfos; }
            set
            {
                numInfos = value;
                OnPropertyChanged();
            }
        }

        // Property for managing the push notification subscription state.
        private bool _isToggled = true;
        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                if (value)
                {
                    PushTokenHandler.Subscribe();
                    _isToggled = value;
                    OnPropertyChanged();
                }
                else
                {
                    PushTokenHandler.Unsubscribe();
                    _isToggled = value;
                    OnPropertyChanged();
                }
            }
        }

        // Property for enabling or disabling the send button.
        private bool isSendEnabled = false;
        public bool IsSendEnabled
        {
            get { return isSendEnabled; }
            set { isSendEnabled = value; OnPropertyChanged(); }
        }

        // Property to indicate whether the app is busy processing.
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        // Property to control visibility of the grid UI element.
        private bool _isGridVisible = true;
        public bool IsGridVisible
        {
            get { return _isGridVisible; }
            set { _isGridVisible = value; OnPropertyChanged(); }
        }

        // Constructor to initialize commands and fetch user data.
        public UserAccountViewModel()
        {
            try
            {
                SetValues();  // Fetch and set user data from the database.

                OpenSettings = new Command(OpenSettingsCommand);
                OpenPersonalData = new Command(OpenPersonalDataCommand);
                OpenLicenses = new Command(OpenLicensesCommand);
                MyMenu = SwipeViewMenu.GetMenus();
                LogoutCommand = new Command(Logout);  // Command for logging out.
            }
            catch
            {
                // Display error message if there is an exception during initialization.
                App.Current.MainPage.DisplayAlert("Fehler", "Bitte überprüfen Sie Ihre Internetverbindung.", "Okay");
            }
        }

        // Method to fetch and set the user's name and number of entries.
        async void SetValues()
        {
            string name = await Database.GetName(Preferences.Get("loginToken", null));
            if (name != "-1")
                Name += name; // Set the user's name.

            int res = await Database.GetNumOfEntries(Preferences.Get("loginToken", null));
            if (res >= 0)
            {
                NumInfos += res.ToString(); // Set the number of submitted entries.
            }
        }

        // Command method for navigating to the settings page.
        async void OpenSettingsCommand()
        {
            Navigator.PreviousPage.Push(Previous.UserAccount);
            await Navigator.ShellGoTo("Settings");
        }

        // Command method for navigating to the personal data page.
        async void OpenPersonalDataCommand()
        {
            Navigator.PreviousPage.Push(Previous.UserAccount);
            await Navigator.ShellGoTo("PersonalData");
        }

        // Co

    }
