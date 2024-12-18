using Desive2.Models;
using Desive2.Objects;
using Desive2.Services;
using Plugin.Connectivity;
using System;
using Desive2.SurveyLibraries;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace Desive2
{
    public partial class App : Application
    {
        bool setCon = true;
        public App()
        {
            string res = String.Empty;
            foreach(Question q in SurveyLibraries.SurveyOne.SectionOne.Questions)
            {
                res += q.ShowQuestion();
            }
            foreach (Question q in SurveyLibraries.SurveyOne.SectionTwo.Questions)
            {
                res += q.ShowQuestion();
            }
            foreach (Question q in SurveyLibraries.SurveyOne.SectionThree.Questions)
            {
                res += q.ShowQuestion();
            }
            foreach (Question q in SurveyLibraries.SurveyTwo.SectionOne.Questions)
            {
                res += q.ShowQuestion();
            }
            foreach (Question q in SurveyLibraries.SurveyTwo.SectionTwo.Questions)
            {
                res += q.ShowQuestion();
            }
            foreach (Question q in SurveyLibraries.SurveyTwo.SectionThree.Questions)
            {
                res += q.ShowQuestion();
            }
            foreach (Question q in SurveyLibraries.SurveyThree.SectionOne.Questions)
            {
                res += q.ShowQuestion();
            }
            foreach (Question q in SurveyLibraries.SurveyThree.SectionTwo.Questions)
            {
                res += q.ShowQuestion();
            }
            InitializeComponent();
            
            MainPage = new AppShell();
 
        }




        protected override void OnStart()
        {
        }            


        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {

           
        }


       
       


   

    }
}
