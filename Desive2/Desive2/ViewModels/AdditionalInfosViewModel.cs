using Desive2.Objects;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    /// <summary>
    /// ViewModel for the Additional Infos page that manages the user's profile picture and opens the FAQ page via a command.
    /// </summary>
    public class AdditionalInfosViewModel : BindableObject
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
        /// List of menu items displayed in the swipe view.
        /// </summary>
        public List<SwipeViewMenu> MyMenu { get; set; }

        /// <summary>
        /// Command to open the FAQ page.
        /// </summary>
        public ICommand OpenFAQ { get; }

        /// <summary>
        /// Initializes a new instance of the AdditionalInfosViewModel and sets up the OpenFAQ command.
        /// </summary>
        public AdditionalInfosViewModel()
        {
            // Load the menu items for the swipe view
            MyMenu = SwipeViewMenu.GetMenus();

            // Command to open the FAQ page
            OpenFAQ = new Command(async () => await Browser.OpenAsync(LinkHandler.FAQ));
        }
    }

}
