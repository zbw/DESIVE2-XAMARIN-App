using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Desive2.AudioRecorder
{
	internal class WaveRecorder : IDisposable
	{
		string audioFilePath;
        MemoryStream memoryAudioStream;
        BinaryWriter writer;
		int byteCount;
		IAudioStream audioStream;

		/// <summary>
		/// Starts recording WAVE format audio from the audio stream.
		/// </summary>
		/// <param name="stream">A <see cref="IAudioStream"/> that provides the audio data.</param>
		/// <param name="filePath">The full path of the file to record audio to.</param>
		public async Task StartRecorder (IAudioStream stream, string filePath)
		{
			if (stream == null)
			{
				throw new ArgumentNullException (nameof (stream));
			}

			try
			{
				//if we're restarting, let's see if we have an existing stream configured that can be stopped
				if (audioStream != null)
				{
					await audioStream.Stop ();
				}

				audioFilePath = filePath;
				audioStream = stream;

                memoryAudioStream = new MemoryStream();
                writer = new BinaryWriter(memoryAudioStream, Encoding.UTF8);

                byteCount = 0;
				audioStream.OnBroadcast += OnStreamBroadcast;
				audioStream.OnActiveChanged += StreamActiveChanged;

				if (!audioStream.Active)
				{
					await audioStream.Start ();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine ("Error in WaveRecorder.StartRecorder(): {0}", ex.Message);

				StopRecorder ();
				throw;
			}
		}

		/// <summary>
		/// Gets a new <see cref="Stream"/> to the audio file in readonly mode.
		/// </summary>
		/// <returns>A <see cref="Stream"/> object that can be used to read the audio file from the beginning.</returns>
		public Stream GetAudioFileStream ()
		{
			//return a new stream to the same audio file, in Read mode
			return new FileStream (audioFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		}

		void StreamActiveChanged (object sender, bool active)
		{
			if (!active)
			{
				StopRecorder ();
			}
		}

		void OnStreamBroadcast (object sender, byte [] bytes)
		{
			try
			{
                if (writer != null)
                {
					writer.Write (bytes);
					byteCount += bytes.Length;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine ("Error in WaveRecorder.OnStreamBroadcast(): {0}", ex.Message);

				StopRecorder ();
			}
		}

		/// <summary>
		/// Stops recording WAV audio from the underlying <see cref="IAudioStream"/> and finishes writing the WAV file.
		/// </summary>
		public void StopRecorder ()
		{
			try
			{
				if (audioStream != null)
				{
					audioStream.OnBroadcast -= OnStreamBroadcast;
					audioStream.OnActiveChanged -= StreamActiveChanged;
				}
				

				using (var fileStream = new FileStream(audioFilePath, FileMode.Create, FileAccess.Write))
				{
					using (var fileWriter = new BinaryWriter(fileStream, Encoding.UTF8))
					{
						//now that audio is finished recording, write a WAV/RIFF header at the beginning of the file
						AudioFunctions.WriteWavHeader(fileWriter, audioStream.ChannelCount, audioStream.SampleRate,
						audioStream.BitsPerSample, byteCount);
						memoryAudioStream.Seek(0, SeekOrigin.Begin);
						memoryAudioStream.CopyTo(fileStream);
					}
				}
                    audioStream = null;
			}
			catch (Exception ex)
			{
				Debug.WriteLine ("Error during StopRecorder: {0}", ex.Message);
				throw;
			}
		}

		public void Dispose ()
		{
			StopRecorder ();
		}
	}
}
