using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class AnswerRequest
    {
        public int AnswerId { get; set; }

        public Guid PlayerToken { get; set; }
    }
}
