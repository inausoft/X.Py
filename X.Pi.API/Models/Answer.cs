using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int responsesCount { get; set; }

        public Answer(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public Answer() { }
    }
}
