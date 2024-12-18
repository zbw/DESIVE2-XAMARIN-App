using Desive2.Objects;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    /// <summary>
    /// ViewModel for the About page that manages the user's profile picture and navigates to different pages via commands.
    /// </summary>
    public class AboutViewModel : BindableObject
    {
        /// <summary>
        /// Gets or sets the profile picture of the current user.
        /// It triggers the OnPropertyChanged method when the profile picture is updated.
        /// </summary>
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set
            {
                CurrentProfilePic.Picture = value;
                OnPropertyChanged();  // Notify UI about the change
            }
        }

        /// <summary>
        /// Initializes a new instance of the AboutViewModel and sets up commands for navigating to external links and licenses.
        /// </summary>
        public AboutViewModel()
        {
            // Command to open contact page
            OpenContact = new Command(async () => await Browser.OpenAsync(LinkHandler.Contact));

            // Command to open FAQ page
            OpenFAQ = new Command(async () => await Browser.OpenAsync(LinkHandler.FAQ));

            // Command to open licenses page
            OpenLicenses = new Command(OpenLicensesCommand);

            // Load the menu items for the swipe view
            MyMenu = SwipeViewMenu.GetMenus();
        }

        /// <summary>
        /// List of menu items displayed in the swipe view.
        /// </summary>
        public List<SwipeViewMenu> MyMenu { get; set; }

        /// <summary>
        /// Command to open the licenses page.
        /// </summary>
        public ICommand OpenLicenses { get; set; }

        /// <summary>
        /// Command to open the contact page.
        /// </summary>
        public ICommand OpenContact { get; }

        /// <summary>
        /// Command to open the FAQ page.
        /// </summary>
        public ICommand OpenFAQ { get; }

        /// <summary>
        /// Opens the licenses page and pushes the current page to the navigation stack.
        /// </summary>
        private async void OpenLicensesCommand()
        {
            // Push the current page (AboutUs) to the navigation stack
            Navigator.PreviousPage.Push(Previous.AboutUs);

            // Navigate to the License page
            await Navigator.ShellGoTo("License");
        }
    }

}
