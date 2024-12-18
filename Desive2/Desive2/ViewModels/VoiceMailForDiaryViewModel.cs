
using Android.Content.Res;
using Com.CleverPush.Abstractions;
using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.ViewModels
{
    public class VoiceMailForDIaryViewModel : BindableObject
    {
        public string ProfilePicture
        {
            get { return CurrentProfilePic.Picture; }
            private set { CurrentProfilePic.Picture = value; OnPropertyChanged(); }
        }
        private int sessionCount = 0;
        public Task<string> Audiofile { get; set; }
        private bool _isTimerRunning = false;
        public bool IsTimerRunning
        {
            get
            {
                return _isTimerRunning;
            }
            private set
            {
                _isTimerRunning = value;
                OnPropertyChanged();
            }
        }

        Plugin.AudioRecorder.AudioPlayer iOSplayer;
        AudioRecorder.AudioPlayer androidPlayer;
        int second = 0, minute = 0;

        #region Properties

        private string minutes = "00";
        public string Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                minutes = value;
                OnPropertyChanged();
            }
        }
        private string seconds = "00";
        public string Seconds
        {
            get
            {
                return seconds;
            }
            set
            {
                seconds = value;
                OnPropertyChanged();
            }
        }
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        private bool _isGridVisible = true;
        public bool IsGridVisible
        {
            get
            {
                return _isGridVisible;
            }
            set { _isGridVisible = value; OnPropertyChanged(); }
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set { _isBusy = value; OnPropertyChanged(); }
        }


        private bool isDeleteVisible = false;
        public bool IsDeleteVisible
        {
            get => isDeleteVisible;
            set { isDeleteVisible = value; OnPropertyChanged(); }
        }
        public ICommand StartRecording { get; set; }
        public ICommand UploadAudio { get; set; }
        public ICommand StopRecording { get; set; }
        public ICommand PlayAudio { get; set; }
        public ICommand DeleteVoiceMail { get; set; }
        private bool isRecordingVisible = true;
        public bool IsRecordingVisible
        {
            get => isRecordingVisible;
            set
            {
                isRecordingVisible = value;
                OnPropertyChanged();
            }
        }

        private bool isStopVisible = false;
        public bool IsStopVisible
        {
            get => isStopVisible;
            set
            {
                isStopVisible = value; ;
                OnPropertyChanged();
            }
        }

        private bool stopVisible = true;
        public bool StopVisible
        {
            get => stopVisible;
            set
            {
                stopVisible = value; ;
                OnPropertyChanged();
            }
        }


        private bool hasPermission = false;



        #endregion
        public Plugin.AudioRecorder.AudioRecorderService iOSrecorder { get; private set; }
        public AudioRecorder.AudioRecorderService androidRecorder { get; private set; }
        public VoiceMailForDIaryViewModel()
        {
            Title = "Sprachnotiz aufnehmen";
            UploadAudio = new Command(AudioUploadCommand);
            StopRecording = new Command(StopRecordingCommand);
            StartRecording = new Command(StartRecordingCommand);
            PlayAudio = new Command(PlayAudioCommand);
            MyMenu = SwipeViewMenu.GetMenus();
            DeleteVoiceMail = new Command(DeleteVoicemailCommand);
            if (Device.RuntimePlatform == Device.iOS)
            {

                iOSrecorder = new Plugin.AudioRecorder.AudioRecorderService
                {
                    StopRecordingOnSilence = false,
                    StopRecordingAfterTimeout = true,
                    TotalAudioTimeout = TimeSpan.FromSeconds(180),
                    SilenceThreshold = 0,
                    FilePath = "ARS_recordingSec.wav",

                };
                iOSplayer = new Plugin.AudioRecorder.AudioPlayer();

                iOSplayer.FinishedPlaying += Finish_Playing;
            }
            else
            {
                androidRecorder = new AudioRecorder.AudioRecorderService
                {
                    StopRecordingOnSilence = false,
                    StopRecordingAfterTimeout = true,
                    TotalAudioTimeout = TimeSpan.FromSeconds(180),
                    SilenceThreshold = 0,
                    FilePath = "data/user/0/de.zbw.desive2/cache/ARS_recordingSec.wav"
                };
                androidPlayer = new AudioRecorder.AudioPlayer();
                androidPlayer.FinishedPlaying += Finish_Playing;
            }
            var task = CheckPermission();
        }

        private async Task CheckPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Microphone>();
                if (status == PermissionStatus.Granted)
                {
                    hasPermission = true;
                }

            }
            else
            {
                hasPermission = true;
            }
        }

        public List<SwipeViewMenu> MyMenu { get; set; }
        private Task<string> recordTask;
        private bool isPlaying = false;

        private async void AudioUploadCommand()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {

                if (!iOSrecorder.IsRecording)
                {
                    IsStopVisible = false;
                    IsRecordingVisible = true;
                }

            }
            else
            {
                if (!androidRecorder.IsRecording)
                {
                    try
                    {
                        IsBusy = true;
                        IsGridVisible = false;
                        var filepath = "";
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            filepath = iOSrecorder.GetAudioFilePath();
                        }
                        else
                        {
                            filepath = androidRecorder.GetAudioFilePath();

                        }
                        CookieContainer cookies = new CookieContainer();
                        //add or use cookies
                        NameValueCollection querystring = new NameValueCollection();
                        //querystring["idAppUser"] = Preferences.Get("idUser", null);

                        //everything except upload file and url can be left blank if needed
                        FilePathHandler.UploadType = Objects.Type.VoiceMail;
                        FilePathHandler.IdUploadAdditionalInformation = await Database.UploadFileEx( filepath, Preferences.Get("loginToken", null));

                        if (FilePathHandler.IdUploadAdditionalInformation != -1 && FilePathHandler.IdUploadAdditionalInformation != -2)
                        {
                            IsRecordingVisible = true;
                            IsStopVisible = false;
                            Response res = await Database.ModifyDiaryEntry(FilePathHandler.IdDiaryEntry, FilePathHandler.IdUploadAdditionalInformation, Preferences.Get("loginToken", null), SurveyContent.GetDiaryJson());

                            
                            await App.Current.MainPage.DisplayAlert("Vielen Dank!", "Ihre Antworten wurden erfolgreich hochgeladen.", "Okay");
                               
                                FilePathHandler.IdUpload = 0;
                                FilePathHandler.IdDiaryEntry = 0;
                                FilePathHandler.IdUploadAdditionalInformation = 0;
                                SurveyContent.Reset();


                            FilePathHandler.UploadType = Objects.Type.VoiceMail;
                            Navigator.PreviousPage = new Stack<Previous>();
                            sessionCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Fehler", ex.Message, "Okay");
                    }
                    finally
                    {
                        Seconds = "00";
                        Minutes = "00";
                        IsTimerRunning = false;
                        IsRecordingVisible = true;
                        IsStopVisible = false;

                        IsBusy = false;
                        IsGridVisible = true;
                    }
                }

            }

        }

        private async void StartRecordingCommand()
        {
            if (!isPlaying)
            {

                var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
                if (sessionCount > 0)
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {

                    }
                    else
                    {
                        androidRecorder.FilePath = "data/user/0/de.zbw.desive2/cache/ARS_recording" + sessionCount + ".wav";
                    }
                }
                if (status == PermissionStatus.Granted)
                {
                    IsRecordingVisible = false;
                    IsStopVisible = true;
                    try
                    {
                        second = 0;
                        minute = 0;
                        IsTimerRunning = true;

                        StartTimer();
                        IsRecordingVisible = false;
                        IsStopVisible = true;
                        StopVisible = true;
                        IsDeleteVisible = false;

                        await RecordAudio();
                    }
                    catch
                    {
                        await App.Current.MainPage.DisplayAlert("Fehler", "Bitte überprüfen Sie Ihre Internetverbindung.", "Okay");
                    }
                }
                else
                {
                    status = await Permissions.RequestAsync<Permissions.Microphone>();
                }
            }
        }

        private async Task RecordAudio()
        {

            if (Device.RuntimePlatform == Device.iOS)
            {

                if (!iOSrecorder.IsRecording)
                {
                    recordTask = await iOSrecorder.StartRecording();
                }
                else
                {
                    IsTimerRunning = false;
                    await iOSrecorder.StopRecording();
                }
            }
            else
            {
                if (!androidRecorder.IsRecording)
                {
                    recordTask = await androidRecorder.StartRecording();
                }
                else
                {
                    IsTimerRunning = false;
                    await androidRecorder.StopRecording();
                }
            }
        }

        private void StartTimer()
        {

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (IsTimerRunning)
                {

                    second++;
                    if (second.ToString().Length == 1)
                    {
                        Seconds = "0" + second.ToString();
                    }
                    else
                    {
                        Seconds = second.ToString();
                    }

                    if (second == 60)
                    {
                        minute++;
                        second = 0;

                        if (minute.ToString().Length == 1)
                        {
                            Minutes = "0" + minute.ToString();
                        }
                        else
                        {
                            Minutes = minute.ToString();
                        }

                        Seconds = "00";
                    }
                }
                return IsTimerRunning;
            });
        }
        private async void StopRecordingCommand()
        {

            StopVisible = false;
            IsDeleteVisible = true;
            IsTimerRunning = false;
            //Seconds = "00";
            //Minutes = "00";
            //second = 0;
            //minute = 0;

            await RecordAudio();


        }
        private async void PlayAudioCommand()
        {
            try
            {
                if (Device.RuntimePlatform == Device.iOS)
                {

                    if (!iOSrecorder.IsRecording)
                    {
                        isPlaying = true;
                        var filePath = "";
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            filePath = iOSrecorder.GetAudioFilePath();
                        }
                        else
                        {
                            filePath = androidRecorder.GetAudioFilePath();
                        }
                        if (filePath != null)
                        {
                            IsTimerRunning = true;


                            second = 0;
                            minute = 0;
                            Seconds = "00";
                            Minutes = "00";
                            StartTimer();



                            if (Device.RuntimePlatform == Device.iOS)
                            {
                                iOSplayer.Play(filePath);
                            }
                            else
                            {
                                androidPlayer.Play(filePath);
                            }


                            second = 0;
                            minute = 0;
                            Seconds = "00";
                            Minutes = "00";
                        }
                    }

                }
                else
                {
                    if (!androidRecorder.IsRecording)
                    {
                        isPlaying = true;
                        var filePath = "";
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            filePath = iOSrecorder.GetAudioFilePath();
                        }
                        else
                        {
                            filePath = androidRecorder.GetAudioFilePath();
                        }
                        if (filePath != null)
                        {
                            if (!IsTimerRunning)
                            {
                                IsTimerRunning = true;


                                second = 0;
                                minute = 0;
                                Seconds = "00";
                                Minutes = "00";
                                StartTimer();



                                if (Device.RuntimePlatform == Device.iOS)
                                {
                                    iOSplayer.Play(filePath);
                                }
                                else
                                {
                                    androidPlayer.Play(filePath);
                                }


                                second = 0;
                                minute = 0;
                                Seconds = "00";
                                Minutes = "00";
                            }
                        }
                    }

                }

            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Fehler", "Bitte überprüfen Sie Ihre Internetverbindung.", "Okay");
            }
        }


        private void Finish_Playing(object sender, EventArgs e)
        {

            IsTimerRunning = false;

            isPlaying = false;
        }

        private void DeleteVoicemailCommand()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {

                if (!iOSrecorder.IsRecording)
                {
                    IsStopVisible = false;
                    IsRecordingVisible = true;
                }

            }
            else
            {
                if (!androidRecorder.IsRecording)
                {
                    IsStopVisible = false;
                    IsRecordingVisible = true;
                }

            }


        }




    }
}
