using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Desive2.Objects
{
    /// <summary>
    /// A static class that handles file paths and upload-related data.
    /// </summary>
    public static class FilePathHandler
    {
        /// <summary>
        /// Gets or sets the ID for the uploaded file.
        /// </summary>
        public static int IdUpload { get; set; }

        /// <summary>
        /// Gets or sets the ID for the diary entry associated with the upload.
        /// </summary>
        public static int IdDiaryEntry { get; set; }

        /// <summary>
        /// Gets or sets the ID for additional information associated with the upload.
        /// </summary>
        public static int IdUploadAdditionalInformation { get; set; }

        /// <summary>
        /// Gets or sets the local picture path (used for storing images).
        /// </summary>
        public static Image LocalPicturePath = new Image();

        /// <summary>
        /// Gets or sets the type of the upload (Picture, VoiceMail, or PDF).
        /// </summary>
        public static Type UploadType = Type.Picture;

        /// <summary>
        /// Resets all the static properties related to the file upload.
        /// </summary>
        public static void Reset()
        {
            IdUpload = 0;
            IdDiaryEntry = 0;
            IdUploadAdditionalInformation = 0;
        }
    }

    /// <summary>
    /// Enum representing the different types of uploads.
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// Represents a picture upload.
        /// </summary>
        Picture,

        /// <summary>
        /// Represents a voice mail upload.
        /// </summary>
        VoiceMail,

        /// <summary>
        /// Represents a PDF upload.
        /// </summary>
        PDF
    }
}

