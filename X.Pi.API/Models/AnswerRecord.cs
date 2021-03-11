using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class AnswerRecord
    {
        public Guid QuestionId { get; set; }

        public int AnswerId { get; set; }

        public bool IsCorrect { get; set; }

        public AnswerRecord(Guid questionId, int answerId)
        {
            QuestionId = questionId;
            AnswerId = answerId;
        }
    }
}
