using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    // ViewModel for the Login Page, responsible for binding properties, handling commands, and managing login actions.
    public class LoginPageViewModel : BindableObject
    {
        // Constructor initializing commands and properties.
        public LoginPageViewModel()
        {
            IsChecked = false;
            LoginCommand = new Command(LoginAction);
            ClickCommand = new Command(Click);
            IsEnabled = false;
        }

        #region Properties

        // ICommand for handling login action.
        public ICommand LoginCommand { get; set; }

        // ICommand for handling click action.
        public ICommand ClickCommand { get; set; }

        // Property for login button color.
        private Color loginColor = Color.White;
        public Color LoginColor
        {
            get { return loginColor; }
            set { loginColor = value; OnPropertyChanged(); }
        }

        // Property for login button text color.
        private Color loginTextColor = Color.Black;
        public Color LoginTextColor
        {
            get { return loginTextColor; }
            set { loginTextColor = value; OnPropertyChanged(); }
        }

        // Property to enable/disable login button.
        private bool isEnabled = false;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        // Property to control visibility of UI elements.
        private bool areElementsVisible = true;
        public bool AreElementsVisible
        {
            get { return areElementsVisible; }
            set { areElementsVisible = value; OnPropertyChanged(); }
        }

        // Property to indicate if the app is busy (e.g., during login).
        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        // Property for email input field.
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
                CanLoginAction(); // Check if login button should be enabled.
            }
        }

        // Property for password input field.
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                CanLoginAction(); // Check if login button should be enabled.
            }
        }

        // Property to track if the user has agreed to terms (checkbox).
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged();
                CanLoginAction(); // Check if login button should be enabled.
            }
        }

        // Property to control whether the cancel button is shown.
        private bool _isShowCancel;
        public bool IsShowCancel
        {
            get { return _isShowCancel; }
            set
            {
                _isShowCancel = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        // Command for contacting support or customer service.
        private ICommand _contactCommand;
        public ICommand ContactCommand
        {
            get { return _contactCommand = _contactCommand ?? new Command(ContactAction); }
        }

        // Command for canceling login action.
        private ICommand _cancelLoginCommand;
        public ICommand CancelLoginCommand
        {
            get { return _cancelLoginCommand = _cancelLoginCommand ?? new Command(CancelLoginAction); }
        }

        // Command for initiating forgotten password process.
        private ICommand _forgotPasswordCommand;
        public ICommand ForgotPasswordCommand
        {
            get { return _forgotPasswordCommand = _forgotPasswordCommand ?? new Command(ForgotPasswordAction); }
        }

        // Command for creating a new account.
        private ICommand _newAccountCommand;
        public ICommand NewAccountCommand
        {
            get { return _newAccountCommand = _newAccountCommand ?? new Command(NewAccountAction); }
        }

        #endregion

        #region Methods

        // Method to enable/disable login button based on input validation.
        void CanLoginAction()
        {
            if ((string.IsNullOrWhiteSpace(this.Email) || string.IsNullOrWhiteSpace(this.Password)))
            {
                LoginColor = Color.White;
                LoginTextColor = Color.Black;
                IsEnabled = false;
            }
            else if (!IsChecked)
            {
                LoginColor = Color.White;
                LoginTextColor = Color.Black;
                IsEnabled = false;
            }
            else
            {
                LoginColor = Color.FromHex("#397baf");
                LoginTextColor = Color.White;
                IsEnabled = true;
            }
        }

        // Method to handle login action.
        async void LoginAction()
        {
            IsBusy = true;
            AreElementsVisible = false;
            try
            {
                Response login = await Database.Login(this.Email.ToLower(), this.Password);

                if (login != null && login.Status == 200)
                {
                    Preferences.Set("email", this.Email.ToLower());
                    Preferences.Set("idUser", login.Body.IdAppUser.ToString());
                    Preferences.Set("loginToken", login.Body.Token);
                    SharedPush.Initialize();
                    if (Preferences.Get("receivesPush", true))
                    {
                        SharedPush.Subscribe();
                    }
                    if (!login.Body.HasSetPassword)
                        await Shell.Current.GoToAsync("//CreatePasswordPage");
                    else
                    {
                        Preferences.Set("validLogin", "1");
                        await Shell.Current.GoToAsync("//MainPage");
                    }

                    this.Password = "";
                    isEnabled = false;
                    IsBusy = false;
                    AreElementsVisible = true;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Anmeldung fehlgeschlagen!", "Bitte überprüfen Sie Ihre Anmeldedaten oder Ihre Internetverbindung.", "Okay");
                    this.Password = "";
                    IsEnabled = false;
                    isEnabled = false;
                    AreElementsVisible = true;
                    IsBusy = false;
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Anmeldung fehlgeschlagen!", "Bitte überprüfen Sie Ihre Anmeldedaten oder Ihre Internetverbindung.", "Okay");
            }
        }

        // Method to handle cancel login action.
        void CancelLoginAction()
        {
            IsBusy = false;
            IsShowCancel = false;
        }

        // Method to handle forgotten password action.
        async void ForgotPasswordAction()
        {
            await Browser.OpenAsync(LinkHandler.ResetPassword);
        }

        // Method to handle new account creation action.
        async void NewAccountAction()
        {
            await Browser.OpenAsync(LinkHandler.Register);
        }

        // Method to handle contacting support action.
        async void ContactAction()
        {
            await Browser.OpenAsync(LinkHandler.Contact);
        }

        // Method to handle Terms of Service click action.
        async void Click()
        {
            await Browser.OpenAsync(LinkHandler.ToS);
        }

        #endregion
    }

}
