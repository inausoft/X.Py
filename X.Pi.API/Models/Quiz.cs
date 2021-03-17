using System;
using System.Collections.Generic;

namespace X.Pi.API.Models
{
    public class Quiz
    {
        public Guid Id { get; internal set; }

        public string Name { get; set; }

        public List<QuizQuestion> QuizQuestions { get; set; }

        public Quiz()
        {
            Id = Guid.NewGuid();
            QuizQuestions = new List<QuizQuestion>();
        }

        //TODO remove
        public static Quiz CreateTestQuiz()
        {
            var quiz = new Quiz();
            quiz.Name = "Test Quiz";

            quiz.QuizQuestions.Add(new QuizQuestion()
            {
                Category = Category.WarmUp,
                Text = "Which of the following answers is correct?",
                CorrectAnswer = "Correct answer",
                PossibleAnswers = {"Wrong answer", "for sure not this one"}
            });

            quiz.QuizQuestions.Add(new QuizQuestion()
            {
                Category = Category.WarmUp,
                Text = "When will X-py be released?",
                CorrectAnswer = "In 1-2 weeks",
                PossibleAnswers = { "In the next year", "Never" }
            });

            quiz.QuizQuestions.Add(new QuizQuestion()
            {
                Category = Category.WarmUp,
                Text = "Will you help bring X-py to life?",
                CorrectAnswer = "Yeeeeeees!",
                PossibleAnswers = { "Not really", "X-py that shi.." }
            });

            return quiz;
        }
    }
}
