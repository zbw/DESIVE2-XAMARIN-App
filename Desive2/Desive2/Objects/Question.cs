using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.Objects
{

    /// <summary>
    /// Represents a generic question in a survey or form.
    /// </summary>
    public abstract class Question
    {
        /// <summary>
        /// Gets or sets metadata text that provides additional information about the question.
        /// </summary>
        public string MetaText { get; set; }

        /// <summary>
        /// Gets or sets the question text that is displayed to the user.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets additional information about the question.
        /// </summary>
        public string Addition { get; set; }

        /// <summary>
        /// Gets or sets a list indicating whether the question has an entry for each possible answer.
        /// </summary>
        public List<bool> HasEntry { get; set; }

        /// <summary>
        /// Gets or sets a list indicating whether the next question should be skipped based on the answer.
        /// </summary>
        public List<bool> SkipsNextQuestion { get; set; }

        /// <summary>
        /// Abstract method to show the question in the appropriate format for the derived class.
        /// </summary>
        /// <returns>The string representation of the question to be shown to the user.</returns>
        public abstract string ShowQuestion();

        /// <summary>
        /// Abstract method to handle setting the answer for the question.
        /// </summary>
        public abstract void SetAnswer();
    }


    /// <summary>
    /// Repräsentiert eine Frage, die nur eine Antwort zulässt (Single-Choice-Frage).
    /// </summary>
    public class SingleAnswerQuestion : Question
    {
        /// <summary>
        /// Gets the list der möglichen Antworten für diese Frage.
        /// </summary>
        public List<string> Answers { get; private set; }

        /// <summary>
        /// Konstruktor für die SingleAnswerQuestion-Klasse, der MetaText, Zusatzinformationen,
        /// den Frageinhalt und eine Liste möglicher Antworten sowie die Skipping-Logik für die nächste Frage erhält.
        /// </summary>
        /// <param name="metaText">Metainformationen zur Frage.</param>
        /// <param name="addition">Zusätzliche Hinweise oder Anmerkungen zur Frage.</param>
        /// <param name="questionsText">Der Text der Frage, der dem Benutzer angezeigt wird.</param>
        /// <param name="answers">Liste möglicher Antworten für die Frage.</param>
        /// <param name="skipsNextQuestion">Liste, die angibt, ob die nächste Frage übersprungen werden soll.</param>
        public SingleAnswerQuestion(string metaText, string addition, string questionsText, List<string> answers, List<bool> skipsNextQuestion)
        {
            // Setzen der Eigenschaften basierend auf den übergebenen Werten
            Addition = addition;
            MetaText = metaText;
            QuestionText = questionsText;
            Answers = answers;
            HasEntry = new List<bool>(); // Leere Liste für 'HasEntry' erstellen
            SkipsNextQuestion = skipsNextQuestion;
        }

        /// <summary>
        /// Konstruktor für die SingleAnswerQuestion-Klasse, der MetaText, Zusatzinformationen,
        /// den Frageinhalt, eine Liste möglicher Antworten, eine Liste für 'HasEntry' und eine Liste für die Skip-Logik erhält.
        /// </summary>
        /// <param name="metaText">Metainformationen zur Frage.</param>
        /// <param name="addition">Zusätzliche Hinweise oder Anmerkungen zur Frage.</param>
        /// <param name="questionsText">Der Text der Frage, der dem Benutzer angezeigt wird.</param>
        /// <param name="answers">Liste möglicher Antworten für die Frage.</param>
        /// <param name="hasEntry">Liste, die angibt, ob für jede Antwort ein Eintrag erforderlich ist.</param>
        /// <param name="skipsNextQuestion">Liste, die angibt, ob die nächste Frage übersprungen werden soll.</param>
        public SingleAnswerQuestion(string metaText, string addition, string questionsText, List<string> answers, List<bool> hasEntry, List<bool> skipsNextQuestion)
        {
            // Setzen der Eigenschaften basierend auf den übergebenen Werten
            Addition = addition;
            MetaText = metaText;
            QuestionText = questionsText;
            Answers = answers;
            HasEntry = hasEntry;
            SkipsNextQuestion = skipsNextQuestion;
        }

        /// <summary>
        /// Gibt die Frage zusammen mit den möglichen Antworten als formatierte Zeichenkette zurück.
        /// </summary>
        /// <returns>Die Frage zusammen mit den möglichen Antworten als String.</returns>
        public override string ShowQuestion()
        {
            // Start der Frageanzeige
            string res = QuestionText + ":\n";

            // Iteriere über alle Antworten und füge sie dem Ergebnis hinzu
            foreach (string answer in Answers)
            {
                res += answer + "\n"; // Jede Antwort wird mit einem Zeilenumbruch hinzugefügt
            }

            // Trennlinie für das Ende der Frage
            res += "----------------------------------------\n";
            return res;
        }

        /// <summary>
        /// Diese Methode setzt die Antwort für diese Frage. Derzeit nicht implementiert.
        /// </summary>
        public override void SetAnswer()
        {
            // Die Implementierung dieser Methode ist noch nicht vorhanden
            // In einem realen Szenario würde hier die Antwort gesetzt werden, möglicherweise durch Benutzereingabe
        }
    }

    /// <summary>
    /// Repräsentiert eine Multiple-Choice-Frage, bei der der Benutzer mehrere Antworten auswählen kann.
    /// </summary>
    public class MultipleChoiceQuestion : Question
    {
        /// <summary>
        /// Gets the list der möglichen Antworten für diese Frage.
        /// </summary>
        public List<string> Answers { get; private set; }

        /// <summary>
        /// Gets the Liste, die angibt, ob eine Antwort deaktiviert werden soll.
        /// </summary>
        public List<bool> SetsInactive { get; private set; }

        /// <summary>
        /// Konstruktor für die MultipleChoiceQuestion-Klasse, der MetaText, Zusatzinformationen,
        /// den Frageinhalt, eine Liste möglicher Antworten sowie eine Liste für 'HasEntry' und die Skip-Logik erhält.
        /// </summary>
        /// <param name="metaText">Metainformationen zur Frage.</param>
        /// <param name="addition">Zusätzliche Hinweise oder Anmerkungen zur Frage.</param>
        /// <param name="questionsText">Der Text der Frage, der dem Benutzer angezeigt wird.</param>
        /// <param name="answers">Liste möglicher Antworten für die Frage.</param>
        /// <param name="hasEntry">Liste, die angibt, ob für jede Antwort ein Eintrag erforderlich ist.</param>
        /// <param name="skipsNextQuestion">Liste, die angibt, ob die nächste Frage übersprungen werden soll.</param>
        public MultipleChoiceQuestion(string metaText, string addition, string questionsText, List<string> answers, List<bool> hasEntry, List<bool> skipsNextQuestion)
        {
            // Setzen der Eigenschaften basierend auf den übergebenen Werten
            Addition = addition;
            MetaText = metaText;
            QuestionText = questionsText;
            Answers = answers;
            HasEntry = hasEntry;
            SetsInactive = new List<bool>(); // Leere Liste für 'SetsInactive' erstellen
            SkipsNextQuestion = skipsNextQuestion;
        }

        /// <summary>
        /// Konstruktor für die MultipleChoiceQuestion-Klasse, der MetaText, Zusatzinformationen,
        /// den Frageinhalt, eine Liste möglicher Antworten, eine Liste für 'HasEntry', eine Liste für 'SetsInactive'
        /// und eine Liste für die Skip-Logik erhält.
        /// </summary>
        /// <param name="metaText">Metainformationen zur Frage.</param>
        /// <param name="addition">Zusätzliche Hinweise oder Anmerkungen zur Frage.</param>
        /// <param name="questionsText">Der Text der Frage, der dem Benutzer angezeigt wird.</param>
        /// <param name="answers">Liste möglicher Antworten für die Frage.</param>
        /// <param name="hasEntry">Liste, die angibt, ob für jede Antwort ein Eintrag erforderlich ist.</param>
        /// <param name="setsInactive">Liste, die angibt, ob eine Antwort deaktiviert werden soll.</param>
        /// <param name="skipsNextQuestion">Liste, die angibt, ob die nächste Frage übersprungen werden soll.</param>
        public MultipleChoiceQuestion(string metaText, string addition, string questionsText, List<string> answers, List<bool> hasEntry, List<bool> setsInactive, List<bool> skipsNextQuestion)
        {
            // Setzen der Eigenschaften basierend auf den übergebenen Werten
            Addition = addition;
            MetaText = metaText;
            QuestionText = questionsText;
            Answers = answers;
            HasEntry = hasEntry;
            SetsInactive = setsInactive;
            SkipsNextQuestion = skipsNextQuestion;
        }

        /// <summary>
        /// Gibt die Frage zusammen mit den möglichen Antworten als formatierte Zeichenkette zurück.
        /// </summary>
        /// <returns>Die Frage zusammen mit den möglichen Antworten als String.</returns>
        public override string ShowQuestion()
        {
            // Start der Frageanzeige
            string res = QuestionText + ":\n";

            // Iteriere über alle Antworten und füge sie dem Ergebnis hinzu
            foreach (string answer in Answers)
            {
                res += answer + "\n"; // Jede Antwort wird mit einem Zeilenumbruch hinzugefügt
            }

            // Trennlinie für das Ende der Frage
            res += "----------------------------------------\n";
            return res;
        }

        /// <summary>
        /// Diese Methode setzt die Antwort für diese Frage. Derzeit nicht implementiert.
        /// </summary>
        public override void SetAnswer()
        {
            // Die Implementierung dieser Methode ist noch nicht vorhanden
            // In einem realen Szenario würde hier die Antwort gesetzt werden, möglicherweise durch Benutzereingabe
        }
    }


    /// <summary>
    /// Repräsentiert eine Matrixfrage, bei der eine Sammlung von Antworten (Matrix-Fragen) in einer strukturierten Weise angezeigt wird.
    /// </summary>
    public class MatrixQuestion : Question
    {
        /// <summary>
        /// Gets die Liste von Antworten, die für diese Matrixfrage zur Verfügung stehen.
        /// </summary>
        public List<MatrixQuestionAnswers> MatrixQuestionAnswers { get; private set; }

        /// <summary>
        /// Gets oder setzt die Rangfolge für die Matrixfrage.
        /// </summary>
        public string Ranking { get; set; }

        /// <summary>
        /// Konstruktor für die MatrixQuestion-Klasse, der MetaText, MetaFrage, Zusatzinformationen, Ranking und eine Liste von MatrixFragen erhält.
        /// </summary>
        /// <param name="metaText">Metainformationen zur Frage.</param>
        /// <param name="metaQuestion">Der Text der Frage, die der Benutzer beantworten soll.</param>
        /// <param name="addition">Zusätzliche Informationen oder Hinweise zur Frage.</param>
        /// <param name="ranking">Das Ranking oder die Reihenfolge der Antwortmöglichkeiten.</param>
        /// <param name="questions">Liste von MatrixAntworten, die mit dieser Frage verbunden sind.</param>
        public MatrixQuestion(string metaText, string metaQuestion, string addition, string ranking, List<MatrixQuestionAnswers> questions)
        {
            // Setzen der Eigenschaften basierend auf den übergebenen Werten
            Addition = addition;
            MetaText = metaText;
            QuestionText = metaQuestion;
            MatrixQuestionAnswers = questions;
            Ranking = ranking;
        }

        /// <summary>
        /// Gibt die Frage zusammen mit den möglichen Antworten als formatierte Zeichenkette zurück.
        /// </summary>
        /// <returns>Die Frage zusammen mit den möglichen Antworten als String.</returns>
        public override string ShowQuestion()
        {
            // Start der Frageanzeige
            string res = QuestionText + ":\n";

            // Iteriere über alle Matrixantworten und füge sie der Ausgabe hinzu
            foreach (MatrixQuestionAnswers question in MatrixQuestionAnswers)
            {
                res += question.ShowQuestion() + "\n"; // Jede Antwort wird mit einem Zeilenumbruch hinzugefügt
            }

            // Trennlinie für das Ende der Frage
            res += "----------------------------------------\n";
            return res;
        }

        /// <summary>
        /// Diese Methode setzt die Antwort für diese Matrixfrage. Derzeit nicht implementiert.
        /// </summary>
        public override void SetAnswer()
        {
            // Die Implementierung dieser Methode ist noch nicht vorhanden
            // In einem realen Szenario würde hier die Antwort gesetzt werden, möglicherweise durch Benutzereingabe
        }
    }

    /// <summary>
    /// Repräsentiert eine einzelne Matrixantwortfrage, die eine Liste von Antwortmöglichkeiten enthält.
    /// </summary>
    /// <summary>
    /// Represents an individual answer set in a matrix question, where each answer corresponds to a part of the matrix.
    /// </summary>
    public class MatrixQuestionAnswers : Question
    {
        /// <summary>
        /// Gets the list of possible answers for this matrix question.
        /// </summary>
        public List<string> Answers { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixQuestionAnswers"/> class.
        /// </summary>
        /// <param name="questionsText">The text of the question.</param>
        /// <param name="answers">The list of possible answers for this question.</param>
        public MatrixQuestionAnswers(string questionsText, List<string> answers)
        {
            QuestionText = questionsText;
            Answers = answers;
        }

        /// <summary>
        /// Displays the question and its possible answers as a formatted string.
        /// </summary>
        /// <returns>A formatted string containing the question and its answers.</returns>
        public override string ShowQuestion()
        {
            string res = QuestionText + ":\n";

            // Loop through each answer and add it to the result string
            foreach (string answer in Answers)
            {
                res += answer + "\n";
            }

            // Add a separator at the end of the question block
            res += "----------------------------------------\n";
            return res;
        }

        /// <summary>
        /// Sets the answer for this matrix question. Currently not implemented.
        /// </summary>
        public override void SetAnswer()
        {
            // Method implementation is not yet provided
            // Future functionality could handle setting the user's answer
        }
    }


    /// <summary>
    /// Represents a multiple choice question that includes an editor for custom answers.
    /// </summary>
    public class MultpleChoiceWithEditor : Question
    {
        /// <summary>
        /// Gets the list of possible answers, where each answer is a tuple containing the answer text and additional editor information.
        /// </summary>
        public List<Tuple<string, string>> Answers { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultpleChoiceWithEditor"/> class.
        /// </summary>
        /// <param name="metaText">The meta text associated with the question.</param>
        /// <param name="addition">Additional information related to the question.</param>
        /// <param name="questionsText">The text of the question itself.</param>
        /// <param name="answers">The list of possible answers, each with an associated editor text.</param>
        public MultpleChoiceWithEditor(string metaText, string addition, string questionsText, List<Tuple<string, string>> answers)
        {
            Addition = addition;
            MetaText = metaText;
            QuestionText = questionsText;
            Answers = answers;
        }

        /// <summary>
        /// Sets the answer for this multiple choice question. Currently not implemented.
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown when the method is called as it is not implemented yet.</exception>
        public override void SetAnswer()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Displays the question and its possible answers as a formatted string. Currently not implemented.
        /// </summary>
        /// <returns>A formatted string containing the question and its answers. Currently throws an exception.</returns>
        /// <exception cref="NotImplementedException">Thrown when the method is called as it is not implemented yet.</exception>
        public override string ShowQuestion()
        {
            throw new NotImplementedException();
        }
    }


}
