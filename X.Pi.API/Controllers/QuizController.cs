using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.Pi.API.Models;
using X.Pi.API.Services;
using X.Pi.API.Services.Interfaces;

namespace X.Pi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly Services.Game _quizService;
        private readonly IPlayerService _playerService;

        public QuizController(Services.Game quizService, IPlayerService playerService)
        {
            _quizService = quizService;
            _playerService = playerService;
        }

        [HttpPost]
        public bool Register([FromBody] Quiz quiz)
        {

            return true;
        }

        [HttpPost]
        [Route("[action]")]
        public void StartQuiz()
        {
            _quizService.StartQuiz();
        }

        [HttpPost]
        [Route("[action]")]
        public Guid AddQuiz([FromBody] Quiz quiz)
        {
            _quizService.Quizes.Add(quiz);

            return quiz.Id;
        }


        [HttpGet("{id}")]
        public Models.Game Get(Guid id)
        {
            Models.Game game = new Models.Game();
            game.Player = _playerService.GetPlayer(id);
            game.ActiveQuestion = _quizService.ActiveQuestion;
            game.State = _quizService.State;

            return game;
        }

        [HttpGet]
        public List<Quiz> Get()
        {
            return _quizService.Quizes;
        }
    }
}