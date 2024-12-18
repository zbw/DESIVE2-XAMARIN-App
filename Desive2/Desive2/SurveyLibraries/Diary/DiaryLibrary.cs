using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.SurveyLibraries.Diary
{
    public static class DiaryLibrary
    {
        private static string metaText = "Zusätzliche Informationen";
        private static string addition1 = "Bitte wählen Sie nur eine der folgenden Antworten aus:";
        private static string addition2 = "Bitte wählen Sie alle zutreffenden Antworten aus:";
        public static List<Question> Questions = new List<Question> {
        new SingleAnswerQuestion(metaText, addition1, "Was beschreibt die Situation, die Sie mit uns geteilt haben? Es geht bei dieser Frage darum, ob in der Situation die Information an Sie weitergegeben wurde oder ob Sie diese weitergegeben haben.", new List<string>() {
                "Die Gesundheitsinformationen wurden mit mir per Messenger (z.B. Whatsapp, Facebook Messenger, Signal, SMS) geteilt.",
                "Die Gesundheitsinformationen habe ich auf Social Media gesehen (z.B. Instagram, Facebook, Twitter).",
                "Ich habe die Gesundheitsinformationen auf anderem Weg erhalten.",
                "Ich habe die Gesundheitsinformationen mit anderen Menschen geteilt.",
                "Keine dieser Optionen trifft auf meine hochgeladene Information zu."
            }, new List < bool > { false, false, false, false, false }),
            new MultipleChoiceQuestion(metaText, addition2,"Wer hat die Gesundheitsinformationen, die Sie hochgeladen haben, mit Ihnen geteilt?", new List<string>() {
                "Eine Person, die ich sehr gut kenne (z.B. Familie, Freunde).",
                "Eine Person, die sich mit dem Thema Gesundheit sehr gut auskennt.",
                "Jemand innerhalb einer Gruppe, in der mehrere Menschen, die ich kenne, zu einem Thema zusammenkommen (z.B. Kita-Gruppe, Sportgruppe, Mitarbeitergruppe).",
                "Jemand innerhalb einer Gruppe, in der mehrere Menschen, die ich nicht kenne, zu einem Thema zusammenkommen (z.B. Bitcoin-Gruppe, Computerspielgruppe).",
                "Ein Nachrichtendienst, den ich zu einem bestimmten Thema abonniert habe.",
                "Sonstiges: ",
            }, new List < bool >() { false, false, false, false, false, true}, new List < bool >() { false, false, false, false, false, false}, new List < bool >() { false, false, false, false, false, false}),
            new SingleAnswerQuestion(metaText, addition1,"Wie schätzen Sie die Gesundheitsinformationen ein, die Ihre hochgeladene Information zeigt?", new List<string>() {
                "Ich halte die Gesundheitsinformationen für richtig.",
                "Ich halte die Gesundheitsinformationen für falsch.",
                "Ich weiß es nicht.",
                "Andere Einschätzung:",
            }, new List<bool>(){false, false, false, true }, new List<bool>(){false, false, false, false }),
            new MultipleChoiceQuestion(metaText, addition2, "Mit wem haben Sie Ihre hochgeladene Gesundheitsinformationen geteilt?", new List<string>() {
                "Mit einer Person, die ich sehr gut kenne (z.B. Familie, Freunde).",
                "Mit einer Person, die sich mit dem Thema Gesundheit sehr gut auskennt.",
                "Mit einer Gruppe, in der mehrere Menschen, die ich kenne, zu einem Thema zusammenkommen (z.B. Kita-Gruppe, Sportgruppe, Arbeitsgruppe).",
                "Mit einer Gruppe, in der mehrere Menschen, die ich nicht kenne, zu einem Thema zusammenkommen (z.B. Bitcoin-Gruppe, Computerspielgruppe)",
                "Ich habe die Information in Sozialen Medien geteilt (z.B. Instagram, Facebook, Twitter).",
                "Sonstiges: "
            }, new List < bool >() { false, false, false, false, false, true }, new List < bool >() { false, false, false, false, false, false } , new List < bool >() { false, false, false, false, false, false }),
            new MultpleChoiceWithEditor(metaText, addition2, "Was haben Sie mit den Gesundheitsinformationen, die Sie gerade hochgeladen haben, gemacht, nachdem Sie sie gesehen / erhalten haben?",
                new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("Ich habe die Gesundheitsinformationen mit anderen Menschen geteilt.", "Mit wem und aus welchem Grund?"),
                    new Tuple<string, string>("Ich habe die Gesundheitsinformationen überprüft oder recherchiert.", "Wo und aus welchem Grund?"),
                    new Tuple<string, string>("Ich habe nicht auf die Gesundheitsinformationen reagiert.", "Aus welchem Grund?")
                }),
        };
    }
}
