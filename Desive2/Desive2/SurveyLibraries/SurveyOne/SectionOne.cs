using Desive2.Objects;
using System.Collections.Generic;

namespace Desive2.SurveyLibraries.SurveyOne
{
    //Also used in Survey 4
    public static class SectionOne
    {
        //Section One
        private static string metaText = "Über Ihre Person";
        private static string additionOnlyOneAnswer = "Bitte wählen Sie nur eine der folgenden Antworten aus:";
        private static string additionAllApplicableAnswers = "Bitte wählen Sie alle zutreffenden Antworten aus:";
        public static List<Question> Questions = new List<Question> {
            new SingleAnswerQuestion(metaText, additionOnlyOneAnswer, "Welchem Geschlecht fühlen Sie sich zugehörig?", new List<string>() {
                    "weiblich",
                    "männlich",
                    "nicht-binär",
                    "Ich möchte diese Frage nicht beantworten."
            }, new List <bool>(){false, false, false, false}, new List <bool>(){false, false, false, false}),

            new SingleAnswerQuestion(metaText, additionOnlyOneAnswer,"Wie alt sind Sie?", new List<string>() {
                "19 und jünger",
                "zwischen 20 und 29 Jahre",
                "zwischen 30 und 39 Jahre",
                "zwischen 40 und 49 Jahre",
                "zwischen 50 und 59 Jahre",
                "zwischen 60 und 69 Jahre",
                "70+ Jahre",
            }, new List <bool>(){false, false, false, false, false, false, false}, new List <bool>(){false, false, false, false, false, false, false}),

            new SingleAnswerQuestion(metaText, additionOnlyOneAnswer,"Welcher ist Ihr höchster Bildungsabschluss?", new List<string>() {
                "Hauptschulabschluss",
                "Mittlere Reife (z.B. Realschule)",
                "Abitur (z.B. Gymnasium)",
                "Berufliche Ausbildung",
                "Hochschule (z.B. Bachelor, Master, Diplom, Magister)",
                "Hochschule (Promotion)",
                "Ich habe keinen Bildungsabschluss.",
                "Einen anderen Abschluss, und zwar: "
            }, new List <bool>(){false, false, false, false, false, false, false, true}, new List <bool>(){false, false, false, false, false, false, false, false}),

            new MultipleChoiceQuestion(metaText, additionAllApplicableAnswers,"Was trifft auf Ihre derzeitige berufliche Situation zu?", new List<string>() {
                "Ich bin erwerbstätig.",
                "Ich bin Rentner:in.",
                "Ich bin Schüler:in/Student:in.",
                "Ich befinde mich in einer Ausbildung/Lehre.",
                "Ich bin nicht erwerbstätig.",
                "Sonstiges:"
            }, new List <bool>(){false, false, false, false, false, true}, new List <bool>(){false, false, false, false, false, false}, new List <bool>(){false, false, false, false, false, false}),

            new SingleAnswerQuestion(metaText, additionOnlyOneAnswer, "Ich wohne …", new List<string>() {
                "auf dem Land (bis 20.000 Einwohner).",
                "in einer kleinen Stadt (bis 100.000 Einwohner).",
                "in einer mittelgroßen Stadt (bis 500.000 Einwohner).",
                "in einer Großstadt (ab 500.000 Einwohner).",
                "Sonstiges:"
            }, new List <bool>(){false, false, false, false, true}, new List <bool>(){false, false, false, false, false}),

        };
    }
}
