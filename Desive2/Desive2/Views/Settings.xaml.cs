using Android.Nfc.Tech;
using Desive2.Services;
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
    public partial class Settings : ContentPage
    {
        bool isOpen = false; // Flag to track the state of the swipe view (open or closed)

        // Constructor to initialize the Settings page
        public Settings()
        {
            InitializeComponent();  // Initializes the components of the page
        }

        // Override of OnAppearing to set the BindingContext when the page appears
        protected override void OnAppearing()
        {
            this.BindingContext = new SettingsViewModel();  // Sets the BindingContext to the SettingsViewModel
        }

        // Method to handle the tap gesture and navigate to the "Einstellungen" page
        private async void OpenSettings(object sender, EventArgs e)
        {
            Navigator.PreviousPage.Push(Previous.UserAccount);  // Pushes the UserAccount page to the navigation stack
            await Navigator.ShellGoTo("Einstellungen");  // Navigates to the "Einstellungen" page
        }

        // Method to toggle the swipe view between open and closed states
        private void OpenSwipe(object sender, EventArgs e)
        {
            if (!isOpen)
            {
                MainSwipeView.Open(OpenSwipeItem.LeftItems);  // Opens the swipe view
                isOpen = true;  // Sets the state to open
            }
            else
            {
                MainSwipeView.Close();  // Closes the swipe view
                isOpen = false;  // Sets the state to closed
            }
        }

        // Method to close the swipe view and navigate to the selected menu item
        private async void CloseSwipe(object sender, EventArgs e)
        {
            var menu = (StackLayout)sender;
            MainSwipeView.Close();  // Closes the swipe view
            var list = menu.Children.ToList();  // Converts the children of the menu to a list
            var lbl = (Label)list[1];  // Gets the label of the selected menu item
            await Navigator.ShellGoTo(lbl.Text);  // Navigates to the page corresponding to the menu item
        }

        // Method to close the swipe view when the swipe grid is clicked
        private void CloseSwipeGrid(object sender, EventArgs e)
        {
            MainSwipeView.Close();  // Closes the swipe view
            isOpen = false;  // Sets the state to closed
        }

        // Override of the back button press behavior
        protected override bool OnBackButtonPressed()
        {
            shell();  // Calls the shell method to navigate to the previous page
            return true;  // Prevents the default back button behavior
        }

        // Method to navigate to the previous page in the shell
        private async void shell()
        {
            await Navigator.ShellGoToPrevious();  // Navigates to the previous page in the navigation stack
        }

        // Method to handle the tap gesture (currently empty)
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//SelectProfilePicture");  // (commented out) Navigates to the SelectProfilePicture page
        }

        // Method to handle the tap gesture and navigate to the LicensePage
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigator.PreviousPage.Push(Previous.UserAccount);  // Pushes the UserAccount page to the navigation stack
            await Application.Current.MainPage.Navigation.PushAsync(new LicensePage());  // Navigates to the LicensePage
        }

        // Method to handle the switch toggle event and subscribe/unsubscribe from push notifications
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Switch sw = (Switch)sender;  // Gets the Switch control that was toggled
            if (sw.IsToggled)
                PushTokenHandler.Subscribe();  // Subscribes to push notifications if the switch is on
            else
                PushTokenHandler.Unsubscribe();  // Unsubscribes from push notifications if the switch is off
        }
    }

}