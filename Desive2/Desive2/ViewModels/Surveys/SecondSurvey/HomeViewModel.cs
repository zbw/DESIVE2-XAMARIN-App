using Desive2.Objects;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Desive2.ViewModels.Surveys.SecondSurvey
{
    /// <summary>
    /// ViewModel class for the Home page, managing menu items and profile picture.
    /// </summary>
    public class HomeViewModel : BindableObject
    {
        /// <summary>
        /// Gets or sets the list of menu items for the swipe view.
        /// </summary>
        public List<SwipeViewMenu> MyMenu { get; set; }

        private string _profilePicture;

        /// <summary>
        /// Gets or sets the profile picture, updating the current profile picture and notifying changes.
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
        /// Initializes the HomeViewModel and loads the menu items.
        /// </summary>
        public HomeViewModel()
        {
            // Fetch the swipe view menus
            MyMenu = SwipeViewMenu.GetMenus();
        }
    }

}
