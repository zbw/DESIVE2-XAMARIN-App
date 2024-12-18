using Desive2.Models;
using Desive2.Objects;
using Desive2.ViewModels;
using Desive2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Desive2.Services
{
    public class Navigator
    {
        public static Stack<Previous> PreviousPage = new Stack<Previous>();
        public static async Task<bool> ShellGoTo(string route)
        {
            if (route == "Startseite")
            {
                
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else if (route == "Über uns")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AboutPage());
            }
            else if (route == "Sprachnotiz")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new VoiceMailPage());
            }
            else if (route == "PDF")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PDFUploadPage());
            }
            else if (route == "Bild hochladen")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PictureUploadPage());
            }
            else if (route == "Umfrage beantworten")
            {
                SurveyState state = await Database.IsSuveyOpen(Preferences.Get("loginToken", null));
                if (state.Survey3 && state.Survey4)
                    Preferences.Set("Survey4Open", true);
                if (state.Survey1)
                {
                    SurveyContent.SurveyType = SurveyType.SurveyOne;
                    Preferences.Set("Survey", "0");
                }
                else if (state.Survey2)
                {
                    SurveyContent.SurveyType = SurveyType.SurveyTwo;
                    Preferences.Set("Survey", "1");
                }
                else if (state.Survey3)
                {
                    SurveyContent.SurveyType = SurveyType.SurveyThree;
                    Preferences.Set("Survey", "2");
                }
                else if (state.Survey4)
                {
                    SurveyContent.SurveyType = SurveyType.SurveyFour;
                    Preferences.Set("Survey", "3");
                    Preferences.Set("Survey4Open", true);
                }
                else
                    Preferences.Set("Survey", null);

                if (Preferences.Get("Survey", null) == "1")
                    await Application.Current.MainPage.Navigation.PushAsync(new Views.Surveys.SecondSurvey.Home());
                else if (Preferences.Get("Survey", null) == "2")
                    await Application.Current.MainPage.Navigation.PushAsync(new Views.Surveys.ThirdSurvey.Home());
                else if (Preferences.Get("Survey", null) == "3" && Preferences.Get("Survey4Open", true))
                    await Application.Current.MainPage.Navigation.PushAsync(new Views.Surveys.FirstSurvey.FourthSurvey.Home());
                else if (Preferences.Get("Survey", null) == "0")
                    await Application.Current.MainPage.Navigation.PushAsync(new Views.Surveys.FirstSurvey.Home());
                else
                    await Application.Current.MainPage.Navigation.PushAsync(new StartSurveyPage());

            }
            
            else if (route == "Einstellungen")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new UserAccountPage());
            }
            else if(route == "Tagebucheintrag verfassen")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new DiaryPage());
            }
            
            else if (route == "FAQ")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new FAQView());
            }
            else if (route == "Addition")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AdditionalInfosPage());
            }
            else if (route == "License")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new LicensePage());
            }
            else if (route == "Settings")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Settings());
            }
            else if (route == "PersonalData")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PersonalDataPage());
            }
            else if (route == "ProfilePicture")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new SelectProfilePicturePage());
            }
            else if (route == "Anleitung")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AdditionalInfosPage());
            }
            return true;
        }

        public static async Task<bool> ShellGoToPrevious()
        {   
              await Application.Current.MainPage.Navigation.PopAsync();
            return true;
        }

       

    }
        public enum Previous
        {
             AboutUs,AdditionalInfos,  DiaryPage, FAQ,  Main, PDF, Picture,   UserAccount, VoiceMail, MainSurveyOne, MainSurveyTwo, MainSurveyThree, Decision, License, Settings, PersonalData, ProfiePicture,
            DiaryOne, DiaryTwo, DiaryThree, DiaryFour, DiaryFive, DiaryEditor  
    }
    
}
