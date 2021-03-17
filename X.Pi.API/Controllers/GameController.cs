using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using X.Pi.API.Hubs;
using X.Pi.API.Models;
using X.Pi.API.Services;

namespace X.Pi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        //private readonly IPlayerService _playerService;
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            //_playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
        }

        [HttpGet]
        public List<Player> GetStandings()
        {
            return _gameService.ActiveGame.Players.OrderByDescending(x => x.Score).Take(3).ToList();
        }

        [HttpPost]
        public RegisterResponse Register([FromBody] RegisterRequest request)
        {
            var resp = new RegisterResponse()
            {
                Token = _gameService.RegisterPlayer(request.Name)
            };
            
            return resp;
        }

        [HttpPost]
        [Route("[action]")]
        public void StartQuiz()
        {
            _gameService.StartQuiz();
        }

        [HttpPost]
        [Route("[action]")]
        public Game Answer([FromBody] AnswerRequest request)
        {
            _gameService.ValidateRespond(request.PlayerToken, request.AnswerId).Last();
            return new Game();
        }

        [HttpGet("{id}")]
        public Game Get(Guid id)
        {
            Game quiz = new Game();
            quiz.Player = _gameService.ActiveGame.Players.FirstOrDefault(it => it.Id == id);
            quiz.ActiveQuestion = _gameService.ActiveQuestion;
            quiz.State = _gameService.State;

            return quiz;
        }
    }
}