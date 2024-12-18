using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using Desive2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desive2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalDataPage : ContentPage
    {
        private bool isOpen = false;  // Variable to track if the swipe view is open or not

        // Constructor to initialize the PersonalDataPage
        public PersonalDataPage()
        {
            InitializeComponent();  // Initializes the components of the page
        }

        // Override of the OnAppearing method to set the BindingContext when the page appears
        protected override void OnAppearing()
        {
            this.BindingContext = new PersonalDataPageViewModel();  // Sets the BindingContext to the ViewModel for this page
        }

        // Method to handle opening the settings page when a button is clicked
        private async void OpenSettings(object sender, EventArgs e)
        {
            Navigator.PreviousPage.Push(Previous.UserAccount);  // Pushes the previous user account page to the navigation stack
            await Navigator.ShellGoTo("Einstellungen");  // Navigates to the "Einstellungen" (Settings) page
        }

        // Method to handle swipe open or close functionality
        private void OpenSwipe(object sender, EventArgs e)
        {
            if (!isOpen)  // If the swipe view is not open
            {
                MainSwipeView.Open(OpenSwipeItem.LeftItems);  // Opens the swipe view to the left items
                isOpen = true;  // Sets the isOpen flag to true
            }
            else  // If the swipe view is already open
            {
                MainSwipeView.Close();  // Closes the swipe view
                isOpen = false;  // Sets the isOpen flag to false
            }
        }

        // Method to handle closing the swipe view when a menu item is selected
        private async void CloseSwipe(object sender, EventArgs e)
        {
            var menu = (StackLayout)sender;  // Gets the StackLayout of the menu
            MainSwipeView.Close();  // Closes the swipe view
            var list = menu.Children.ToList();  // Converts the children of the menu to a list

            var lbl = (Label)list[1];  // Gets the second child, which is expected to be a Label

            await Navigator.ShellGoTo(lbl.Text);  // Navigates to the page specified in the label's text
        }

        // Method to handle closing the swipe view when the grid is tapped
        private void CloseSwipeGrid(object sender, EventArgs e)
        {
            MainSwipeView.Close();  // Closes the swipe view
            isOpen = false;  // Sets the isOpen flag to false
        }

        // Override of the back button press behavior
        protected override bool OnBackButtonPressed()
        {
            shell();  // Calls the shell method to navigate back
            return true;  // Prevents the default back button behavior
        }

        // Method to navigate to the previous page in the navigation stack
        private async void shell()
        {
            await Navigator.ShellGoToPrevious();  // Navigates to the previous page
        }

        // Method to toggle password visibility when the image button is clicked
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var imageButton = sender as ImageButton;  // Gets the ImageButton that was clicked
            if (MyEntry.IsPassword)  // If the password is currently hidden
            {
                imageButton.Source = ImageSource.FromFile("eyeon.png");  // Change the image to show the password
                MyEntry.IsPassword = false;  // Set the entry to not hide the password
            }
            else  // If the password is currently visible
            {
                imageButton.Source = ImageSource.FromFile("eyeoff.png");  // Change the image to hide the password
                MyEntry.IsPassword = true;  // Set the entry to hide the password
            }
        }

        // Method to handle the tap gesture and open the FAQ link in the browser
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(LinkHandler.FAQ);  // Opens the FAQ link in the browser
        }

        // Method to handle checkbox state changes
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var cb = sender as CheckBox;  // Gets the checkbox that was changed
            if (cb.IsChecked)  // If the checkbox is checked
            {
                btnDelete.IsEnabled = true;  // Enables the delete button
                btnDelete.TextColor = Color.White;  // Sets the delete button text color to white
                btnDelete.BackgroundColor = Color.FromHex("#397baf");  // Sets the delete button background color
            }
            else  // If the checkbox is unchecked
            {
                btnDelete.IsEnabled = false;  // Disables the delete button
                btnDelete.TextColor = Color.Black;  // Sets the delete button text color to black
                btnDelete.BackgroundColor = Color.White;  // Sets the delete button background color to white
            }
        }

        // Method to handle the button click for deleting the account
        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Displays a confirmation alert to the user
            if (await App.Current.MainPage.DisplayAlert("Achtung!", "Sind Sie sicher, dass Sie Ihre Teilnahme an der Studie vorzeitig abbrechen und Ihre personenbezogenen Daten löschen möchten? Falls ja, tippen Sie bitte auf \"Fortfahren\".", "Fortfahren", "Abbrechen"))
            {
                // If the user confirms, deletes the account
                if (await Database.DeleteAccount(Preferences.Get("loginToken", null)))
                {
                    await App.Current.MainPage.DisplayAlert("Account löschen", "Wir haben Ihre Anfrage erhalten und werden die Löschung Ihres Accounts schnellstmöglich veranlassen. Anschließend erhalten Sie von uns eine Bestätigungsmail.", "Okay");
                    await Navigator.ShellGoTo("Startseite");  // Navigates to the home page
                    Navigator.PreviousPage = new Stack<Previous>();  // Clears the previous page stack
                }
            }
        }
    }

}