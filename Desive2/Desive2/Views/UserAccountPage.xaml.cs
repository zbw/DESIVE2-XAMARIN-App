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
    public partial class UserAccountPage : ContentPage
    {
        // Flag to track if the swipe menu is open or closed
        bool isOpen = false;

        // Constructor to initialize the page components
        public UserAccountPage()
        {
            InitializeComponent();  // Initializes the page's components
                                    // BindingContext = new UserAccountViewModel();  // Uncomment this to set the BindingContext if using a ViewModel
        }

        // Event handler when the page is about to appear
        protected override void OnAppearing()
        {
            this.BindingContext = new UserAccountViewModel();  // Sets the BindingContext to the UserAccountViewModel when the page appears
        }

        // Event handler to navigate to the settings page
        private async void OpenSettings(object sender, EventArgs e)
        {
            Navigator.PreviousPage.Push(Previous.UserAccount);  // Saves the previous page for navigation stack
            await Navigator.ShellGoTo("Einstellungen");  // Navigates to the "Einstellungen" (Settings) page
        }

        // Event handler for swipe action (opens the swipe menu)
        private void OpenSwipe(object sender, EventArgs e)
        {
            if (!isOpen)
            {
                MainSwipeView.Open(OpenSwipeItem.LeftItems);  // Opens the left items of the swipe menu
                isOpen = true;  // Marks the menu as open
            }
            else
            {
                MainSwipeView.Close();  // Closes the swipe menu
                isOpen = false;  // Marks the menu as closed
            }
        }

        // Event handler for closing the swipe menu when an item is selected
        private async void CloseSwipe(object sender, EventArgs e)
        {
            var menu = (StackLayout)sender;  // Gets the sender of the event, assumed to be a StackLayout
            MainSwipeView.Close();  // Closes the swipe menu
            var list = menu.Children.ToList();  // Converts the children of the menu into a list

            var lbl = (Label)list[1];  // Gets the second child in the list, assumed to be a Label
            await Navigator.ShellGoTo(lbl.Text);  // Navigates to the page referenced by the label's text
        }

        // Event handler to close the swipe menu when the grid area is clicked
        private void CloseSwipeGrid(object sender, EventArgs e)
        {
            MainSwipeView.Close();  // Closes the swipe menu
            isOpen = false;  // Marks the menu as closed
        }

        // Event handler for the back button press event
        protected override bool OnBackButtonPressed()
        {
            shell();  // Calls the shell method to navigate back

            return true;  // Prevents the default back button behavior
        }

        // Navigates to the previous page
        private async void shell()
        {
            await Navigator.ShellGoToPrevious();  // Navigates to the previous page
        }

        // Event handler for tap gesture to navigate to the "ProfilePicture" page
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigator.PreviousPage.Push(Previous.Settings);  // Saves the previous page for navigation stack
            await Navigator.ShellGoTo("ProfilePicture");  // Navigates to the ProfilePicture page
        }

        // Event handler for tap gesture to navigate to the "LicensePage"
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigator.PreviousPage.Push(Previous.UserAccount);  // Saves the previous page for navigation stack
            await App.Current.MainPage.Navigation.PushAsync(new LicensePage());  // Navigates to the LicensePage
        }
    }

}