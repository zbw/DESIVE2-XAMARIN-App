using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    // ViewModel for the Main Page, responsible for binding properties, handling commands, and managing navigation actions.
    class MainPageViewModel : BindableObject
    {
        // Property for user's profile picture.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); }
        }

        // Property for the menu items.
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Property for user's name, initialized with "Hallo ".
        private string name = "Hallo ";
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        // Property for task description.
        private string task = "";
        public string Task
        {
            get { return task; }
            set { task = value; OnPropertyChanged(); }
        }

        // Property to control visibility of elements on the page.
        private bool areElementsVisible = true;
        public bool AreElementsVisible
        {
            get { return areElementsVisible; }
            set { areElementsVisible = value; OnPropertyChanged(); }
        }

        // Property to indicate if the page is busy (e.g., during a navigation or data fetch).
        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        // Property to track if a survey is open.
        private bool isSurveyOpen = false;
        public bool IsSurveyOpen
        {
            get { return isSurveyOpen; }
            set { isSurveyOpen = value; OnPropertyChanged(); }
        }

        // ICommand for opening settings page.
        public ICommand OpenSettings { get; set; }

        // ICommand for opening diary page.
        public ICommand OpenDiary { get; set; }

        // ICommand for opening FAQ page.
        public ICommand OpenFAQ { get; set; }

        // ICommand for opening survey page.
        public ICommand OpenSurvey { get; set; }

        // Constructor, initializing commands and setting up task.
        public MainPageViewModel()
        {
            MyMenu = SwipeViewMenu.GetMenus();
            OpenSettings = new Command(OpenSettingsCommand);
            OpenDiary = new Command(OpenDiaryCommand);
            OpenFAQ = new Command(OpenFAQCommand);
            OpenSurvey = new Command(OpenSurveyCommand);
            SetTask();
        }

        // Method to fetch user's name and survey state, and update UI accordingly.
        async void SetTask()
        {
            string res = await Database.GetName(Preferences.Get("loginToken", "null"));
            SurveyState state = await Database.IsSuveyOpen(Preferences.Get("loginToken", null));

            if (res != "-1")
                Name += res;

            if (state != null)
            {
                if (state.EndOfStudy)
                    await App.Current.MainPage.DisplayAlert("Ende der Studie", "Bitte vergessen Sie nicht, Ihre IBAN anzugeben. Danach können Sie die App von Ihrem Gerät löschen. Vielen Dank für Ihre Teilnahme!", "Okay");

                if (state.Survey3 && state.Survey4)
                {
                    Preferences.Set("Survey4Open", true);
                    SurveyContent.SurveyType = SurveyType.SurveyOne;
                    SurveyContent.SurveySection = SurveySection.SectionOne;
                    Task = "Umfragenkatalog 1 erhältlich";
                    IsSurveyOpen = true;
                }

                if (state.Survey1)
                {
                    Preferences.Set("Survey", "0");
                    Task = "Umfragenkatalog 1 erhältlich";
                    SurveyContent.SurveyType = SurveyType.SurveyOne;
                    SurveyContent.SurveySection = SurveySection.SectionOne;
                    IsSurveyOpen = true;
                }
                else if (state.Survey2)
                {
                    Task = "Umfragenkatalog 2 erhältlich";
                    Preferences.Set("Survey", "1");
                    SurveyContent.SurveyType = SurveyType.SurveyTwo;
                    SurveyContent.SurveySection = SurveySection.SectionOne;
                    IsSurveyOpen = true;
                }
                else if (state.Survey3)
                {
                    Task = "Umfragenkatalog 3 erhältlich";
                    Preferences.Set("Survey", "2");
                    SurveyContent.SurveyType = SurveyType.SurveyThree;
                    SurveyContent.SurveySection = SurveySection.SectionOne;
                    IsSurveyOpen = true;
                }
                else if (state.Survey4)
                {
                    Task = "Umfragenkatalog 4 erhältlich";
                    Preferences.Set("Survey", "3");
                    Preferences.Set("Survey4Open", true);
                    IsSurveyOpen = true;
                }
                else
                {
                    IsSurveyOpen = false;
                    Preferences.Set("Survey", null);
                }
            }
        }

        // Command handler for opening FAQ page.
        async void OpenFAQCommand()
        {
            AreElementsVisible = false;
            IsBusy = true;
            Navigator.PreviousPage.Push(Previous.Main);
            await Navigator.ShellGoTo("Addition");
            AreElementsVisible = true;
            IsBusy = false;
        }

        // Command handler for opening settings page.
        async void OpenSettingsCommand()
        {
            AreElementsVisible = false;
            IsBusy = true;
            Navigator.PreviousPage.Push(Previous.Main);
            await Navigator.ShellGoTo("Einstellungen");
            AreElementsVisible = true;
            IsBusy = false;
        }

        // Command handler for opening diary page.
        private async void OpenDiaryCommand()
        {
            AreElementsVisible = false;
            IsBusy = true;
            Navigator.PreviousPage.Push(Previous.Main);
            await Navigator.ShellGoTo("Tagebucheintrag verfassen");
            AreElementsVisible = true;
            IsBusy = false;
        }

        // Command handler for opening survey page.
        private async void OpenSurveyCommand()
        {
            AreElementsVisible = false;
            IsBusy = true;
            Navigator.PreviousPage.Push(Previous.Main);
            if (await AreSurveysClosed())
            {
                IsSurveyOpen = false;
                Preferences.Set("Survey", null);
                await Navigator.ShellGoTo("Umfrage beantworten");
            }
            else
            {
                IsSurveyOpen = true;
                await Navigator.ShellGoTo("Umfrage beantworten");
            }
            AreElementsVisible = true;
            IsBusy = false;
        }

        // Method to check if all surveys are closed.
        private async Task<bool> AreSurveysClosed()
        {
            SurveyState state = await Database.IsSuveyOpen(Preferences.Get("loginToken", null));

            if (state != null)
            {
                if (state.Survey1 == false && state.Survey2 == false && state.Survey3 == false && state.Survey4 == false)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
