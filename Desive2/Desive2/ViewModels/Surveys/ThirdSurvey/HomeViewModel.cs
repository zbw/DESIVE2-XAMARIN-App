using Desive2.Objects;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Desive2.ViewModels.Surveys.ThirdSurvey
{
    /// <summary>
    /// ViewModel for the Home page that manages the list of menu items and the user's profile picture.
    /// </summary>
    public class HomeViewModel : BindableObject
    {
        /// <summary>
        /// Gets or sets the list of menu items displayed in the swipe view.
        /// </summary>
        public List<SwipeViewMenu> MyMenu { get; set; }

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
        /// Initializes a new instance of the HomeViewModel and loads the menu items.
        /// </summary>
        public HomeViewModel()
        {
            // Load the menu items for the swipe view
            MyMenu = SwipeViewMenu.GetMenus();
        }
    }

}
