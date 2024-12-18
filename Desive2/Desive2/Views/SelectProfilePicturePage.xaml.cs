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
    public partial class SelectProfilePicturePage : ContentPage
    {
        // Constructor to initialize the SelectProfilePicturePage
        public SelectProfilePicturePage()
        {
            InitializeComponent();  // Initializes the components of the page
            this.BindingContext = new SelectPictureViewModel();  // Sets the BindingContext to the SelectPictureViewModel
        }

        // Method to handle the tap gesture and navigate to the UserAccountPage
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UserAccountPage());  // Pushes the UserAccountPage to the navigation stack
        }

        // Override of the back button press behavior
        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage.Navigation.PopAsync();  // Pops the current page from the navigation stack
            return true;  // Prevents the default back button behavior
        }

        // Override of the OnAppearing method to set the BindingContext when the page appears
        protected override void OnAppearing()
        {
            this.BindingContext = new SelectPictureViewModel();  // Sets the BindingContext to the SelectPictureViewModel when the page appears
        }
    }

}