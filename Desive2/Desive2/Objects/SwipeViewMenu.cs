using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.Objects
{
    public class SwipeViewMenu
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        public static List<SwipeViewMenu> GetMenus()
        {
            return new List<SwipeViewMenu>
            {
                 new SwipeViewMenu{ Name = "Startseite", Icon = "home.png"},
               
                new SwipeViewMenu{ Name = "Tagebucheintrag verfassen", Icon = "edit.png"},
                
                new SwipeViewMenu{ Name = "Umfrage beantworten", Icon = "tasks.png"},
                new SwipeViewMenu{ Name = "Anleitung", Icon = "faq.png"},

                new SwipeViewMenu{ Name = "Über uns", Icon = "aboutUs.png"},

            };
        }
    }
}
