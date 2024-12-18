using Desive2.Services;
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
    // Define the AdditionalInfosPage class, inheriting from ContentPage
    public partial class AdditionalInfosPage : ContentPage
    {
        // Boolean variable to track the swipe view's open/close state
        private bool isOpen = false;

        // Constructor for the AdditionalInfosPage, initializes components
        public AdditionalInfosPage()
        {
            InitializeComponent();
        }

        // Event handler for opening or closing the swipe view
        private void OpenSwipe(object sender, EventArgs e)
        {
            // If the swipe view is not open, open it; otherwise, close it
            if (!isOpen)
            {
                MainSwipeView.Open(OpenSwipeItem.LeftItems);
                isOpen = true;
            }
            else
            {
                MainSwipeView.Close();
                isOpen = false;
            }
        }

        // Event handler for closing the swipe view and navigating to a selected menu item
        private async void CloseSwipe(object sender, EventArgs e)
        {
            var menu = (StackLayout)sender;
            MainSwipeView.Close(); // Close the swipe view
            var list = menu.Children.ToList(); // Get the children of the menu

            var lbl = (Label)list[1]; // Get the label of the selected item
                                      // Navigate to the selected menu page
            var task = await Navigator.ShellGoTo(lbl.Text);
        }

        // Event handler for closing the swipe view when tapping outside
        private void CloseSwipeGrid(object sender, EventArgs e)
        {
            MainSwipeView.Close(); // Close the swipe view
            isOpen = false; // Update the isOpen state to false
        }

        // Event handler for the tap gesture (no specific functionality here)
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            return;
        }

        // Override method to handle the back button press
        protected override bool OnBackButtonPressed()
        {
            shell(); // Navigate to the previous shell page
            return true; // Return true to indicate the event is handled
        }

        // Method to navigate to the previous shell page
        private async void shell()
        {
            await Navigator.ShellGoToPrevious(); // Go to the previous page in the shell
        }

        // Event handler for opening the settings page
        private async void OpenSettings(object sender, EventArgs e)
        {
            // Push "AdditionalInfos" to the previous page stack and navigate to the "Einstellungen" page
            Navigator.PreviousPage.Push(Previous.AdditionalInfos);
            await Navigator.ShellGoTo("Einstellungen");
        }
    }

}