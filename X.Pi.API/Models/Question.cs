using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class Question
    {
        internal Guid QuestionId { get; set; }

        public string Text { get; set; }

        public Category Category { get; set; }

        public List<Answer> Answers { get; set; }

        internal int CorrectAnswerId { get; set; }

        public Question()
        {
            QuestionId = Guid.NewGuid();
            Answers = new List<Answer>();
        }
    }

    public class QuizQuestion
    {
        public string Text { get; set; }

        public Category Category { get; set; }

        public string CorrectAnswer { get; set; }

        public List<string> PossibleAnswers { get; set; }

        public QuizQuestion()
        {
            PossibleAnswers = new List<string>();
        }

        public Question CreateQuestion()
        {
            var question = new Question()
            {
                Category = Category,
                Text = Text,
            };
            question.Answers.Add(new Answer(1, CorrectAnswer));
            for(int i = 0; i< PossibleAnswers.Count; i++)
            {
                question.Answers.Add(new Answer(i + 2, PossibleAnswers[i]));
            }

            question.CorrectAnswerId = 1;

            return question;
        }
    }
}
