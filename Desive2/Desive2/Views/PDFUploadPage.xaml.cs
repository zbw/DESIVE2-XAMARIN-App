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
    public partial class PDFUploadPage : ContentPage
    {
        private bool isOpen = false;  // Variable to track whether the swipe view is open or not

        // Constructor to initialize the PDFUploadPage
        public PDFUploadPage()
        {
            InitializeComponent();  // Initializes the components of the page
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

            var task = await Navigator.ShellGoTo(lbl.Text);  // Navigates to the page specified in the label's text
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

        // Method to handle opening the settings page
        private async void OpenSettings(object sender, EventArgs e)
        {
            Navigator.PreviousPage.Push(Previous.PDF);  // Pushes the previous PDF page to the navigation stack
            await Navigator.ShellGoTo("Einstellungen");  // Navigates to the "Einstellungen" page (Settings)
        }

        // Method to handle opening the "Addition" page
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigator.PreviousPage.Push(Previous.PDF);  // Pushes the previous PDF page to the navigation stack
            await Navigator.ShellGoTo("Addition");  // Navigates to the "Addition" page
        }
    }

}