using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desive2.Views.Surveys.ThirdSurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Abschnitt1 : ContentPage
    {
        public Abschnitt1()
        {
            InitializeComponent();
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }
        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage.Navigation.PopAsync();
            return true;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            SurveyContent.SurveyCount = 0;
            SurveyContent.SurveySection = SurveySection.SectionOne;
            SurveyContent.GoToNextQuestion(SurveyType.SurveyThree, SurveySection.SectionOne, this);
        }
    }
}