
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Desive2.Services
{
    using Xamarin.Essentials;

    public class CurrentProfilePic
    {
        // Property to store the current profile picture, with a default value retrieved from Preferences.
        public static string Picture { get; set; } = Preferences.Get("picture", "neutral.png");

        /// <summary>
        /// Retrieves the current profile picture.
        /// </summary>
        /// <returns>The file name or path of the current profile picture.</returns>
        public static string GetPicture()
        {
            return Picture;
        }

        /// <summary>
        /// Sets a new profile picture and stores it in Preferences for future access.
        /// </summary>
        /// <param name="newPicture">The new picture to set as the profile picture.</param>
        public static void SetPicture(string newPicture)
        {
            // Update the Picture property and store the new value in Preferences.
            Picture = newPicture;
            Preferences.Set("picture", newPicture);
        }
    }

}
