using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using Desive2.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desive2.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        bool isOpen = false; // Tracks the state of the swipe view (whether it's open or closed)

        public MainPage()
        {
            InitializeComponent();  // Initializes the page components

            //SharedPush.Initialize();  // Initializes push notifications (commented out for now)

            //CheckForPush(Preferences.Get("loginToken", null));  // Checks for login token (commented out for now)

            if (Preferences.Get("firstLogin", true))  // Checks if the user is logging in for the first time
            {
                // Displays a welcome alert for first-time users
                App.Current.MainPage.DisplayAlert("Herzlich willkommen bei DESIVE²!",
                    "Um Sie an anstehende Umfragen und das Teilen von Inhalten zu erinnern, nutzen wir Pushbenachrichtigungen.\nSie können diese jederzeit in den Einstellungen deaktivieren.",
                    "Okay");

                Preferences.Set("firstLogin", false);  // Sets the 'firstLogin' preference to false after showing the welcome message
            }
        }

        protected override void OnAppearing()
        {
            BindingContext = new MainPageViewModel();  // Sets the binding context of the page to the MainPageViewModel when the page appears
        }

        private void OpenSwipe(object sender, EventArgs e)
        {
            if (!isOpen)  // If the swipe view is not open, open it
            {
                MainSwipeView.Open(OpenSwipeItem.LeftItems);  // Opens the left swipe items of the MainSwipeView
                isOpen = true;  // Set isOpen to true, indicating the swipe view is open
            }
            else  // If the swipe view is open, close it
            {
                MainSwipeView.Close();  // Closes the MainSwipeView
                isOpen = false;  // Set isOpen to false, indicating the swipe view is closed
            }
        }

        private async void CloseSwipe(object sender, EventArgs e)
        {
            var menu = (StackLayout)sender;  // Gets the sender object as a StackLayout
            MainSwipeView.Close();  // Closes the MainSwipeView
            var list = menu.Children.ToList();  // Converts the StackLayout children to a list

            var lbl = (Label)list[1];  // Gets the second child of the StackLayout (a Label)
            Navigator.PreviousPage.Push(Previous.Main);  // Pushes the previous page state to Navigator
            var task = await Navigator.ShellGoTo(lbl.Text);  // Navigates to the page corresponding to the label's text
        }

        private void CloseSwipeGrid(object sender, EventArgs e)
        {
            MainSwipeView.Close();  // Closes the MainSwipeView
            isOpen = false;  // Sets isOpen to false, indicating the swipe view is closed
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            return;  // This method doesn't perform any actions, it just returns
        }

        protected override bool OnBackButtonPressed()
        {
            shell();  // Calls the shell method when the back button is pressed

            return true;  // Prevents the default back button behavior
        }

        private async void shell()
        {
            await Navigator.ShellGoToPrevious();  // Navigates to the previous page in the navigation stack
        }
    }

}
