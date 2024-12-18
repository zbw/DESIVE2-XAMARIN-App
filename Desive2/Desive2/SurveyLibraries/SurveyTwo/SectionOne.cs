using Desive2.Objects;
using Desive2.Views.DiaryQuestions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.SurveyLibraries.SurveyTwo
{
    class SectionOne
    {
        private static string meta = "Ihr Umgang mit Information";
        private static string additionOnlyOneAnswer = "Bitte wählen Sie nur eine der folgenden Antworten aus:";
        private static string additionAllApplicableAnswers = "Bitte wählen Sie alle zutreffenden Antworten aus:";
        private static string additionAnswerPerQuestion = "Bitte wählen Sie die zutreffende Antwort für jeden Punkt aus:";
        private static string scale = "Von 1 = 'stimme überhaupt nicht zu' bis 5 = 'stimme voll und ganz zu'";
        public static List<Question> Questions = new List<Question> {
            
            new SingleAnswerQuestion(meta, additionOnlyOneAnswer, "Wie sicher sind Sie, dass Sie in der Lage sind, Nachrichten oder Informationen zum Thema Gesundheit zu erkennen, die die Realität falsch darstellen oder sogar unwahr sind?", new List<string>() {
                "1 - überhaupt nicht sicher",
                "2",
                "3",
                "4",
                "5 - sehr sicher",
                "weiß nicht / keine Angabe"
            }, new List<bool>(){false, false, false, false, false, false }, new List<bool>(){false, false, false, false, false, false}),

            new SingleAnswerQuestion(meta, additionOnlyOneAnswer, "Wie häufig stoßen Sie auf Nachrichten oder Informationen zum Thema Gesundheit, von denen Sie glauben, dass sie die Realität falsch darstellen oder sogar unwahr sind?", new List<string>() {
                "täglich",
                "mehrmals die Woche",
                "1-2 Mal pro Woche",
                "einmal im Monat",
                "seltener als einmal im Monat",
                "nie",
                "weiß nicht / keine Angabe"
            }, new List<bool>(){false, false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, true, false}),

            new MultipleChoiceQuestion(meta, additionAllApplicableAnswers, "Über welche Themen / Personen haben Sie in den letzten Wochen falsche oder irreführende Informationen gesehen?", new List<string>() {
                "Politik / Politiker:innen",
                "Prominente (z.B. Schauspieler:innen, Musiker:innen, Sportler:innen)",
                "Coronavirus (COVID-19)",
                "Andere Gesundheitsthemen",
                "Immigration",
                "Produkte und Dienstleistungen",
                "Klimakrise und Umwelt",
                "Sonstiges Thema:"
            }, new List<bool>(){false, false, false, false, false, false, false, true}, new List<bool>(){false, false, false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, false, false, false}),
            
            new MultipleChoiceQuestion(meta,additionAllApplicableAnswers, "Was macht eine Gesundheitsinformation aus Ihrer Sicht glaubwürdig? Eine Gesundheitsinformation ist glaubwürdig, wenn ...", new List<string>() {
                "sie von der Mehrheit der Wissenschaft als richtig anerkannt wird.",
                "sie in Nachrichtensendungen öffentlich-rechtlicher Medien verbreitet wird.",
                "viele Menschen sie in sozialen Netzwerken teilen.",
                "meine Freund:innen / Familie sie für glaubwürdig halten.",
                "sie etwas beinhaltet, was für mich neu und überraschend ist.",
                "jemand einzelnes mutig genug ist, sich damit gegen die Mehrheitsmeinung zu stellen.",
                "sie von staatlichen Stellen oder Institutionen verbreitet wird.",
                "bekannte Persönlichkeiten sie verbreiten.",
                "es davon Foto- oder Videoaufnahmen gibt.",
                "sie mir plausibel erscheint."
            }, new List<bool>(){false, false, false, false, false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, false, false, false, false, false }, new List<bool>(){false, false, false, false, false, false, false, false, false, false}),
            
            new MatrixQuestion(meta, "Inwiefern stimmen Sie mit folgenden Aussagen überein?", additionAnswerPerQuestion, scale, new List<MatrixQuestionAnswers> {
                new MatrixQuestionAnswers("Falschinformationen können die öffentliche Meinung manipulieren.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Falschinformationen stellen eine Gefahr für die Demokratie dar.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Falschinformationen sind keine Bedrohung.", new List<string>{
                   "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Falschinformation können effektive Maßnahmen in der Gesundheitsversorgung der Bevölkerung verhindern oder blockieren.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Falschinformationen stellen eine Gefahr für die persönliche Gesundheit dar.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Die Meinungsfreiheit lässt auch die Verbreitung von Falschinformationen zu.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu",
                    "weiß nicht / keine Angabe"
                }),
            })
        };
    }
}
