using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using X.Pi.API.Models;
using X.Pi.API.Services;
using X.Pi.API.Services.Interfaces;

namespace X.Pi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;
        private readonly IPlayerService _playerService;

        public QuizController(QuizService quizService, IPlayerService playerService)
        {
            _quizService = quizService;
            _playerService = playerService;
        }

        [HttpPost]
        [Route("[action]")]
        public void StartQuiz()
        {
            _quizService.StartQuiz();
        }

        [HttpPost]
        [Route("[action]")]
        public Guid AddQuiz([FromBody] QuizSource quiz)
        {
            //_quizService.Quizes.Add(quiz);

            return quiz.Id;
        }


        [HttpGet("{id}")]
        public Quiz Get(Guid id)
        {
            Quiz quiz = new Quiz();
            quiz.Player = _playerService.GetPlayer(id);
            quiz.ActiveQuestion = _quizService.ActiveQuestion;
            quiz.State = _quizService.State;

            return quiz;
        }

        //[HttpGet]
        //public List<Quiz> Get()
        //{
        //    return _quizService.Quizes;
        //}

        [HttpPost]
        [Route("[action]")]
        public AnswerRecord Answer([FromBody] AnswerRequest request)
        {
            return _quizService.ValidateRespond(request.PlayerToken, request.AnswerId).Last();
        }
    }
}