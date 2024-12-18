using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.SurveyLibraries.SurveyThree
{
    public class SectionOne
    {
        private static string meta = "Einschätzung zu Ihren digitalen Fähigkeiten";

        private static string additionAnswerPerQuestion = "Bitte wählen Sie die zutreffende Antwort für jeden Punkt aus:";
        private static string scale = "Von 1 = 'gar nicht' bis 5 = 'in hohem Maße'";
        public static List<Question> Questions = new List<Question> {
             new MatrixQuestion(meta, "Ich kann ...", additionAnswerPerQuestion, scale, new List<MatrixQuestionAnswers>
        {
            new MatrixQuestionAnswers("fortgeschrittene Suchstrategien (z.B. Filterfunktion, Trunkierung) anwenden, um eine Suchanfrage im Internet einzugrenzen.", new List<string>{
                "1 - gar nicht",
                "2",
                "3",
                "4",
                "5 - in hohem Maße",
            }),
            new MatrixQuestionAnswers("die Zuverlässigkeit von Informationen aus dem Internet anhand von Kriterien erklären (z.B. Aktualität, Schreibstil, Referenzen).", new List<string>{
                "1 - gar nicht",
                "2",
                "3",
                "4",
                "5 - in hohem Maße",
            }),
            new MatrixQuestionAnswers("Suchstrategien im Internet an meinen persönlichen Bedarf anpassen (z.B. Auswahl von thematischen Suchmaschinen).", new List<string>{
                "1 - gar nicht",
                "2",
                "3",
                "4",
                "5 - in hohem Maße",
            }),
            new MatrixQuestionAnswers("bei einer speziellen Aufgabe die angemessenste (Bedienungs-)Anleitung (z.B. Videotutorials, Hilfeseiten) für ein Computertool bestimmen.", new List<string>{
                "1 - gar nicht",
                "2",
                "3",
                "4",
                "5 - in hohem Maße",
            }),
            new MatrixQuestionAnswers("Sicherheitseinstellungen meiner digitalen Geräte konfigurieren/ändern.", new List<string>{
                "1 - gar nicht",
                "2",
                "3",
                "4",
                "5 - in hohem Maße",
            }),
            new MatrixQuestionAnswers("die richtige digitale Anwendung für mich und für andere zur Lösung eines Problems auswählen.", new List<string>{
                "1 - gar nicht",
                "2",
                "3",
                "4",
                "5 - in hohem Maße",
            }),
            new MatrixQuestionAnswers("unterscheiden, welche Computertools geeignet sind, um Inhalte gemeinsam zu erstellen und zu verwalten.", new List<string>{
                "1 - gar nicht",
                "2",
                "3",
                "4",
                "5 - in hohem Maße",
            })
        })
        };

    }
}
