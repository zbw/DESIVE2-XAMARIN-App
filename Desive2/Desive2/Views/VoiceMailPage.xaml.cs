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
    public partial class VoiceMailPage : ContentPage
    {
        // Flag to track the state of the swipe view
        bool isOpen = false;

        // Constructor to initialize the page components
        public VoiceMailPage()
        {
            InitializeComponent();  // Initializes the page's components
        }

        // Event handler for opening settings when triggered
        private async void OpenSettings(object sender, EventArgs e)
        {
            Navigator.PreviousPage.Push(Previous.VoiceMail);  // Pushes the current page to the previous page stack
            await Navigator.ShellGoTo("Einstellungen");  // Navigates to the "Einstellungen" page
        }

        // Event handler when the page is about to appear
        protected override void OnAppearing()
        {
            this.BindingContext = new VoiceMailViewModel();  // Sets the BindingContext to the VoiceMailViewModel when the page appears
        }

        // Event handler for opening/closing the swipe view when triggered
        private void OpenSwipe(object sender, EventArgs e)
        {
            if (!isOpen)
            {
                MainSwipeView.Open(OpenSwipeItem.LeftItems);  // Opens the left swipe items of the MainSwipeView
                isOpen = true;  // Updates the flag to indicate the swipe view is open
            }
            else
            {
                MainSwipeView.Close();  // Closes the MainSwipeView
                isOpen = false;  // Updates the flag to indicate the swipe view is closed
            }
        }

        // Event handler for closing the swipe menu and navigating to the selected page
        private async void CloseSwipe(object sender, EventArgs e)
        {
            var menu = (StackLayout)sender;  // Retrieves the StackLayout of the menu
            MainSwipeView.Close();  // Closes the MainSwipeView
            var list = menu.Children.ToList();  // Converts the children of the StackLayout to a list

            var lbl = (Label)list[1];  // Retrieves the second label (assuming it's for the page navigation)
            await Navigator.ShellGoTo(lbl.Text);  // Navigates to the page specified by the label's text
        }

        // Event handler for closing the swipe grid view
        private void CloseSwipeGrid(object sender, EventArgs e)
        {
            MainSwipeView.Close();  // Closes the MainSwipeView
            isOpen = false;  // Resets the flag to indicate the swipe view is closed
        }

        // Event handler for the back button press
        protected override bool OnBackButtonPressed()
        {
            shell();  // Calls the shell method to navigate to the previous page
            return true;  // Prevents the default back button behavior
        }

        // Navigates to the previous page in the Navigator's Shell
        private async void shell()
        {
            await Navigator.ShellGoToPrevious();  // Navigates to the previous page
        }

        // Event handler for the tap gesture to navigate to the AdditionalInfosPage
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdditionalInfosPage());  // Navigates to the AdditionalInfosPage
        }
    }

}
