using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class Game
    {
        public GameState State { get; set; }

        public Question ActiveQuestion { get; set; }

        public Player Player { get; set; }

        public TimeSpan TimeLeft { get; set; }
    }

    public enum GameState
    {
        WaitingForStart,
        QuestionAsked,
        QuestionResults,
        QuizResults,
        NotScheduled
    }
}
