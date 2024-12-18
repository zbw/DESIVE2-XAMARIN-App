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
    // Define the CreatePasswordPage class, inheriting from ContentPage
    public partial class CreatePasswordPage : ContentPage
    {
        // Constructor for the CreatePasswordPage, initializes components and sets the BindingContext
        public CreatePasswordPage()
        {
            InitializeComponent();
            // Set the BindingContext to a new instance of CreatePasswordViewModel
            BindingContext = new CreatePasswordViewModel();
        }

        // Override method to handle when the page appears
        protected override void OnAppearing()
        {
            // Reset the BindingContext to a new instance of CreatePasswordViewModel when the page appears
            this.BindingContext = new CreatePasswordViewModel();
        }
    }

}