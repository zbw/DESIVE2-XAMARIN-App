using Desive2.Models;
using Desive2.ViewModels;
using Desive2.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Desive2.Views.DiaryQuestions;
using System;
using System.Linq;

namespace Desive2.Objects
{
    public static class SurveyContent
    {
        /// <summary>
        /// Static variable to track the count of surveys.
        /// </summary>
        public static int SurveyCount = 0;

        /// <summary>
        /// Static variable to track the count of diaries.
        /// </summary>
        public static int DiaryCount = 0;

        /// <summary>
        /// Static variable to track the current diary path.
        /// </summary>
        public static DiaryPath DiaryPath = DiaryPath.PathOne;

        /// <summary>
        /// Static variable to track the current survey type.
        /// </summary>
        public static SurveyType SurveyType;

        /// <summary>
        /// Static variable to track the current survey section.
        /// </summary>
        public static SurveySection SurveySection;

        /// <summary>
        /// Static flag indicating whether the previous question was skipped.
        /// </summary>
        public static bool SkippedPrevoiusQuestion = false;

        /// <summary>
        /// Dictionary holding editor responses.
        /// </summary>
        public static Dictionary<string, string> Editor = new Dictionary<string, string>();

        /// <summary>
        /// Dictionary holding single-choice answers.
        /// </summary>
        public static Dictionary<string, string> SingleChoice = new Dictionary<string, string>();

        /// <summary>
        /// Dictionary holding multiple-choice answers.
        /// </summary>
        public static Dictionary<string, List<string>> MultipleChoice = new Dictionary<string, List<string>>();

        /// <summary>
        /// Dictionary holding matrix question answers.
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> Matrix = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Dictionary holding multiple-choice answers with editor.
        /// </summary>
        public static Dictionary<string, List<Dictionary<string, string>>> MultipleChoiceWithEditor = new Dictionary<string, List<Dictionary<string, string>>>();

        /// <summary>
        /// Converts the diary content into a JSON string.
        /// </summary>
        /// <returns>A JSON string representing the diary content.</returns>
        public static string GetDiaryJson()
        {
            string sc = JsonConvert.SerializeObject(Editor); // Serialize the editor responses to JSON
            string surveyJson = GetSurveyJson(); // Serialize the survey responses to JSON

            JObject result = new JObject();
            JObject o1 = JObject.Parse(sc); // Parse the editor responses JSON
            JObject o2 = JObject.Parse(surveyJson); // Parse the survey responses JSON
            result["0"] = o1; // Add editor responses to the result
            result["1"] = o2; // Add survey responses to the result
            return result.ToString(); // Return the final merged JSON string
        }

        /// <summary>
        /// Converts the survey content into a JSON string.
        /// </summary>
        /// <returns>A JSON string representing the survey content.</returns>
        public static string GetSurveyJson()
        {
            string sc = JsonConvert.SerializeObject(SingleChoice); // Serialize the single-choice responses to JSON
            string mc = JsonConvert.SerializeObject(MultipleChoice); // Serialize the multiple-choice responses to JSON
            string matrix = JsonConvert.SerializeObject(Matrix); // Serialize the matrix responses to JSON
            string mcwe = JsonConvert.SerializeObject(MultipleChoiceWithEditor); // Serialize the multiple-choice with editor responses to JSON

            JObject o1 = JObject.Parse(sc); // Parse the single-choice responses JSON
            JObject o2 = JObject.Parse(mc); // Parse the multiple-choice responses JSON
            JObject o3 = JObject.Parse(matrix); // Parse the matrix responses JSON
            JObject o4 = JObject.Parse(mcwe); // Parse the multiple-choice with editor responses JSON
            o1.Merge(o2); // Merge the multiple-choice responses into the result
            o1.Merge(o3); // Merge the matrix responses into the result
            o1.Merge(o4); // Merge the multiple-choice with editor responses into the result
            return o1.ToString(); // Return the final merged JSON string
        }

