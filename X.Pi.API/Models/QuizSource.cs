using System;
using System.Collections.Generic;

namespace X.Pi.API.Models
{
    public class QuizSource
    {
        public Guid Id { get; internal set; }

        public string Name { get; set; }

        public List<QuizQuestion> QuizQuestions { get; set; }

        public QuizSource()
        {
            Id = Guid.NewGuid();
            QuizQuestions = new List<QuizQuestion>();
        }

        //TODO remove
        public static QuizSource CreateTestQuiz()
        {
            var quiz = new QuizSource();
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
