using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.SurveyLibraries.SurveyTwo
{
    public class SectionTwo
    {
        private static string meta = "Ihr Umgang mit Gesundheitsinformationen";
        private static string additionOnlyOneAnswer = "Bitte wählen Sie nur eine der folgenden Antworten aus:";
        private static string additionAllApplicableAnswers = "Bitte wählen Sie alle zutreffenden Antworten aus:";
        private static string additionAnswerPerQuestion = "Bitte wählen Sie die zutreffende Antwort für jeden Punkt aus:";
        private static string scale = "Von 1 = 'gar kein Interesse' bis 5 = 'sehr großes Interesse'";
        public static List<Question> Questions = new List<Question> {
            
            new MultipleChoiceQuestion(meta, additionAllApplicableAnswers, "Wie eignen Sie sich Wissen über Gesundheitsthemen an?", new List<string>() {
                "Ich suche darüber / dazu in Suchmaschinen.",
                "Ich frage Freund:innen / Familie / Bekannte oder Kolleg:innen.",
                "Ich frage meinen Arzt / meine Ärztin.",
                "Ich lese dazu in der Presse (z.B. Zeitschriften, Magazine, Tageszeitungen).",
                "Ich folge Gesundheitsthemen in Sozialen Medien.",
                "Ich eigne mir kein neues Wissen über Gesundheitsthemen an.",
                "Sonstiges:"
            }, new List<bool>(){false, false, false, false, false, false, true}, new List<bool>(){false, false, false, false, false, true, false} , new List<bool>(){false, false, false, false, false, false, false}), 
            
            new MatrixQuestion(meta, "Wie groß ist Ihr Interesse an folgenden Gesundheitsthemen im Allgemeinen?", additionAnswerPerQuestion, scale, new List<MatrixQuestionAnswers>{
                new MatrixQuestionAnswers("Krankheiten", new List<string>{
                    "1 - gar kein Interesse",
                    "2",
                    "3",
                    "4",
                    "5 - sehr großes Interesse",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Therapiemöglichkeiten", new List<string>{
                    "1 - gar kein Interesse",
                    "2",
                    "3",
                    "4",
                    "5 - sehr großes Interesse",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Aktuelle, in den Nachrichten erwähnte Themen, die die Gesundheit betreffen (z.B. COVID-19, Affenpocken etc.)", new List<string>{
                    "1 - gar kein Interesse",
                    "2",
                    "3",
                    "4",
                    "5 - sehr großes Interesse",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Tipps und Ratschläge für eine gesunde Lebensweise", new List<string>{
                    "1 - gar kein Interesse",
                    "2",
                    "3",
                    "4",
                    "5 - sehr großes Interesse",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Sportliche Aktivitäten, die die Gesundheit betreffen", new List<string>{
                    "1 - gar kein Interesse",
                    "2",
                    "3",
                    "4",
                    "5 - sehr großes Interesse",
                    "weiß nicht / keine Angabe"
                })
            }),

            new SingleAnswerQuestion(meta, additionOnlyOneAnswer, "Sind Sie besser oder schlechter über Gesundheitsthemen informiert als die meisten Menschen in Ihrem persönlichen Umfeld (z.B. Familie, Freundeskreis, Bekannte), was schätzen Sie?", new List<string>() {
                "besser informiert",
                "gleich gut informiert",
                "weniger gut informiert"
            }, new List<bool>(){false, false, false}, new List<bool>(){false, false, false}),

            new SingleAnswerQuestion(meta, additionOnlyOneAnswer, "Wenn Sie an den letzten Monat zurückdenken, wie oft haben Sie sich über Gesundheitsthemen informiert?", new List<string>() {
                "täglich",
                "mehrmals die Woche",
                "1-2 Mal pro Woche",
                "einmal im Monat",
                "überhaupt nicht"
            }, new List<bool>(){false, false, false, false, false}, new List<bool>(){false, false, false, false, true}),


            new MultipleChoiceQuestion(meta, additionAllApplicableAnswers, "Wo haben Sie sich im letzten Monat über Gesundheitsthemen informiert?", new List<string>() {
                "auf der Arbeit / im Job",
                "bei meinen Arztbesuchen",
                "im privaten Umfeld",
                "bei eigenen Recherchen im Internet",
                "bei eigenen Recherchen, z.B. in Bibliotheken, bei Ämtern etc.",
                "in den Nachrichten",
                "in der Werbung",
                "Sonstiges:"
            }, new List <bool>(){false, false, false, false, false, false, false, true }, new List <bool>(){false, false, false, false, false, false, false, false }, new List <bool>(){false, false, false, false, false, false, false, false })
        };
    }
}
