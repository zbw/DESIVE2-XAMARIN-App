using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Desive2.ViewModels.Diary
{
    /// <summary>
    /// ViewModel for the Editor page, handling the visibility of the audio component based on the file upload type.
    /// </summary>
    class EditorViewModel : BindableObject
    {
        private bool isAudioVisible = false;

        /// <summary>
        /// Gets or sets a value indicating whether the audio component is visible.
        /// </summary>
        public bool IsAudioVisible
        {
            get { return isAudioVisible; }
            set
            {
                isAudioVisible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorViewModel"/> class.
        /// Sets the visibility of the audio component based on the file upload type.
        /// </summary>
        public EditorViewModel()
        {
            // If the upload type is VoiceMail, set IsAudioVisible to false
            if (FilePathHandler.UploadType == Objects.Type.VoiceMail)
            {
                IsAudioVisible = false;
            }
            else
            {
                // Otherwise, set IsAudioVisible to true
                IsAudioVisible = true;
            }
        }
    }

}
