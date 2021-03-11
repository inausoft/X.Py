using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Pi.API.Models;

namespace X.Pi.API.Services
{
    public interface IQuestionsService
    {
        Question GetNextQuestion();

        bool AreQuestionsLeft();
    }
}
