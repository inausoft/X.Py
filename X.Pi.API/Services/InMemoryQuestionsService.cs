using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Pi.API.Models;

namespace X.Pi.API.Services
{
    public class InMemoryQuestionsService : IQuestionsService
    {
        private int count = 0;
        private List<Question> questions;

        public InMemoryQuestionsService()
        {
            questions = new List<Question>();
            questions.Add(new Question()
            {
                Text = "Jak nazywa sie obecny engineering manager w naszym projekcie?",
                Category = Category.WarmUp,
                Answers = new List<Answer>()
                {
                    new Answer(1, "Bartek Gąsiorek"),
                    new Answer(2, "Bartek Ślimaczek"),
                    new Answer(3, "Bartek Dżdżowniczek"),
                },
                CorrectAnswerId = 1
            });
            questions.Add(new Question()
            {
                Text = "Jaki aktor wcielił się w rolę Batmana w filmie \"Dark Knight\" ?",
                Category = Category.WarmUp,
                Answers = new List<Answer>()
                {
                    new Answer(1, "Samuel L. Jackosn"),
                    new Answer(2, "Christian Bale"),
                    new Answer(3, "Robert De Niro"),
                },
                CorrectAnswerId = 2
            });
            questions.Add(new Question()
            {
                Text = "Kto zkomponował \"Nad pięknym modrym Dunajem\" ?",
                Category = Category.WarmUp,
                Answers = new List<Answer>()
                {
                    new Answer(1, "Mozart"),
                    new Answer(2, "Beethoven"),
                    new Answer(3, "J. Strauss"),
                },
                CorrectAnswerId = 3
            });

        }

        public Question GetNextQuestion()
        {
            var questionId = count;
            count++;
            return questions[questionId];
        }

        public bool AreQuestionsLeft()
        {
            return count < questions.Count;
        }
    }
}
