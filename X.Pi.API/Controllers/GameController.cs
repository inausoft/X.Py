using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using X.Pi.API.Models;
using X.Pi.API.Services;

namespace X.Pi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
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
        public void StartGame()
        {
            _gameService.StartGame();
        }

        [HttpPost]
        [Route("[action]")]
        public Game Answer([FromBody] AnswerRequest request)
        {
            _gameService.ValidateRespond(request.PlayerToken, request.AnswerId).Last();
            return _gameService.ActiveGame;
        }

        [HttpGet("{id}")]
        public Game Get(Guid id)
        {
            return _gameService.ActiveGame;
        }
    }
}