using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    /// <summary>
    /// ViewModel for managing the logic and commands for the DiaryPage.
    /// </summary>
    class DiaryPageViewModel : BindableObject
    {
        // Property for storing and binding the profile picture.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); }
        }

        // List for storing swipe menu options.
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Property for binding the task name.
        private string task = "";
        public string Task
        {
            get { return task; }
            set { task = value; OnPropertyChanged(); }
        }

        // Properties for controlling visibility of UI elements.
        private bool areElementsVisible = true;
        public bool AreElementsVisible
        {
            get { return areElementsVisible; }
            set { areElementsVisible = value; OnPropertyChanged(); }
        }

        // Property for controlling the busy state (e.g., showing loading indicator).
        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        // ICommand properties for the various buttons in the UI.
        public ICommand OpenSettings { get; set; }
        public ICommand OpenScreenshot { get; set; }
        public ICommand OpenAudio { get; set; }
        public ICommand OpenPDF { get; set; }
        public ICommand OpenAdditionalInfos { get; set; }

        // Constructor for initializing the commands and swipe menu.
        public DiaryPageViewModel()
        {
            MyMenu = SwipeViewMenu.GetMenus();
            OpenSettings = new Command(OpenSettingsCommand);
            OpenAudio = new Command(OpenAudioCommand);
            OpenScreenshot = new Command(OpenScreenshotCommand);
            OpenPDF = new Command(OpenPDFCommand);
            OpenAdditionalInfos = new Command(OpenAdditionalInfosCommand);
        }

        // Command handler for opening the PDF page.
        async void OpenPDFCommand()
        {
            AreElementsVisible = false; // Hide elements to show loading state.
            IsBusy = true; // Show busy state.
            Navigator.PreviousPage.Push(Previous.DiaryPage); // Save current page for navigation back.
            await Navigator.ShellGoTo("PDF"); // Navigate to PDF page.
            AreElementsVisible = true; // Show elements again.
            IsBusy = false; // Hide busy state.
        }

        // Command handler for opening the settings page.
        async void OpenSettingsCommand()
        {
            AreElementsVisible = false;
            IsBusy = true;
            await Navigator.ShellGoTo("Einstellungen"); // Navigate to settings page.
            Navigator.PreviousPage.Push(Previous.DiaryPage); // Save current page for navigation back.
            AreElementsVisible = true;
            IsBusy = false;
        }

        // Command handler for opening the additional information page.
        async void OpenAdditionalInfosCommand()
        {
            AreElementsVisible = false;
            IsBusy = true;
            await Navigator.ShellGoTo("Addition"); // Navigate to additional info page.
            Navigator.PreviousPage.Push(Previous.DiaryPage); // Save current page for navigation back.
            AreElementsVisible = true;
            IsBusy = false;
        }

        // Command handler for opening the screenshot upload page.
        private async void OpenScreenshotCommand()
        {
            AreElementsVisible = false;
            IsBusy = true;
            Navigator.PreviousPage.Push(Previous.DiaryPage); // Save current page for navigation back.
            await Navigator.ShellGoTo("Bild hochladen"); // Navigate to screenshot upload page.
            AreElementsVisible = true;
            IsBusy = false;
        }

        // Command handler for opening the audio recording page.
        private async void OpenAudioCommand()
        {
            AreElementsVisible = false;
            IsBusy = true;
            Navigator.PreviousPage.Push(Previous.DiaryPage); // Save current page for navigation back.
            await Navigator.ShellGoTo("Sprachnotiz"); // Navigate to audio recording page.
            AreElementsVisible = true;
            IsBusy = false;
        }
    }

}
