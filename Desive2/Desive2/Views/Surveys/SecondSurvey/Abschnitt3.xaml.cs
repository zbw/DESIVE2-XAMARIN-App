using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desive2.Views.Surveys.SecondSurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Abschnitt3 : ContentPage
    {
        private bool skippedPreviousQuestion = false;
        public Abschnitt3()
        {
            if (SurveyContent.SkippedPrevoiusQuestion)
            {
                SurveyContent.SkippedPrevoiusQuestion = false;
                skippedPreviousQuestion = true;
            }
            InitializeComponent();
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }
        protected override bool OnBackButtonPressed()
        {
            SurveyContent.SurveyCount = SurveyLibraries.SurveyTwo.SectionTwo.Questions.Count;
            if (skippedPreviousQuestion)
                SurveyContent.SurveyCount--;
            SurveyContent.SurveySection = SurveySection.SectionTwo;
            SurveyContent.GoToPrevious(SurveyContent.SurveyType, SurveyContent.SurveySection);
            return true;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            SurveyContent.SurveySection = SurveySection.SectionThree;
            SurveyContent.GoToNextQuestion(SurveyContent.SurveyType, SurveyContent.SurveySection, this);
        }
    }
}