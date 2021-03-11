using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class Quiz
    {
        public string Name { get; set; }

        public List<QuizQuestion> QuizQuestions { get; set; }

        public Guid Id { get; internal set; }

        public Quiz()
        {
            Id = Guid.NewGuid();
            QuizQuestions = new List<QuizQuestion>();
        }

        public static Quiz CreateTestQuiz()
        {
            var quiz = new Quiz();
            quiz.Name = "Test Quiz";

            quiz.QuizQuestions.Add(new QuizQuestion()
            {
                Category = Category.WarmUp,
                Text = "Test tex question 1??",
                CorrectAnswer = "Correct Answer",
                PossibleAnswers = {"dsda", "dasdasad"}
            });

            quiz.QuizQuestions.Add(new QuizQuestion()
            {
                Category = Category.WarmUp,
                Text = "Jak nazywal sie pies myszki Mickey",
                CorrectAnswer = "Pluto",
                PossibleAnswers = { "Azor", "Rex" }
            });

            return quiz;
        }
    }
}
