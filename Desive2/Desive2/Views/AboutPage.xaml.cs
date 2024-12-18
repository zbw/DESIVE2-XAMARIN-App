using Desive2.Objects;
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
    // Define the AboutPage class, inheriting from ContentPage
    public partial class AboutPage : ContentPage
    {
        // Boolean variable to track the swipe view's open/close state
        bool isOpen = false;

        // Constructor for the AboutPage, initializes components and sets the menu
        public AboutPage()
        {
            InitializeComponent();
            MyMenu = GetMenus(); // Assign the menu items to the MyMenu property
        }

        // Property that holds the list of menu items for the swipe view
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Method to generate and return a list of swipe view menu items
        private List<SwipeViewMenu> GetMenus()
        {
            // Returning a list of predefined menu items with names and icon file names
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

        // Event handler for opening the settings page
        private async void OpenSettings(object sender, EventArgs e)
        {
            // Navigate to the "Einstellungen" page and store "AboutUs" in the PreviousPage stack
            Navigator.PreviousPage.Push(Previous.AboutUs);
            await Navigator.ShellGoTo("Einstellungen");
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
            Navigator.PreviousPage.Push(Previous.AboutUs); // Push "AboutUs" to the previous page stack
            await Navigator.ShellGoTo(lbl.Text); // Navigate to the selected menu page
        }

        // Event handler for closing the swipe view when tapping outside
        private void CloseSwipeGrid(object sender, EventArgs e)
        {
            MainSwipeView.Close(); // Close the swipe view
            isOpen = false; // Update the isOpen state to false
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
    }

}