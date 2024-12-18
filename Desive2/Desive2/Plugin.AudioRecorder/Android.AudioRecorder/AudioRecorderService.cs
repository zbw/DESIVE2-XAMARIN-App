using Android.Content;
using Android.Media;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Desive2.AudioRecorder
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Android.Content;
    using Android.Media;
    using Android.Util;

    public partial class AudioRecorderService
    {
        // The sample rate to be used for recording audio
        public int PreferredSampleRate { get; private set; }

        // Default file name for the audio file
        private const string DefaultFileName = "recorded_audio.wav";

        /// <summary>
        /// Initializes the audio recorder service, determining the preferred sample rate from the system.
        /// </summary>
        partial void Init()
        {
            // Only perform this initialization if the Android SDK version is JellyBean or higher
            if (Android.OS.Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.JellyBean)
            {
                try
                {
                    // Access the AudioManager system service to retrieve properties related to audio output
                    var audioManager = (AudioManager)Android.App.Application.Context.GetSystemService(Context.AudioService);
                    var property = audioManager.GetProperty(AudioManager.PropertyOutputSampleRate);

                    // If the property value is valid, parse it into an integer and use it as the preferred sample rate
                    if (!string.IsNullOrEmpty(property) && int.TryParse(property, out int sampleRate))
                    {
                        Debug.WriteLine($"Setting PreferredSampleRate to {sampleRate} as reported by AudioManager.PropertyOutputSampleRate");
                        PreferredSampleRate = sampleRate;  // Set the preferred sample rate
                    }
                }
                catch (Exception ex)
                {
                    // If an error occurs while accessing the AudioManager, log the error and retain the default sample rate
                    Debug.WriteLine("Error using AudioManager to get AudioManager.PropertyOutputSampleRate: {0}", ex);
                    Debug.WriteLine("PreferredSampleRate will remain at the default");
                }
            }
        }

        /// <summary>
        /// Retrieves the default file path for the audio recording.
        /// </summary>
        /// <returns>Task containing the file path.</returns>
        Task<string> GetDefaultFilePath()
        {
            // Combine the temporary directory path with the default file name to generate the full file path
            return Task.FromResult(Path.Combine(Path.GetTempPath(), DefaultFileName));
        }

        /// <summary>
        /// Placeholder method to handle actions when recording starts.
        /// </summary>
        void OnRecordingStarting()
        {
            // Custom logic to handle when the recording starts can be added here
        }

        /// <summary>
        /// Placeholder method to handle actions when recording stops.
        /// </summary>
        void OnRecordingStopped()
        {
            // Custom logic to handle when the recording stops can be added here
        }
    }

}
