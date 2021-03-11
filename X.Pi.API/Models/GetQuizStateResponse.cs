using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class GetQuizStateResponse
    {
        public Question Question { get; set; }

        public Player Player { get; set; }

        public GetQuizStateResponse(Question question, Player player)
        {
            Question = question;
            Player = player;
        }
    }
}
