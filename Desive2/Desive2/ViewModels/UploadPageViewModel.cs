using Android.Content.Res;
using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using Desive2.Views.DiaryQuestions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    public class UploadPageViewModel : ExtendedBindableObject
    {
        // Property for getting and setting the user's profile picture.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); }
        }

        // Property to track whether the page is in a busy state.
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        // Property to control visibility of the close button.
        private bool _isCloseVisible = false;
        public bool IsCloseVisible
        {
            get
            {
                return _isCloseVisible;
            }
            set { _isCloseVisible = value; OnPropertyChanged(); }
        }

        // Property to control visibility of the progress spinner.
        private bool slIsVisible;
        public bool SlIsVisible
        {
            get { return slIsVisible; }
            set { slIsVisible = value; OnPropertyChanged(); }
        }

        // Constructor to initialize the commands and properties.
        public UploadPageViewModel()
        {
            PhotoPicker = new Command(PickPhoto);
            RemovePicture = new Command(RemovePictureCommand);
            UploadPicture = new Command(async () => await Upload());
            Image.Source = "imageCollection.png"; // Default image placeholder.
            MyMenu = SwipeViewMenu.GetMenus();
            IsBusy = false;
            slIsVisible = true;
        }

        // Property for the logo image.
        public ByteImage Logo { get; set; }

        // List of menu items for swipe menu.
        public List<SwipeViewMenu> MyMenu { get; set; }

        // Property to control visibility of the upload section.
        private bool isUploadVisible = false;
        public bool IsUploadVisible
        {
            get => isUploadVisible;
            set
            {
                isUploadVisible = value; ;
                OnPropertyChanged();
            }
        }

        // Property for the button text.
        private string buttonText = "Bild auswählen";
        public string ButtonText
        {
            get => buttonText;
            set
            {
                buttonText = value;
                OnPropertyChanged();
            }
        }

        // Title property.
        public string Title { get; set; }

        // Command for picking a photo.
        public ICommand PhotoPicker { get; set; }

        // Command for uploading the picture.
        public ICommand UploadPicture { get; set; }

        // Command for removing the picture.
        public ICommand RemovePicture { get; set; }

        // Property for the image being displayed.
        private Image _image = new Image();
        public Image Image
        {
            get => _image;
            private set { _image = value; OnPropertyChanged(); }
        }

        // Property for storing the Base64 representation of the image.
        public string Base64 { get; set; }

        // Method for picking a photo and converting it to a Base64 string.
        async void PickPhoto()
        {
            try
            {
                // Fetch image stream using a dependency service.
                Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
                if (stream != null)
                {
                    // Convert the image to a byte array and then to Base64.
                    using (MemoryStream memory = new MemoryStream())
                    {
                        stream.CopyTo(memory);
                        byte[] bytes = memory.ToArray();
                        Image.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                        FilePathHandler.LocalPicturePath.Source = Image.Source;
                        Base64 = System.Convert.ToBase64String(bytes); // Store the Base64 string.
                    }

                    // Update the UI visibility.
                    IsUploadVisible = true;
                    IsCloseVisible = true;
                    ButtonText = "Anderes Bild hochladen";
                }

            }
            catch
            {
                // Display error message in case of failure.
                await App.Current.MainPage.DisplayAlert("Fehler", "Bitte überprüfen Sie Ihre Internetverbindung.", "Okay");
            }
        }

        // Method to upload the image and related data.
        private async Task Upload()
        {
            try
            {
                // Reset prior data.
                SurveyContent.Reset();
                FilePathHandler.Reset();
                string user = Preferences.Get("idUser", null); // Get the user ID from preferences.
                IsCloseVisible = false;

                // Show loading indicator and hide other UI elements.
                IsBusy = true;
                SlIsVisible = false;

                // Upload the image and diary entry.
                FilePathHandler.IdUpload = await Database.UploadPicture(Base64, Preferences.Get("loginToken", null));
                FilePathHandler.IdDiaryEntry = await Database.UploadDiaryEntry(SurveyContent.GetDiaryJson(), Preferences.Get("loginToken", null), FilePathHandler.IdUpload);

                // Handle errors during upload.
                if (FilePathHandler.IdUpload == -1 || FilePathHandler.IdDiaryEntry == -1)
                {
                    throw new System.Exception();
                }

                // Navigate to the next page after successful upload.
                FilePathHandler.UploadType = Type.Picture;
                Navigator.PreviousPage.Push(Previous.Picture);
                SurveyContent.SurveyCount = 0;
                SurveyContent.DiaryPath = DiaryPath.PathOne;
                SurveyContent.GoToNextDiary(SurveyContent.DiaryPath);

                // Reset UI state.
                IsBusy = false;
                SlIsVisible = true;
                Image.Source = "imageCollection.png";
                IsUploadVisible = false;
                ButtonText = "Bild auswählen";
            }
            catch
            {
                // Handle errors during the upload process.
                await App.Current.MainPage.DisplayAlert("Fehler", "Bitte überprüfen Sie Ihre Internetverbindung oder das Dateiformat.", "Okay");
                IsBusy = false;
                SlIsVisible = true;
                Image.Source = "imageCollection.png";
                IsUploadVisible = false;
                ButtonText = "Bild auswählen";
            }
        }

        // Method to remove the selected picture.
        private void RemovePictureCommand()
        {
            Image.Source = "imageCollection.png"; // Reset to default image.
            SlIsVisible = true;
            Image.Source = "imageCollection.png";
            IsUploadVisible = false;
            ButtonText = "Bild auswählen";
            IsCloseVisible = false;
        }
    }

}
