using Desive2.Objects;
using Java.Lang.Reflect;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.SurveyLibraries.SurveyThree
{
    public class SectionTwo
    {
        private static string meta = "Ihr Umgang mit Gesundheitsinformationen";
        private static string additionOnlyOneAnswer = "Bitte wählen Sie nur eine der folgenden Antworten aus:";
        private static string additionAllApplicableAnswers = "Bitte wählen Sie alle zutreffenden Antworten aus:";
        private static string additionAnswerPerQuestion = "Bitte wählen Sie die zutreffende Antwort für jeden Punkt aus:";
        private static string scale = "Von 1 = 'stimme überhaupt nicht zu' bis 5 = 'stimme voll und ganz zu'";
        public static List<Question> Questions = new List<Question> {
            new MatrixQuestion(meta, "Inwieweit treffen die folgenden Aussagen auf Sie zu?", additionAnswerPerQuestion, "Von 1 = 'trifft überhaupt nicht zu' bis 7 = 'trifft voll und ganz zu'", new List<MatrixQuestionAnswers>{

                new MatrixQuestionAnswers("Ich vermeide neue Informationen zu meiner Gesundheit.", new List<string>{
                  "1 - trifft überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7 - trifft voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Auch wenn es mich beunruhigt, möchte ich neue Informationen zu meiner Gesundheit bekommen.", new     List<string>{
                   "1 - trifft überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7 - trifft voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Wenn es um Informationen zu Gesundheit geht, kann Unwissenheit auch ein Segen sein.", new List<string>{
                   "1 - trifft überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7 - trifft voll und ganz zu"
                }),
                 new MatrixQuestionAnswers("Ich kann mir Situationen vorstellen, in denen ich lieber nichts Neues über meine Gesundheit erfahren möchte.", new List<string>{
                   "1 - trifft überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7 - trifft voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Ich möchte neue Informationen zu meiner Gesundheit sofort bekommen.", new List<string>{
                   "1 - trifft überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7 - trifft voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Ich limitiere die Menge an Informationen, die ich über meine Gesundheit beziehe.", new List<string>{
                    "1 - trifft überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7 - trifft voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Ich fühle mich besser, wenn ich weniger neue Informationen zu meiner Gesundheit erfahre.", new   List<string>{
                    "1 - trifft überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7 - trifft voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Mehr über meine Gesundheit zu wissen, hilft mir besser mit der aktuellen Situation umzugehen.", new  List<string>{
                    "1 - trifft überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7 - trifft voll und ganz zu"
                })
            }),
            
            new MatrixQuestion(meta, "In welchem Maße treffen die folgenden Aussagen auf Sie zu?", additionAnswerPerQuestion, scale, new List<MatrixQuestionAnswers>{
                new MatrixQuestionAnswers("Ich weiß, wie ich im Internet nützliche Gesundheitsinformationen finde.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Ich weiß, wie ich das Internet nutzen kann, um Antworten auf meine Fragen rund um das Thema Gesundheit zu bekommen.", new List<string>{
                  "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Ich weiß, welche Quellen für Gesundheitsinformationen im Internet verfügbar sind.", new List<string>{
                   "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Ich weiß, wo ich im Internet nützliche Gesundheitsinformationen finden kann.", new List<string>{
                   "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Ich weiß, wie ich Gesundheitsinformationen aus dem Internet so nutzen kann, dass sie mir weiterhelfen.", new    List<string>{
                  "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Ich bin in der Lage, Gesundheitsinformationen, die ich im Internet finde, kritisch zu bewerten.", new List<string>{
                   "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Ich kann im Internet zuverlässige von fragwürdigen Gesundheitsinformationen unterscheiden.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu"
                }),
                new MatrixQuestionAnswers("Wenn ich gesundheitsbezogene Entscheidungen auf Basis von Gesundheitsinformationen aus dem Internet treffe, fühle ich mich dabei sicher.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu"
                }),
            }),

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

            new SingleAnswerQuestion(meta, additionOnlyOneAnswer, "Wie sicher sind Sie, dass Sie in der Lage sind, Nachrichten oder Informationen zum Thema Gesundheit zu erkennen, die die Realität falsch darstellen oder sogar unwahr sind?", new List<string>() {
                "1 - überhaupt nicht sicher",
                "2",
                "3",
                "4",
                "5 - sehr sicher"
            }, new List<bool>(){false, false, false, false, false}, new List<bool>(){false, false, false, false, false})
        };

    }
}
