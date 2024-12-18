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
    public partial class VoiceMailForDiary : ContentPage
    {
        // Constructor to initialize the page components
        public VoiceMailForDiary()
        {
            InitializeComponent();  // Initializes the page's components
        }

        // Event handler for the back button press event
        protected override bool OnBackButtonPressed()
        {
            return true;  // Prevents the default back button behavior
        }

        // Event handler when the page is about to appear
        protected override void OnAppearing()
        {
            this.BindingContext = new VoiceMailForDIaryViewModel();  // Sets the BindingContext to the VoiceMailForDIaryViewModel when the page appears
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