﻿using Desive2.Objects;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desive2.Views.Surveys.FirstSurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        bool isOpen = false;
        public Home()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new FirstSurvey.Abschnitt1());
        }

       
        protected override bool OnBackButtonPressed()
        {
            shell();
            return true;
        }

        private void OpenSwipe(object sender, EventArgs e)
        {
            if (!isOpen)
            {
                MainSwipeView.Open(OpenSwipeItem.LeftItems);
                isOpen = true;

            }
            else
            {
                MainSwipeView.Close();
                isOpen = false;

            }
        }
        private async void shell()
        {
            await Navigator.ShellGoToPrevious();
        }
        private async void CloseSwipe(object sender, EventArgs e)
        {
            var menu = (StackLayout)sender;
            MainSwipeView.Close();
            var list = menu.Children.ToList();

            var lbl = (Label)list[1];
            Navigator.PreviousPage.Push(Previous.MainSurveyOne);
            await Navigator.ShellGoTo(lbl.Text);
        }

        private void CloseSwipeGrid(object sender, EventArgs e)
        {
            MainSwipeView.Close();
            isOpen = false;

        }
    }
}