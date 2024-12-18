using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.SurveyLibraries.SurveyOne
{
    public class SectionTwo
    {
        private static string metaText = "Ihre Interneterfahrung und -nutzung";
        private static string additionDefinitionOfInternet = "*Mit 'Internet' sind auch Soziale Medien wie Facebook gemeint, Apps auf dem Smartphone, das Suchen über Google oder auch die Nutzung eines Browsers wie Google Chrome oder Firefox.\n\nBitte wählen Sie nur eine der folgenden Antworten aus:";
        private static string additionOnlyOneAnswer = "Bitte wählen Sie nur eine der folgenden Antworten aus:";
        private static string additionAllApplicableAnswers = "Bitte wählen Sie alle zutreffenden Antworten aus:";
        public static List<Question> Questions = new List<Question> {
           new SingleAnswerQuestion(metaText, additionDefinitionOfInternet, "Wie häufig sind Sie im Internet?*", new List<string>() {
                "täglich",
                "mehrmals die Woche",
                "1-2 Mal pro Woche",
                "einmal im Monat",
                "seltener als einmal im Monat",
                "weiß nicht / keine Angabe"
            }, new List<bool>(){false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, false}),

            new MultipleChoiceQuestion(metaText, additionAllApplicableAnswers,"Welche Informationsquellen nutzen Sie, um sich über das aktuelle Tagesgeschehen zu informieren?", new List<string>() {
                "Zeitung / Zeitschrift (online oder gedruckt)",
                "öffentlich-rechtliches Radio / Fernsehen",
                "privates Radio / Fernsehen",
                "Apps und Webseiten von Nachrichtenkanälen",
                "Soziale Medien (z.B. Facebook, Instagram, Twitter)",
                "Ich informiere mich nicht über das aktuelle Tagesgeschehen.",
                "Sonstiges:",
            }, new List<bool>(){false, false, false, false, false, false, true}, new List<bool>(){false, false, false, false, false, true, false}, new List<bool>(){false, false, false, false, false, false, false}),

            new MultipleChoiceQuestion(metaText, additionAllApplicableAnswers,"Welche der folgenden Sozialen Medien nutzen Sie?", new List<string>() {
                "Tiktok",
                "Facebook",
                "Instagram",
                "Twitter",
                "Pinterest",
                "Snapchat",
                "Telegram",
                "Keine der genannten Optionen."
            }, new List<bool>(){false, false, false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, false, false, true}, new List<bool>(){false, false, false, false, false, false, false, false}),
            
            new SingleAnswerQuestion(metaText, additionOnlyOneAnswer,"Welche Aussage beschreibt Ihre Nutzung von Sozialen Medien am besten?", new List<string>() {
                "Ich bin sehr aktiv: Ich lese Beiträge, schaue mir Videos und Bilder an, erstelle selbst Beiträge (auch in Gruppen mit Menschen, die ich nicht kenne), diskutiere und tausche mich mit anderen aus.",
                "Ich bin eher aktiv: Ich lese Beiträge, schaue mir Videos und Bilder an, erstelle selbst Beiträge und like manchmal Beiträge anderer.",
                "Ich bin eher passiv: Ich lese Beiträge (z.B. Posts, Artikel, Meinungen), schaue mir Videos und Bilder an und like manchmal Beiträge.",
                "Ich bin nicht aktiv, da ich keine Sozialen Medien nutze / kenne.",
            }, new List<bool>(){false, false, false, false}, new List<bool>(){false, false, false, false}),

            new MultipleChoiceQuestion(metaText, additionAllApplicableAnswers, "Welche der folgenden Messenger-Dienste nutzen Sie?", new List<string>() {
                "Whatsapp",
                "Signal",
                "Facebook Messenger",
                "Threema",
                "Telegram",
                "Keine der genannten Optionen."
            }, new List<bool>(){false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, true}, new List<bool>(){false, false, false, false, false, false}),

            new SingleAnswerQuestion(metaText, additionOnlyOneAnswer,"Welche Aussage beschreibt Ihre Nutzung von Messenger-Diensten am besten?", new List<string>() {
                "Ich bin sehr aktiv: Ich kommuniziere mit Freund:innen, Familie, Bekannten, aber auch mit mir unbekannten Personen, erstelle Gruppen und nehme sehr aktiv an diesen teil, diskutiere und tausche mich mit anderen aus.",
                "Ich bin eher aktiv: Ich kommuniziere mit Freund:innen, Familie, Bekannten, bin in Gruppen eher aktiv, aber fange keine Diskussionen an.",
                "Ich bin eher passiv: Ich kommuniziere mit Freund:innen, Familie, Bekannten in geringem Maße und lese eher Beiträge und beteilige mich selten.",
                "Ich bin nicht aktiv, da ich keine Messenger-Dienste nutze / kenne.",
            }, new List<bool>(){false, false, false, false}, new List<bool>(){false, false, false, false}),

            new SingleAnswerQuestion(metaText, additionOnlyOneAnswer, "Wie oft nutzen Sie Soziale Medien und Messenger-Dienste (z.B. WhatsApp, Telegram oder Signal)?", new List<string>() {
                "täglich",
                "mehrmals die Woche",
                "1-2 Mal pro Woche",
                "einmal im Monat",
                "seltener als einmal im Monat",
                "nie",
                "weiß nicht / keine Angabe"
            }, new List<bool>(){false, false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, false, false}),

            new MultipleChoiceQuestion(metaText, additionAllApplicableAnswers,"Welche der folgenden Geräte nutzen Sie privat (oder beruflich)?", new List<string>() {
                "Fest installierter Computer",
                "Laptop / Notebook",
                "Tablet",
                "Smartphone",
                "Einfaches Handy / Mobiltelefon",
                "Smartwatch, Wearable, Smart Tracker",
                "Ich nutze keines dieser Geräte."
            }, new List<bool>(){false, false, false, false, false, false, false}, new List<bool>(){false, false, false, false, false, false, true}, new List<bool>(){false, false, false, false, false, false, false}),

        };

    }

}
