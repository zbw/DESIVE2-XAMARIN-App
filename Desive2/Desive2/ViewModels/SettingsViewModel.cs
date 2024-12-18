using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Media.Session.MediaSession;

namespace Desive2.ViewModels
{
    internal class SettingsViewModel : BindableObject
    {
        // Property to get and set the profile picture of the user.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; } // Returns the current profile picture.
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); } // Sets a new profile picture and notifies the change.
        }

        // List of menu items for the swipe view.
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Private backing field for the toggle state (whether push notifications are enabled or not).
        private bool _isToggled = Preferences.Get("receivesPush", true);

        // Property to get and set the toggle state for push notifications.
        public bool IsToggled
        {
            get
            {
                return _isToggled; // Returns the current state of the toggle.
            }
            set
            {
                if (value) // If the toggle is set to true (enabled).
                {
                    SharedPush.Subscribe(); // Subscribe to push notifications.
                    _isToggled = value; // Set the toggle state.
                    OnPropertyChanged(); // Notify that the toggle state has changed.
                }
                else // If the toggle is set to false (disabled).
                {
                    SharedPush.Unsubscribe(); // Unsubscribe from push notifications.
                    _isToggled = value; // Set the toggle state.
                    OnPropertyChanged(); // Notify that the toggle state has changed.
                }
            }
        }

        // Command to handle the action of selecting a new profile picture.
        public ICommand SelectProfilePicture { get; set; }

        // Constructor that initializes the command and the menu list.
        public SettingsViewModel()
        {
            SelectProfilePicture = new Command(SelectPictureCommand); // Initialize the SelectProfilePicture command.
            MyMenu = SwipeViewMenu.GetMenus(); // Get the menu items for the swipe view.
        }

        // Async method that navigates to the profile picture selection page.
        async void SelectPictureCommand()
        {
            Navigator.PreviousPage.Push(Previous.Settings); // Push the current page to the navigation stack.
            await Navigator.ShellGoTo("//ProfilePicture"); // Navigate to the profile picture selection page.
        }
    }

}
