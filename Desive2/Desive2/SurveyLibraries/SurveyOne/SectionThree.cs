using Desive2.Objects;
using System.Collections.Generic;

namespace Desive2.SurveyLibraries.SurveyOne
{
    public class SectionThree
    {
        private static string meta = "Ihre Erfahrung mit Falschinformation";
        private static string additionOnlyOneAnswer = "Bitte wählen Sie nur eine der folgenden Antworten aus:";
        private static string additionAllApplicableAnswers = "Bitte wählen Sie alle zutreffenden Antworten aus:";
        private static string additionAnswerPerQuestion = "Bitte wählen Sie die zutreffende Antwort für jeden Punkt aus:";
        private static string scale = "Von 1 = 'gar kein Interesse' bis 5 = 'sehr großes Interesse'";
        public static List<Question> Questions = new List<Question> {
            new MatrixQuestion(meta, "Wie groß ist Ihr Interesse an folgenden Themen?", additionAnswerPerQuestion, scale, new List<MatrixQuestionAnswers> {
                new MatrixQuestionAnswers("Politik", new List<string>{
                    "1 - gar kein Interesse",
                    "2",
                    "3",
                    "4",
                    "5 - sehr großes Interesse",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Wissenschaft und Forschung", new List<string>{
                    "1 - gar kein Interesse",
                    "2",
                    "3",
                    "4",
                    "5 - sehr großes Interesse",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Lokale Informationen aus meiner Umgebung", new List<string>{
                    "1 - gar kein Interesse",
                    "2",
                    "3",
                    "4",
                    "5 - sehr großes Interesse",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Wirtschaft und Finanzen", new List<string>{
                    "1 - gar kein Interesse",
                    "2",
                    "3",
                    "4",
                    "5 - sehr großes Interesse",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Gesundheit", new List<string>{
                    "1 - gar kein Interesse",
                    "2",
                    "3",
                    "4",
                    "5 - sehr großes Interesse",
                    "weiß nicht / keine Angabe"
                }),
            }),

            new MultipleChoiceQuestion(meta, additionAllApplicableAnswers, "Markieren Sie alle Aussagen, die auf Sie zutreffen.", new List<string>() {
                "Ich habe in den Medien schon einmal Falschinformation zum Thema Gesundheit gelesen.",
                "Ich bin schon im Internet auf Falschinformation zum Thema Gesundheit gestoßen.",
                "Ich bin schon in den sozialen Medien auf Falschinformation zu Gesundheitsthemen gestoßen.",
                "Mir wurde schon Falschinformation zum Thema Gesundheit von Freund:innen und Bekannten geschickt.",
                "Ich hatte noch keinen Kontakt mit Falschinformation zum Thema Gesundheit.",
                "Ich habe aus anderen Quellen Falschinformationen bekommen, zum Beispiel: ",
                "Keine der Aussagen trifft auf mich zu."
            }, new List<bool>(){false, false, false, false, false, true, false}, new List<bool>(){false, false, false, false, false, false, true}, new List<bool>(){false, false, false, false, false, false, false}),

            new SingleAnswerQuestion(meta, additionOnlyOneAnswer, "Wie sicher sind Sie, dass Sie in der Lage sind, Nachrichten oder Informationen zum Thema Gesundheit zu erkennen, die die Realität falsch darstellen oder sogar unwahr sind? ", new List<string>() {
                "1 - überhaupt nicht sicher",
                "2",
                "3",
                "4",
                "5 - sehr sicher",
                "weiß nicht / keine Angabe"
            }, new List<bool>(){false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, false}),

            new SingleAnswerQuestion(meta, additionOnlyOneAnswer, "Wie häufig stoßen Sie auf Nachrichten oder Informationen zum Thema Gesundheit, von denen Sie glauben, dass sie die Realität falsch darstellen oder sogar unwahr sind? ", new List<string>() {
                "täglich",
                "mehrmals die Woche",
                "1-2 Mal pro Woche",
                "einmal im Monat",
                "seltener als einmal im Monat",
                "nie",
                "weiß nicht / keine Angabe"
            }, new List<bool>(){false, false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, true, false}),

            new MultipleChoiceQuestion(meta, additionAllApplicableAnswers, "Über welche Themen haben Sie in den letzten Wochen falsche oder irreführende Informationen gesehen?", new List<string>() {
                "Politik / Politiker:innen",
                "Prominente (z.B. Schauspieler:innen, Musiker:innen, Sportler:innen)",
                "Coronavirus (COVID-19)",
                "Andere Gesundheitsthemen",
                "Immigration",
                "Produkte und Dienstleistungen",
                "Klimakrise und Umwelt",
                "Sonstiges Thema:"
            }, new List<bool>(){false, false, false, false, false, false, false, true}, new List<bool>(){false, false, false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, false, false, false}),
        };



    }
}
