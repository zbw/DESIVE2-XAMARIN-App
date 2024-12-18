using Desive2.Models;
using Desive2.Services;
using Desive2.ViewModels;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.Views
{
    public partial class LoginPage : ContentPage
    {
        // Constructor for LoginPage
        public LoginPage()
        {
            BindingContext = new LoginPageViewModel();  // Set the binding context to the LoginPageViewModel
            InitializeComponent();  // Initialize the page components

            // Check the version of the app and if the user is already logged in
            CheckVersion();
            var pref = Preferences.Get("validLogin", null);
            if (pref == "1")  // If the user is valid, go to the main page
                GoToMain();
        }

        // Navigate to the main page
        public async void GoToMain()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        // Check the app version and handle updates
        public async void CheckVersion()
        {
            var number = DependencyService.Get<IAppVersionAndBuild>().GetVersionNumber();  // Get the app version
            var Build = DependencyService.Get<IAppVersionAndBuild>().GetBuildNumber();  // Get the app build number

            if (Device.RuntimePlatform == Device.Android)  // For Android devices
            {
                string version = await Database.GetAndroidVersion();  // Get the Android version from the database
                if ((version != "-1") && Convert.ToInt32(Build) < Convert.ToInt32(version))  // If there's a new version available
                {
                    if (await App.Current.MainPage.DisplayAlert("Eine neue Version der App ist verfügbar!", "Bitte laden Sie sich die neueste Version aus dem App-Store herunter.", " App-Store öffnen", "Abbrechen"))
                    {
                        // If the user agrees, open the app's Google Play store page
                        await Browser.OpenAsync("https://play.google.com/store/apps/details?id=de.zbw.desive2&pli=1");
                    }
                    Preferences.Set("idUser", null);  // Clear the user ID
                    await Shell.Current.GoToAsync("//LoginPage");  // Navigate back to the login page
                }
                else
                {
                    var pref = Preferences.Get("setPassword", null);
                    if (pref == "1")
                    {
                        GoToMain();  // Go to the main page if the user has set a password
                    }
                }
            }
            else if (Device.RuntimePlatform == Device.iOS)  // For iOS devices
            {
                string version = await Database.GetAppleVersion();  // Get the iOS version from the database
                if ((version != "-1") && Convert.ToInt32(Build) < Convert.ToInt32(version))  // If there's a new version available
                {
                    if (await App.Current.MainPage.DisplayAlert("Eine neue Version der App ist verfügbar!", "Bitte laden Sie sich die neueste Version aus dem App-Store herunter.", " App-Store öffnen", "Abbrechen"))
                    {
                        // If the user agrees, open the app's iOS App Store page
                        await Browser.OpenAsync("https://apps.apple.com/de/app/desive/id6444365688");
                    }
                    Preferences.Set("idUser", null);  // Clear the user ID
                    await Shell.Current.GoToAsync("//LoginPage");  // Navigate back to the login page
                }
                else
                {
                    var pref = Preferences.Get("setPassword", null);
                    if (pref == "1")
                    {
                        GoToMain();  // Go to the main page if the user has set a password
                    }
                }
            }
        }

        // Override the OnAppearing method to refresh the binding context when the page appears
        protected override void OnAppearing()
        {
            this.BindingContext = new LoginPageViewModel();  // Reset the binding context
            ((LoginPageViewModel)BindingContext).IsEnabled = false;  // Disable interaction temporarily
        }

        // Override the OnBackButtonPressed method to prevent going back
        protected override bool OnBackButtonPressed()
        {
            return true;  // Prevent going back
        }

        // Handle the eye icon click event to toggle password visibility
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var imageButton = sender as ImageButton;
            if (MyEntry.IsPassword)
            {
                imageButton.Source = ImageSource.FromFile("eyeon.png");  // Show the password
                MyEntry.IsPassword = false;  // Set IsPassword to false
            }
            else
            {
                imageButton.Source = ImageSource.FromFile("eyeoff.png");  // Hide the password
                MyEntry.IsPassword = true;  // Set IsPassword to true
            }
        }
    }

}
