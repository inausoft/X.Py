using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class Player
    {
        internal Guid Id { get; private set; }
        public string Nickname { get; private set; }

        public int Score
        {
            get { return answersHistory.Sum(x => Convert.ToInt32(x.IsCorrect)); }
        }

        public List<AnswerRecord> answersHistory { get; private set; }

        public Player(Guid id, string name)
        {
            Id = id;
            Nickname = name;

            answersHistory = new List<AnswerRecord>();
        }
    }
}
