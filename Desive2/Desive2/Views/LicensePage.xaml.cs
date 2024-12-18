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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Define the LicensePage class, inheriting from ContentPage
    public partial class LicensePage : ContentPage
    {
        // Constructor for LicensePage
        public LicensePage()
        {
            InitializeComponent(); // Initialize the page components
        }

        // Async method to navigate back to the previous page when a tap gesture is recognized
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigator.ShellGoToPrevious(); // Navigate to the previous page
        }

        // Override method to handle back button press
        protected override bool OnBackButtonPressed()
        {
            Navigator.ShellGoToPrevious(); // Navigate to the previous page
            return true; // Indicate that the back button press is handled
        }
    }

}