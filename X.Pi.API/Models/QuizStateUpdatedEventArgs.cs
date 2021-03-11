using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class QuizUpdatedEventArgs : EventArgs
    {
        public Quizz Quiz { get; set; }

        public QuizUpdatedEventArgs(Quizz quiz)
        {
            Quiz = quiz;
        }
    }
}
