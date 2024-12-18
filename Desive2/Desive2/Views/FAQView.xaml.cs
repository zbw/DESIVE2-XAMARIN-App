using Desive2.Objects;
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
    // Define the FAQView class, inheriting from ContentPage
    public partial class FAQView : ContentPage
    {
        // Private boolean variable to track whether the swipe view is open
        bool isOpen = false;

        // Constructor for FAQView
        public FAQView()
        {
            MyMenu = GetMenus(); // Set the menu list
            InitializeComponent(); // Initialize the page components
        }

        // Property to hold the list of menu items for the swipe view
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Method to generate the list of menu items
        private List<SwipeViewMenu> GetMenus()
        {
            return new List<SwipeViewMenu>
        {
            new SwipeViewMenu{ Name = "Startseite", Icon = "home.png"},
            new SwipeViewMenu{Name = "Eintrag", Icon="diary.png"},
            new SwipeViewMenu{ Name = "Sprachnotiz", Icon = "mic.png"},
            new SwipeViewMenu{ Name = "Bild hochladen", Icon = "upload.png"},
            new SwipeViewMenu{ Name = "Umfragen", Icon = "tasks.png"},
            new SwipeViewMenu{ Name = "Über uns", Icon = "aboutUs.png"},
            new SwipeViewMenu{ Name = "Einstellungen", Icon = "settings.png"},
        };
        }

        // Method to handle swipe open/close functionality
        private void OpenSwipe(object sender, EventArgs e)
        {
            if (!isOpen) // If swipe view is closed, open it
            {
                MainSwipeView.Open(OpenSwipeItem.LeftItems);
                isOpen = true;
            }
            else // If swipe view is open, close it
            {
                MainSwipeView.Close();
                isOpen = false;
            }
        }

        // Async method to handle closing of the swipe view and navigation to the selected page
        private async void CloseSwipe(object sender, EventArgs e)
        {
            var menu = (StackLayout)sender;
            MainSwipeView.Close(); // Close the swipe view
            var list = menu.Children.ToList(); // Get the children of the menu

            var lbl = (Label)list[1]; // Get the label from the menu
            Navigator.PreviousPage.Push(Previous.FAQ); // Push current page to the navigation stack
            var task = await Navigator.ShellGoTo(lbl.Text); // Navigate to the page specified by the label's text
        }

        // Method to close the swipe view when triggered by a grid interaction
        private void CloseSwipeGrid(object sender, EventArgs e)
        {
            MainSwipeView.Close(); // Close the swipe view
            isOpen = false; // Update the isOpen flag to false
        }

        // Async method to open the settings page when triggered
        private async void OpenSettings(object sender, EventArgs e)
        {
            Navigator.PreviousPage.Push(Previous.FAQ); // Push current page to navigation stack
            await Navigator.ShellGoTo("Einstellungen"); // Navigate to the "Einstellungen" (Settings) page
        }

        // Placeholder method for tap gesture (currently does nothing)
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // Empty method for tap gesture, currently does nothing
        }

        // Override method to handle back button press
        protected override bool OnBackButtonPressed()
        {
            shell(); // Call the shell method to go back in the navigation
            return true; // Indicate that the back button press is handled
        }

        // Async method to navigate back to the previous page in the navigation stack
        private async void shell()
        {
            await Navigator.ShellGoToPrevious(); // Navigate back to the previous page
        }
    }

}