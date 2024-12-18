using Desive2.Models;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Desive2.ViewModels
{
    /// <summary>
    /// ViewModel for creating a password, which includes validation for password confirmation and enabling the submit button based on input.
    /// </summary>
    public class CreatePasswordViewModel : BindableObject
    {
        // Private fields for the passwords entered by the user.
        private string password1 = "";
        private string password2 = "";

        /// <summary>
        /// Gets or sets the first password entered by the user.
        /// Triggers CanLoginAction method to validate the input and enable the submit button.
        /// </summary>
        public string Password1
        {
            get
            {
                return password1;
            }
            set
            {
                password1 = value;
                OnPropertyChanged();  // Notify UI about the change
                CanLoginAction();  // Check if button should be enabled
            }
        }

        /// <summary>
        /// Gets or sets the second password entered by the user.
        /// Triggers CanLoginAction method to validate the input and enable the submit button.
        /// </summary>
        public string Password2
        {
            get
            {
                return password2;
            }
            set
            {
                password2 = value;
                OnPropertyChanged();  // Notify UI about the change
                CanLoginAction();  // Check if button should be enabled
            }
        }

        // Private fields for button state and color properties.
        private bool _isButtonActive = false;
        private Color _buttonColor = Color.White;
        private Color _textColor = Color.Black;

        /// <summary>
        /// Gets or sets a value indicating whether the submit button is active.
        /// </summary>
        public bool IsButtonActive
        {
            get { return _isButtonActive; }
            set
            {
                _isButtonActive = value;
                OnPropertyChanged();  // Notify UI about the change
            }
        }

        /// <summary>
        /// Gets or sets the background color of the button.
        /// </summary>
        public Color ButtonColor
        {
            get { return _buttonColor; }
            set
            {
                _buttonColor = value;
                OnPropertyChanged();  // Notify UI about the change
            }
        }

        /// <summary>
        /// Gets or sets the text color of the button.
        /// </summary>
        public Color TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                OnPropertyChanged();  // Notify UI about the change
            }
        }

        /// <summary>
        /// Command for creating a password.
        /// </summary>
        public ICommand CreatePasswordCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the CreatePasswordViewModel and sets up the command.
        /// </summary>
        public CreatePasswordViewModel()
        {
            CreatePasswordCommand = new Command(CreatePassword);  // Command to handle password creation
        }

        /// <summary>
        /// Validates the password inputs and enables or disables the submit button based on the validation.
        /// </summary>
        void CanLoginAction()
        {
            if (string.IsNullOrWhiteSpace(this.Password1) || string.IsNullOrWhiteSpace(this.Password2) || this.Password1 == "" || this.Password2 == "")
            {
                ButtonColor = Color.White;  // Disable button
                TextColor = Color.Black;    // Set text color to black
                IsButtonActive = false;     // Button is not active
            }
            else
            {
                ButtonColor = Color.FromHex("#397baf");  // Enable button with custom color
                TextColor = Color.White;                 // Set text color to white
                IsButtonActive = true;                   // Button is active
            }
        }

        /// <summary>
        /// Creates a new password if the two passwords match and updates the database.
        /// </summary>
        private async void CreatePassword()
        {
            if (!Password1.Equals(Password2))  // Check if passwords match
                await App.Current.MainPage.DisplayAlert("Achtung", "Ihre Passwörter stimmen nicht miteinander überein.", "Okay");
            else
            {
                // If passwords match, set the new password in the database
                if (await Database.SetPassword(Password1, Preferences.Get("loginToken", null)) > 0)
                {
                    Preferences.Set("validLogin", "1");  // Set login status as valid
                    await Shell.Current.GoToAsync("//MainPage");  // Navigate to the main page
                }
            }
        }
    }

}
