using Desive2.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Desive2.SurveyLibraries.SurveyTwo
{
    public class SectionThree
    {
        private static string meta = "Ihre Einstellung zur Wissenschaft";
        private static string additionOnlyOneAnswer = "Bitte wählen Sie nur eine der folgenden Antworten aus:";
        private static string additionAllApplicableAnswers = "Bitte wählen Sie alle zutreffenden Antworten aus:";
        private static string additionAnswerPerQuestion = "Bitte wählen Sie die zutreffende Antwort für jeden Punkt aus:";
        private static string scale = "Von 1 = 'stimme überhaupt nicht zu' bis 5 = 'stimme voll und ganz zu'";
        public static List<Question> Questions = new List<Question> {
            
            new SingleAnswerQuestion(meta, additionAnswerPerQuestion, "Wie sehr vertrauen Sie Wissenschaft und Forschung?", new List<string>() {
                "1 - vertraue überhaupt nicht",
                "2",
                "3",
                "4",
                "5 - vertraue voll und ganz",
                "weiß nicht / keine Angabe"
            }, new List <bool>(){false, false, false, false, false, false}),
             
            new MatrixQuestion(meta, "Hier ist eine Reihe von häufig gehörten Meinungen über die Wissenschaft. Geben Sie bitte an, ob Sie diesen Meinungen zustimmen oder nicht.", additionAnswerPerQuestion, scale, new List<MatrixQuestionAnswers>{
                
                new MatrixQuestionAnswers("Wissenschaftler:innen ignorieren Ergebnisse, die ihren Ansichten widersprechen.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu",
                    "weiß nicht / keine Angabe"
                }),

                new MatrixQuestionAnswers("Wir sollten darauf vertrauen, dass Wissenschaftler:innen ihre Arbeit ehrlich machen.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu",
                    "weiß nicht / keine Angabe"
                }),

                new MatrixQuestionAnswers("Ich glaube, dass Wissenschaftler:innen Lösungen für unsere größten Probleme finden können.", new     List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu",
                    "weiß nicht / keine Angabe"
                }),
                
                new MatrixQuestionAnswers("Wissenschaftler:innen halten ihre Ergebnisse bewusst geheim.", new List<string>{
                    "1 - stimme überhaupt nicht zu",
                    "2",
                    "3",
                    "4",
                    "5 - stimme voll und ganz zu",
                    "weiß nicht / keine Angabe"
                })
            }),
            
            new MatrixQuestion(meta, "Wie oft...", additionAnswerPerQuestion, "", new List<MatrixQuestionAnswers>{
                
                new MatrixQuestionAnswers("sprechen Sie mit Freund:innen oder der Familie über Wissenschaft und Forschung?", new List<string>{
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),

                new MatrixQuestionAnswers("gehen Sie zu Veranstaltungen, Vorträgen oder Diskussionen über Wissenschaft und Forschung?", new     List<string>{
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),

                new MatrixQuestionAnswers("lesen Sie Artikel zu wissenschaftlichen Themen in gedruckten Zeitungen oder Magazinen?", new List<string>    {
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),

                new MatrixQuestionAnswers("sehen Sie sich Fernsehsendungen über Wissenschaft und Forschung im regulären Fernsehprogramm an?", new List<string>{
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),

                new MatrixQuestionAnswers("kommt es vor, dass Sie im Radio Neuigkeiten oder Berichte über Wissenschaft und Forschung hören?", new List<string>{
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),

                new MatrixQuestionAnswers("informieren Sie sich im Internet über Wissenschaft und Forschung?", new List<string>{
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                })
            }),
            
            new MatrixQuestion(meta, "Und wenn Sie sich im Internet über Wissenschaft und Forschung informieren, wie oft tun Sie dies über folgende Wege?", additionAnswerPerQuestion, "", new List<MatrixQuestionAnswers>{
                new MatrixQuestionAnswers("Über Facebook, Twitter oder andere Soziale Medien", new List<string>{
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Über Blogs oder Online-Foren", new List<string>{
                  "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Über Wikipedia", new List<string>{
                   "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Über Youtube oder ähnliche Videoplattformen", new List<string>{
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Über Webauftritte von wissenschaftlichen Einrichtungen oder Organisationen", new List<string>{
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Über Websites oder Mediatheken von Nachrichtenmedien wie Zeitungen, Magazine oder Fernsehsender", new List<string>{
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                }),
                new MatrixQuestionAnswers("Über Podcasts", new List<string>{
                    "nie",
                    "selten",
                    "gelegentlich",
                    "häufig",
                    "sehr häufig",
                    "weiß nicht / keine Angabe"
                })
        })
        };
    }
}
