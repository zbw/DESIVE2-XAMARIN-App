using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using Desive2.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    public class PDFPageViewModel : ExtendedBindableObject
    {
        // Private backing field for the IsBusy property.
        private bool _isBusy;

        // Property to get and set the profile picture of the current user.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); }
        }

        // Property to manage the busy state of the view model.
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy); // Notifies that the IsBusy property has changed.
            }
        }

        // Private backing field for the SlIsVisible property.
        private bool slIsVisible;

        // Property to manage the visibility of a specific UI element (e.g., a loading indicator).
        public bool SlIsVisible
        {
            get { return slIsVisible; }
            set { slIsVisible = value; OnPropertyChanged(); }
        }

        // Constructor to initialize commands and default values for the properties.
        public PDFPageViewModel()
        {
            PDFPicker = new Command(PickPDF); // Command to trigger PDF selection.
            PDFTapped = new Command(PDFTappedCommand); // Command to open the selected PDF.
            UploadPDF = new Command(async () => await Upload()); // Command to handle PDF upload asynchronously.

            MyMenu = SwipeViewMenu.GetMenus(); // Fetch menu items for the swipe view.
            IsBusy = false; // Set the default value for IsBusy.
            slIsVisible = true; // Set the default value for SlIsVisible.
        }

        // Property to hold the logo image.
        public ByteImage Logo { get; set; }

        // Property to hold the list of menu items for the swipe view.
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Property to manage the visibility of the upload button.
        private bool isUploadVisible = false;
        public bool IsUploadVisible
        {
            get => isUploadVisible;
            set
            {
                isUploadVisible = value;
                OnPropertyChanged(); // Notifies that the IsUploadVisible property has changed.
            }
        }

        // Property for the button text, which can change based on PDF selection.
        private string buttonText = "PDF auswählen";
        public string ButtonText
        {
            get => buttonText;
            set
            {
                buttonText = value;
                OnPropertyChanged(); // Notifies that the ButtonText property has changed.
            }
        }

        // Property for the title, can be used to set the page title.
        public string Title { get; set; }

        // Property for the PDF source path.
        public string PDFSource { get; set; }

        // Private backing field for the filename property.
        private string _filename = "";
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; OnPropertyChanged(); } // Notifies that the Filename property has changed.
        }

        // Command to pick a PDF file using the file picker.
        public ICommand PDFPicker { get; set; }

        // Command to handle the PDF upload process.
        public ICommand UploadPDF { get; set; }

        // Command to handle PDF tapping actions, such as opening the PDF.
        public ICommand PDFTapped { get; set; }

        // Private backing field for the Image property.
        private ImageSource _image;

        // Property to hold the image of the selected PDF.
        public ImageSource Image
        {
            get => _image;
            private set { _image = value; OnPropertyChanged(); } // Notifies that the Image property has changed.
        }

        // Property to hold the request body (byte array) of the uploaded PDF.
        public byte[] RequestBody { get; set; }

        // Asynchronous method to pick a PDF file using a custom file picker.
        async void PickPDF()
        {
            try
            {
                // Define custom file types for different platforms.
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] {"com.adobe.pdf" } },
                { DevicePlatform.Android, new[] {"application/pdf" } },
                { DevicePlatform.UWP, new[] {".pdf" } },
                { DevicePlatform.Tizen, new[] {"*/*" } },
                { DevicePlatform.macOS, new[] {"pdf" } }
            });

                // Pick a file using the custom file types.
                var pickResult = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = customFileType,
                    PickerTitle = "Wählen Sie ein PDF aus"
                });

                // If a file is selected, update properties and read the file.
                if (pickResult != null)
                {
                    var stream = await pickResult.OpenReadAsync();
                    Image = ImageSource.FromStream(() => stream); // Display the PDF as an image.
                    var path = pickResult.FullPath;
                    PDFSource = path; // Set the PDF source path.
                    Filename = pickResult.FileName; // Set the selected file's name.
                    RequestBody = File.ReadAllBytes(path); // Read the file's content into a byte array.

                    IsUploadVisible = true; // Show the upload button.
                    ButtonText = "Anderes PDF hochladen"; // Change the button text to allow uploading another PDF.
                }
            }
            catch
            {
                // Display an error message if an exception occurs.
                await App.Current.MainPage.DisplayAlert("Fehler", "Bitte überprüfen Sie Ihre Internetverbindung.", "Okay");
            }
        }

        // Asynchronous method to handle the PDF upload process.
        private async Task Upload()
        {
            try
            {
                SurveyContent.Reset(); // Reset the survey content.
                FilePathHandler.Reset(); // Reset the file path handler.
                string user = Preferences.Get("idUser", null); // Retrieve the user ID from preferences.

                if (user != null)
                {
                    IsBusy = true; // Set the IsBusy flag to true during the upload process.
                    SlIsVisible = false; // Hide the loading indicator.

                    // Upload the PDF and save its ID to the file path handler.
                    FilePathHandler.IdUpload = await Database.UploadPDF(user, PDFSource, Preferences.Get("loginToken", null));
                    FilePathHandler.IdDiaryEntry = await Database.UploadDiaryEntry(SurveyContent.GetDiaryJson(), Preferences.Get("loginToken", null), FilePathHandler.IdUpload);
                }

                // After a successful upload, navigate to the PDF page and reset survey content.
                FilePathHandler.UploadType = Objects.Type.PDF;
                Navigator.PreviousPage.Push(Previous.PDF);
                SurveyContent.SurveyCount = 0;
                SurveyContent.DiaryPath = DiaryPath.PathOne;
                SurveyContent.GoToNextDiary(SurveyContent.DiaryPath);

                IsBusy = false; // Set the IsBusy flag to false after the upload.
                SlIsVisible = true; // Show the loading indicator again.
                IsUploadVisible = false; // Hide the upload button.
                ButtonText = "PDF auswählen"; // Reset the button text.
                Filename = ""; // Reset the filename.
                Image = ""; // Reset the image.
            }
            catch
            {
                // Display an error message if an exception occurs during the upload.
                await App.Current.MainPage.DisplayAlert("Fehler", "Bitte überprüfen Sie Ihre Internetverbindung.", "Okay");
                IsBusy = false; // Set the IsBusy flag to false.
                SlIsVisible = true; // Show the loading indicator again.
                IsUploadVisible = false; // Hide the upload button.
                ButtonText = "PDF auswählen"; // Reset the button text.
            }
        }

        // Asynchronous method to open the tapped PDF file.
        private async void PDFTappedCommand()
        {
            if (PDFSource != null)
            {
                // Open the selected PDF using the Launcher.
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(PDFSource)
                });
            }
        }
    }

}
