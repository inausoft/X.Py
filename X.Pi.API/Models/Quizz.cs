using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace X.Pi.API.Models
{
    public class Quizz
    {
        public string Name { get; set; }

        public List<Question> Questions { get; private set; }

        public Quizz(string name, List<Question> questions)
        {

            Questions = questions;
        }

        //To be removed once better mechanism will be created.
        //private void FillQuizWithStaticQuestions()
        //{
        //    Questions.Add(new Question()
        //    {
        //        Description = "Jak nazywa sie obecny engineering manager w naszym projekcie?",
        //        Category = Category.WarmUp,
        //        Answers = new List<Answer>()
        //        {
        //            new Answer(1, "Bartek Gąsiorek"),
        //            new Answer(2, "Bartek Ślimaczek"),
        //            new Answer(3, "Bartek Dżdżowniczek"),
        //        },
        //        CorrectAnswerId = 1
        //    });
        //    Questions.Add(new Question()
        //    {
        //        Description = "Jaki aktor wcielił się w rolę Batmana w filmie \"Dark Knight\" ?",
        //        Category = Category.WarmUp,
        //        Answers = new List<Answer>()
        //        {
        //            new Answer(1, "Samuel L. Jackosn"),
        //            new Answer(2, "Christian Bale"),
        //            new Answer(3, "Robert De Niro"),
        //        },
        //        CorrectAnswerId = 2
        //    });
        //    Questions.Add(new Question()
        //    {
        //        Description = "Kto zkomponował \"Nad pięknym modrym Dunajem\" ?",
        //        Category = Category.WarmUp,
        //        Answers = new List<Answer>()
        //        {
        //            new Answer(1, "Mozart"),
        //            new Answer(2, "Beethoven"),
        //            new Answer(3, "J. Strauss"),
        //        },
        //        CorrectAnswerId = 3
        //    });
        //}
    }
}
