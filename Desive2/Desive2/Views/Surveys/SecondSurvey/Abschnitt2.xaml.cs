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
    public partial class Abschnitt2 : ContentPage
    {
        public Abschnitt2()
        {
            InitializeComponent();
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }
        protected override bool OnBackButtonPressed()
        {
            SurveyContent.SurveyCount = SurveyLibraries.SurveyTwo.SectionOne.Questions.Count;
            if (SurveyContent.SkippedPrevoiusQuestion)
            {
                SurveyContent.SurveyCount--;
                SurveyContent.SkippedPrevoiusQuestion = false;
            }
            SurveyContent.SurveySection = SurveySection.SectionOne;
            SurveyContent.GoToPrevious(SurveyContent.SurveyType, SurveyContent.SurveySection);
            return true;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            SurveyContent.SurveySection = SurveySection.SectionTwo;
            SurveyContent.GoToNextQuestion(SurveyContent.SurveyType, SurveyContent.SurveySection, this);
        }
    }
}