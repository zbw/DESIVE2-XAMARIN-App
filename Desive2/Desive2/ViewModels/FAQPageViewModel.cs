using Desive2.Objects;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    // Class for the FAQ page's ViewModel, which binds data to the view.
    public class FAQPageViewModel : BindableObject
    {
        // Property for binding the profile picture. It retrieves and updates the picture from the CurrentProfilePic object.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); } // Notifies when the profile picture changes.
        }

        // List to hold the swipe view menu items.
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Constructor that initializes the menu by fetching it through SwipeViewMenu.GetMenus() method.
        public FAQPageViewModel()
        {
            MyMenu = SwipeViewMenu.GetMenus(); // Initializes MyMenu with the result from SwipeViewMenu.GetMenus().
        }
    }

}
