using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class Answerrr
    {
        public Player Player { get; private set; }

        public Question Question { get; private set; }

        public int AnswerId { get; private set; }

        public Answerrr(Player player, Question question, int answerId, TimeSpan time)
        {
            Player = player;
            Question = question;
            AnswerId = answerId;
        }
    }
}
