using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Pi.API.Models;

namespace X.Pi.API.Services
{
    public class InMemoryQuizService
    {
        public List<Quizz> quizes;

        public InMemoryQuizService()
        {
            quizes = new List<Quizz>();
        }

        public void AddQuiz(Quizz quiz)
        {
            quizes.Add(quiz);
        }

        public Quizz GetQuiz(Guid id)
        {
            return new Quizz(null, null);
        }
    }
}
