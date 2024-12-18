using Desive2.Services;
using Xamarin.Essentials;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin;
using Xamarin.Forms;
using Desive2.Models;


namespace Desive2.ViewModels
{
    public class SelectPictureViewModel : BindableObject
    {
        // Property to get and set the current profile picture.
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); } // Notifies that the ProfilePicture property has changed.
        }

        // Private backing field for visibility of the first checkbox.
        private bool _isFirstCheckVisible = false;

        // Property for getting and setting the visibility of the first checkbox.
        public bool IsFirstCheckVisible
        {
            get { return _isFirstCheckVisible; }
            set { _isFirstCheckVisible = value; OnPropertyChanged(); }
        }

        // Other visibility properties for each checkbox.
        private bool _isSecondCheckVisible = false;
        public bool IsSecondCheckVisible
        {
            get { return _isSecondCheckVisible; }
            set { _isSecondCheckVisible = value; OnPropertyChanged(); }
        }

        private bool _isThirdCheckVisible = false;
        public bool IsThirdCheckVisible
        {
            get { return _isThirdCheckVisible; }
            set { _isThirdCheckVisible = value; OnPropertyChanged(); }
        }

        private bool _isFourthCheckVisible = false;
        public bool IsFourthCheckVisible
        {
            get { return _isFourthCheckVisible; }
            set { _isFourthCheckVisible = value; OnPropertyChanged(); }
        }

        private bool _isFifthCheckVisible = false;
        public bool IsFifthCheckVisible
        {
            get { return _isFifthCheckVisible; }
            set { _isFifthCheckVisible = value; OnPropertyChanged(); }
        }

        private bool _isSixthCheckVisible = false;
        public bool IsSixthCheckVisible
        {
            get { return _isSixthCheckVisible; }
            set { _isSixthCheckVisible = value; OnPropertyChanged(); }
        }

        private bool _isSeventhCheckVisible = false;
        public bool IsSeventhCheckVisible
        {
            get { return _isSeventhCheckVisible; }
            set { _isSeventhCheckVisible = value; OnPropertyChanged(); }
        }

        private bool _isEighthCheckVisible = false;
        public bool IsEighthCheckVisible
        {
            get { return _isEighthCheckVisible; }
            set { _isEighthCheckVisible = value; OnPropertyChanged(); }
        }

        private bool _isNinthCheckVisible = false;
        public bool IsNinthCheckVisible
        {
            get { return _isNinthCheckVisible; }
            set { _isNinthCheckVisible = value; OnPropertyChanged(); }
        }

        // Commands for selecting different pictures.
        public ICommand SelectFirstPicture { get; set; }
        public ICommand SelectSecondPicture { get; set; }
        public ICommand SelectThirdPicture { get; set; }
        public ICommand SelectFourthPicture { get; set; }
        public ICommand SelectFifthPicture { get; set; }
        public ICommand SelectSixthPicture { get; set; }
        public ICommand SelectSeventhPicture { get; set; }
        public ICommand SelectEigthPicture { get; set; }
        public ICommand SelectNinthPicture { get; set; }

        // Constructor to initialize the commands and check the initial profile picture.
        public SelectPictureViewModel()
        {
            // Initialize commands.
            SelectFirstPicture = new Command(SelectFirstPictureCommand);
            SelectSecondPicture = new Command(SelectSecondPictureCommand);
            SelectThirdPicture = new Command(SelectThirdPictureCommand);
            SelectFourthPicture = new Command(SelectFourthPictureCommand);
            SelectFifthPicture = new Command(SelectFifthPictureCommand);
            SelectSixthPicture = new Command(SelectSixthPictureCommand);
            SelectSeventhPicture = new Command(SelectSeventhPictureCommand);
            SelectEigthPicture = new Command(SelectEigthPictureCommand);
            SelectNinthPicture = new Command(SelectNinthPictureCommand);

            // Select the corresponding picture based on the current profile picture.
            if (CurrentProfilePic.Picture == "female1.png")
            {
                SelectFirstPictureCommand();
            }
            else if (CurrentProfilePic.Picture == "male1.png")
            {
                SelectSecondPictureCommand();
            }
            else if (CurrentProfilePic.Picture == "female2.png")
            {
                SelectThirdPictureCommand();
            }
            else if (CurrentProfilePic.Picture == "male2.png")
            {
                SelectFourthPictureCommand();
            }
            else if (CurrentProfilePic.Picture == "female3.png")
            {
                SelectFifthPictureCommand();
            }
            else if (CurrentProfilePic.Picture == "male3.png")
            {
                SelectSixthPictureCommand();
            }
            else if (CurrentProfilePic.Picture == "female4.png")
            {
                SelectSeventhPictureCommand();
            }
            else if (CurrentProfilePic.Picture == "male4.png")
            {
                SelectEigthPictureCommand();
            }
            else if (CurrentProfilePic.Picture == "neutral.png")
            {
                SelectNinthPictureCommand();
            }
        }

        // Methods for selecting each picture and updating the profile picture.
        private void SelectFirstPictureCommand()
        {
            SetInvisible(); // Hide all checkboxes.
            IsFirstCheckVisible = true; // Show the first checkbox.
            ProfilePicture = "female1.png"; // Set the profile picture.
            Preferences.Set("picture", "female1.png"); // Save the selected picture.
            count++; // Increment the count.

            // Toggle the API based on count.
            if (count >= 10)
            {
                testing = !testing;
                Database.ChangeAPI(testing); // Change the API.
                count = 0; // Reset the count.
            }
        }

        private void SelectSecondPictureCommand()
        {
            SetInvisible();
            IsSecondCheckVisible = true;
            ProfilePicture = "male1.png";
            Preferences.Set("picture", "male1.png");
        }

        private void SelectThirdPictureCommand()
        {
            SetInvisible();
            IsThirdCheckVisible = true;
            ProfilePicture = "female2.png";
            Preferences.Set("picture", "female2.png");
        }

        private void SelectFourthPictureCommand()
        {
            SetInvisible();
            IsFourthCheckVisible = true;
            ProfilePicture = "male2.png";
            Preferences.Set("picture", "male2.png");
        }

        private void SelectFifthPictureCommand()
        {
            SetInvisible();
            IsFifthCheckVisible = true;
            ProfilePicture = "female3.png";
            Preferences.Set("picture", "female3.png");
        }

        private void SelectSixthPictureCommand()
        {
            SetInvisible();
            IsSixthCheckVisible = true;
            ProfilePicture = "male3.png";
            Preferences.Set("picture", "male3.png");
        }

        private void SelectSeventhPictureCommand()
        {
            SetInvisible();
            IsSeventhCheckVisible = true;
            ProfilePicture = "female4.png";
            Preferences.Set("picture", "female4.png");
        }

        private void SelectEigthPictureCommand()
        {
            SetInvisible();
            IsEighthCheckVisible = true;
            ProfilePicture = "male4.png";
            Preferences.Set("picture", "male4.png");
        }

        private void SelectNinthPictureCommand()
        {
            SetInvisible();
            IsNinthCheckVisible = true;
            ProfilePicture = "neutral.png";
            Preferences.Set("picture", "neutral.png");
        }

        // Method to hide all checkboxes.
        private void SetInvisible()
        {
            IsFirstCheckVisible = false;
            IsSecondCheckVisible = false;
            IsThirdCheckVisible = false;
            IsFourthCheckVisible = false;
            IsFifthCheckVisible = false;
            IsSixthCheckVisible = false;
            IsSeventhCheckVisible = false;
            IsEighthCheckVisible = false;
            IsNinthCheckVisible = false;
        }
    }

}