        /// <summary>
        /// Resets all survey and diary data to initial values.
        /// </summary>
        public static void Reset()
        {
            // Reset all survey content dictionaries to empty
            SingleChoice = new Dictionary<string, string>();
            MultipleChoice = new Dictionary<string, List<string>>();
            Matrix = new Dictionary<string, Dictionary<string, string>>();
            MultipleChoiceWithEditor = new Dictionary<string, List<Dictionary<string, string>>>();
            Editor = new Dictionary<string, string>();

            // Reset survey and diary count and path
            SurveyCount = -1;
            DiaryCount = 0;
            DiaryPath = DiaryPath.PathOne;
            SurveySection = SurveySection.SectionOne;

            // Navigate back to the root page of the app
            Application.Current.MainPage.Navigation.PopToRootAsync(false);
        }


        /// <summary>
        /// Navigates to the previous page in the navigation stack, and decrements the SurveyCount if applicable.
        /// </summary>
        /// <param name="type">The type of the survey to go back to.</param>
        /// <param name="section">The section of the survey to go back to.</param>
        public static async void GoToPrevious(SurveyType type, SurveySection section)
        {
            // Decrease the SurveyCount if it's greater than 0
            if (SurveyCount > 0)
            {
                SurveyCount--; // Decrement the SurveyCount to track the previous survey page
            }

            // Navigate back to the previous page in the stack asynchronously
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public static async void GoToNextQuestion(SurveyType type, SurveySection section, Page page)
        {
            if(!(page is Views.Surveys.FirstSurvey.Abschnitt1 || page is Views.Surveys.FirstSurvey.Abschnitt2 || page is Views.Surveys.FirstSurvey.Abschnitt3 || page is Views.Surveys.SecondSurvey.Abschnitt1 || page is Views.Surveys.SecondSurvey.Abschnitt2 || page is Views.Surveys.SecondSurvey.Abschnitt3 || page is Views.Surveys.ThirdSurvey.Abschnitt1 || page is Views.Surveys.ThirdSurvey.Abschnitt2 || page is Views.Surveys.FirstSurvey.FourthSurvey.Home))
            {
                SurveyCount++;
            }            
            switch (type)
            {
                case SurveyType.SurveyOne:
                    switch (section)
                    {
                        case SurveySection.SectionOne:


                            if (SurveyCount == SurveyLibraries.SurveyOne.SectionOne.Questions.Count)
                            {
                                SurveyCount = 0;
                                SurveySection = SurveySection.SectionTwo;
                                await Application.Current.MainPage.Navigation.PushAsync(new Views.Surveys.FirstSurvey.Abschnitt2());
                                break;

                            }
                            else
                            {
                                if (SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount] is SingleAnswerQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount]));

                                }
                                else if (SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount] is MultipleChoiceQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount] is MatrixQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new MatrixPage((MatrixQuestion)SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount]));
                                }
                            }
                            break;

                        case SurveySection.SectionTwo:
                            if (SurveyCount == SurveyLibraries.SurveyOne.SectionTwo.Questions.Count)
                            {
                                SurveySection = SurveySection.SectionThree;
                                SurveyCount = 0;
                                await Application.Current.MainPage.Navigation.PushAsync(new Views.Surveys.FirstSurvey.Abschnitt3());
                            }
                            else
                            {
                                if (SurveyLibraries.SurveyOne.SectionTwo.Questions[SurveyCount] is SingleAnswerQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.SurveyOne.SectionTwo.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyOne.SectionTwo.Questions[SurveyCount] is MultipleChoiceQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.SurveyOne.SectionTwo.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyOne.SectionTwo.Questions[SurveyCount] is MatrixQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new MatrixPage((MatrixQuestion)SurveyLibraries.SurveyOne.SectionTwo.Questions[SurveyCount]));
                                }
                            }
                            break;

                        case SurveySection.SectionThree:
                            if (SurveyCount == SurveyLibraries.SurveyOne.SectionThree.Questions.Count)
                            {
                                await Upload("1");
                                break;
                            }
                            else
                            {
                                if (SurveyLibraries.SurveyOne.SectionThree.Questions[SurveyCount] is SingleAnswerQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.SurveyOne.SectionThree.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyOne.SectionThree.Questions[SurveyCount] is MultipleChoiceQuestion)

                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.SurveyOne.SectionThree.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyOne.SectionThree.Questions[SurveyCount] is MatrixQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new MatrixPage((MatrixQuestion)SurveyLibraries.SurveyOne.SectionThree.Questions[SurveyCount]));
                                }
                            }
                            break;
                    }
                    break;

                case SurveyType.SurveyTwo:
                    switch (section)
                    {
                        case SurveySection.SectionOne:
                            if (SurveyCount == SurveyLibraries.SurveyTwo.SectionOne.Questions.Count)
                            {
                                SurveyCount = 0;
                                SurveySection = SurveySection.SectionTwo;
                                await Application.Current.MainPage.Navigation.PushAsync(new Views.Surveys.SecondSurvey.Abschnitt2());
                            }
                            else
                            {
                                if (SurveyLibraries.SurveyTwo.SectionOne.Questions[SurveyCount] is SingleAnswerQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.SurveyTwo.SectionOne.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyTwo.SectionOne.Questions[SurveyCount] is MultipleChoiceQuestion)

                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.SurveyTwo.SectionOne.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyTwo.SectionOne.Questions[SurveyCount] is MatrixQuestion)

                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new MatrixPage((MatrixQuestion)SurveyLibraries.SurveyTwo.SectionOne.Questions[SurveyCount]));
                                }
                            }
                            break;

                        case SurveySection.SectionTwo:
                            if (SurveyCount == SurveyLibraries.SurveyTwo.SectionTwo.Questions.Count)
                            {
                                SurveyCount = 0;
                                SurveySection = SurveySection.SectionThree;
                                await Application.Current.MainPage.Navigation.PushAsync(new Views.Surveys.SecondSurvey.Abschnitt3());
                            }
                            else
                            {
                                if (SurveyLibraries.SurveyTwo.SectionTwo.Questions[SurveyCount] is SingleAnswerQuestion)
                                {

                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.SurveyTwo.SectionTwo.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyTwo.SectionTwo.Questions[SurveyCount] is MultipleChoiceQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.SurveyTwo.SectionTwo.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyTwo.SectionTwo.Questions[SurveyCount] is MatrixQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new MatrixPage((MatrixQuestion)SurveyLibraries.SurveyTwo.SectionTwo.Questions[SurveyCount]));
                                }
                            }
                            break;

                        case SurveySection.SectionThree:
                            if (SurveyCount == SurveyLibraries.SurveyTwo.SectionThree.Questions.Count)
                            {
                                await Upload("2");
                                break;
                            }
                            else
                            {
                                if (SurveyLibraries.SurveyTwo.SectionThree.Questions[SurveyCount] is SingleAnswerQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.SurveyTwo.SectionThree.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyTwo.SectionThree.Questions[SurveyCount] is MultipleChoiceQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.SurveyTwo.SectionThree.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyTwo.SectionThree.Questions[SurveyCount] is MatrixQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new MatrixPage((MatrixQuestion)SurveyLibraries.SurveyTwo.SectionThree.Questions[SurveyCount]));
                                }
                            }
                            break;
                    }
                    break;

                case SurveyType.SurveyThree:
                    switch (section)
                    {
                        case SurveySection.SectionOne:
                            if (SurveyCount == SurveyLibraries.SurveyThree.SectionOne.Questions.Count)
                            {
                                SurveyCount = 0;
                                SurveySection = SurveySection.SectionTwo;
                                await Application.Current.MainPage.Navigation.PushAsync(new Views.Surveys.ThirdSurvey.Abschnitt2());
                            }
                            else
                            {
                                if (SurveyLibraries.SurveyThree.SectionOne.Questions[SurveyCount] is SingleAnswerQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.SurveyThree.SectionOne.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyThree.SectionOne.Questions[SurveyCount] is MultipleChoiceQuestion)

                                {

                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.SurveyThree.SectionOne.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyThree.SectionOne.Questions[SurveyCount] is MatrixQuestion)

                                {

                                    await Application.Current.MainPage.Navigation.PushAsync(new MatrixPage((MatrixQuestion)SurveyLibraries.SurveyThree.SectionOne.Questions[SurveyCount]));
                                }
                            }
                            break;

                        case SurveySection.SectionTwo:
                            if (SurveyCount == SurveyLibraries.SurveyThree.SectionTwo.Questions.Count)
                            {
                                await Upload("3");
                                break;
                            }
                            else
                            {
                                if (SurveyLibraries.SurveyThree.SectionTwo.Questions[SurveyCount] is SingleAnswerQuestion)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.SurveyThree.SectionTwo.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyThree.SectionTwo.Questions[SurveyCount] is MultipleChoiceQuestion)
                                {

                                    await Application.Current.MainPage.Navigation.PushAsync(new Views.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.SurveyThree.SectionTwo.Questions[SurveyCount]));
                                }
                                else if (SurveyLibraries.SurveyThree.SectionTwo.Questions[SurveyCount] is MatrixQuestion)
                                {

                                    await Application.Current.MainPage.Navigation.PushAsync(new MatrixPage((MatrixQuestion)SurveyLibraries.SurveyThree.SectionTwo.Questions[SurveyCount]));
                                }
                            }
                            break;


                    }
                    break;

                case SurveyType.SurveyFour:

                    if (SurveyCount == SurveyLibraries.SurveyOne.SectionOne.Questions.Count)
                    {
                        await Upload("4");
                        break;
                    }
                    else
                    {

                        if (SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount] is SingleAnswerQuestion)
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new Views.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount]));

                        }
                        else if (SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount] is MultipleChoiceQuestion)
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new Views.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount]));
                        }
                        else if (SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount] is MatrixQuestion)
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new MatrixPage((MatrixQuestion)SurveyLibraries.SurveyOne.SectionOne.Questions[SurveyCount]));
                        }

                    }
                    break;
            }
        }

        /// <summary>
        /// Navigates to the next diary question based on the given path and current diary count.
        /// </summary>
        /// <param name="path">The path of the diary (PathOne, PathTwo, or PathThree).</param>
        /// <returns>A task representing the asynchronous operation of navigating to the next page.</returns>
        public static async void GoToNextDiary(DiaryPath path)
        {
            // Switch based on the provided DiaryPath
            switch (path)
            {
                case DiaryPath.PathOne:
                    // For PathOne, navigate to different pages based on the DiaryCount
                    if (DiaryCount == 0)
                    {
                        // First question in PathOne, navigate to the SingleChoicePage
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[0]));
                    }
                    else if (DiaryCount == 1)
                    {
                        // Second question in PathOne, navigate to the MultipleChoicePage
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[1]));
                    }
                    else if (DiaryCount == 2)
                    {
                        // Third question in PathOne, navigate to the SingleChoicePage
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[2]));
                    }
                    else if (DiaryCount == 3)
                    {
                        // Fourth question in PathOne, navigate to the MultipleChoiceWithEditorPage
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.MultipleChoiceWithEditorPage((MultpleChoiceWithEditor)SurveyLibraries.Diary.DiaryLibrary.Questions[4]));
                    }
                    else
                    {
                        // No more questions, navigate to the editor
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.Editor());
                    }
                    break;

                case DiaryPath.PathTwo:
                    // For PathTwo, navigate to different pages based on the DiaryCount
                    if (DiaryCount == 0)
                    {
                        // First question in PathTwo, navigate to the SingleChoicePage
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[0]));
                    }
                    else if (DiaryCount == 1)
                    {
                        // Second question in PathTwo, navigate to the MultipleChoicePage
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.MultipleChoicePage((MultipleChoiceQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[3]));
                    }
                    else if (DiaryCount == 2)
                    {
                        // Third question in PathTwo, navigate to the SingleChoicePage
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[2]));
                    }
                    else
                    {
                        // No more questions, navigate to the editor
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.Editor());
                    }
                    break;

                case DiaryPath.PathThree:
                    // For PathThree, navigate to different pages based on the DiaryCount
                    if (DiaryCount == 0)
                    {
                        // First question in PathThree, navigate to the SingleChoicePage
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[0]));
                    }
                    else if (DiaryCount == 1)
                    {
                        // Second question in PathThree, navigate to the SingleChoicePage
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.SingleChoicePage((SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[2]));
                    }
                    else
                    {
                        // No more questions, navigate to the editor
                        await Application.Current.MainPage.Navigation.PushAsync(new Desive2.Views.DiaryQuestions.Editor());
                    }
                    break;
            }
        }

        /// <summary>
        /// Navigates to the previous diary question based on the given path and current diary count.
        /// Removes the corresponding question from the survey content before navigating to the previous page.
        /// </summary>
        /// <param name="path">The path of the diary (PathOne, PathTwo, or PathThree).</param>
        /// <returns>A task representing the asynchronous operation of navigating to the previous page.</returns>
        public static async void GoToPreviousDiary(DiaryPath path)
        {
            // If DiaryCount is 0, no previous question, so return immediately
            if (DiaryCount == 0)
                return;

            // Switch based on the provided DiaryPath
            switch (path)
            {
                case DiaryPath.PathOne:
                    // For PathOne, remove questions from SurveyContent based on DiaryCount
                    if (DiaryCount == 1)
                    {
                        // Remove first question from SingleChoice
                        SingleAnswerQuestion question = (SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[0];
                        SurveyContent.SingleChoice.Remove(question.QuestionText);
                    }
                    else if (DiaryCount == 2)
                    {
                        // Remove second question from MultipleChoice
                        MultipleChoiceQuestion question = (MultipleChoiceQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[1];
                        SurveyContent.MultipleChoice.Remove(question.QuestionText);
                    }
                    else if (DiaryCount == 3)
                    {
                        // Remove third question from MultipleChoice
                        SingleAnswerQuestion question = (SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[2];
                        SurveyContent.MultipleChoice.Remove(question.QuestionText);
                    }
                    else if (DiaryCount == 4)
                    {
                        // Remove fourth question from MultipleChoiceWithEditor
                        MultpleChoiceWithEditor question = (MultpleChoiceWithEditor)SurveyLibraries.Diary.DiaryLibrary.Questions[4];
                        SurveyContent.MultipleChoiceWithEditor.Remove(question.QuestionText);
                    }
                    break;

                case DiaryPath.PathTwo:
                    // For PathTwo, remove questions from SurveyContent based on DiaryCount
                    if (DiaryCount == 1)
                    {
                        // Switch path back to PathOne and remove first question from SingleChoice
                        DiaryPath = DiaryPath.PathOne;
                        SingleAnswerQuestion question = (SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[0];
                        SurveyContent.SingleChoice.Remove(question.QuestionText);
                    }
                    else if (DiaryCount == 2)
                    {
                        // Remove second question from MultipleChoice
                        MultipleChoiceQuestion question = (MultipleChoiceQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[3];
                        SurveyContent.MultipleChoice.Remove(question.QuestionText);
                    }
                    else if (DiaryCount == 3)
                    {
                        // Remove third question from MultipleChoice
                        SingleAnswerQuestion question = (SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[2];
                        SurveyContent.MultipleChoice.Remove(question.QuestionText);
                    }
                    break;

                case DiaryPath.PathThree:
                    // For PathThree, remove questions from SurveyContent based on DiaryCount
                    if (DiaryCount == 1)
                    {
                        // Switch path back to PathOne and remove first question from SingleChoice
                        DiaryPath = DiaryPath.PathOne;
                        SingleAnswerQuestion question = (SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[0];
                        SurveyContent.SingleChoice.Remove(question.QuestionText);
                    }
                    else if (DiaryCount == 2)
                    {
                        // Remove second question from SingleChoice
                        SingleAnswerQuestion question = (SingleAnswerQuestion)SurveyLibraries.Diary.DiaryLibrary.Questions[2];
                        SurveyContent.SingleChoice.Remove(question.QuestionText);
                    }
                    break;
            }

            // Decrement DiaryCount to navigate to the previous question
            DiaryCount--;

            // Pop the current page to go back to the previous one
            await Application.Current.MainPage.Navigation.PopAsync();
        }


        /// <summary>
        /// Uploads the survey answers to the database and navigates back to the main page upon success.
        /// Iterates over the navigation stack to extract answers from the pages and then uploads them.
        /// </summary>
        /// <param name="survey">The survey identifier used to upload the answers.</param>
        /// <returns>A task that represents the asynchronous upload operation, returning true if the upload is successful, otherwise false.</returns>
        private static async Task<bool> Upload(string survey)
        {
            // Get the count of pages in the navigation stack
            int cnt = Application.Current.MainPage.Navigation.NavigationStack.Count;

            // Iterate over each page in the navigation stack to gather answers
            for (int i = 0; i < cnt; i++)
            {
                // Pop the page from the navigation stack without removing it from history (false argument)
                Page page = await Application.Current.MainPage.Navigation.PopAsync(false);

                // Check the type of the page and retrieve the corresponding answers
                if (page is Views.SingleChoicePage)
                {
                    // If the page is a SingleChoicePage, retrieve answers from it
                    Views.SingleChoicePage singleChoice = (Views.SingleChoicePage)page;
                    singleChoice.GetAnswers();
                }
                else if (page is Views.MultipleChoicePage)
                {
                    // If the page is a MultipleChoicePage, retrieve answers from it
                    Views.MultipleChoicePage multPage = (Views.MultipleChoicePage)page;
                    multPage.GetAnswers();
                }
                else if (page is Views.MatrixPage)
                {
                    // If the page is a MatrixPage, retrieve answers from it
                    MatrixPage matrixPage = (MatrixPage)page;
                    matrixPage.GetAnswers();
                }
            }

            // Attempt to upload the survey answers to the database
            if (await Database.UploadSurveyAnswers(
                Preferences.Get("idUser", null),       // User ID from preferences
                GetSurveyJson(),                       // JSON representation of the survey answers
                survey,                                // Survey identifier
                Preferences.Get("loginToken", null)    // Login token from preferences
            ))
            {
                // If the upload is successful, show a success message and reset the survey state
                await App.Current.MainPage.DisplayAlert("Vielen Dank für Ihre Teilnahme!", "Ihre Antworten wurden erfolgreich hochgeladen.", "Okay");

                // Reset the SurveyCount and preferences
                SurveyCount = 0;
                Reset();
                Preferences.Set("Survey", survey);

                // Clear answer dictionaries
                SingleChoice = new Dictionary<string, string>();
                MultipleChoice = new Dictionary<string, List<string>>();
                Matrix = new Dictionary<string, Dictionary<string, string>>();

                // Navigate to the MainPage after the upload
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());

                return true;  // Return true to indicate success
            }
            else
            {
                // If the upload fails, return false
                return false;
            }
        }

    }
    /// <summary>
    /// Enum representing the different types of surveys.
    /// </summary>
    public enum SurveyType
    {
        /// <summary>
        /// Represents the first survey.
        /// </summary>
        SurveyOne,

        /// <summary>
        /// Represents the second survey.
        /// </summary>
        SurveyTwo,

        /// <summary>
        /// Represents the third survey.
        /// </summary>
        SurveyThree,

        /// <summary>
        /// Represents the fourth survey.
        /// </summary>
        SurveyFour
    }

    /// <summary>
    /// Enum representing the different sections of a survey.
    /// </summary>
    public enum SurveySection
    {
        /// <summary>
        /// Represents the first section of the survey.
        /// </summary>
        SectionOne,

        /// <summary>
        /// Represents the second section of the survey.
        /// </summary>
        SectionTwo,

        /// <summary>
        /// Represents the third section of the survey.
        /// </summary>
        SectionThree
    }

    /// <summary>
    /// Enum representing different diary paths.
    /// </summary>
    public enum DiaryPath
    {
        /// <summary>
        /// Represents the first diary path.
        /// </summary>
        PathOne,

        /// <summary>
        /// Represents the second diary path.
        /// </summary>
        PathTwo,

        /// <summary>
        /// Represents the third diary path.
        /// </summary>
        PathThree
    }

}
