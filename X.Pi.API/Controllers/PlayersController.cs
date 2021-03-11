using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using X.Pi.API.Models;
using X.Pi.API.Services;
using X.Pi.API.Services.Interfaces;

namespace X.Pi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly Services.Game quizService;

        private readonly IPlayerService playerService;

        public PlayersController(Services.Game game, IPlayerService playerService)
        {
            if(game == null)
            {
                throw new ArgumentNullException(nameof(playerService));
            }

            if(playerService == null)
            {
                throw new ArgumentNullException(nameof(playerService));
            }

            this.quizService = game;
            this.playerService = playerService;
        }

        [HttpGet]
        public List<Player> GetStandings()
        {
            return playerService.participants.OrderByDescending(x => x.Score).Take(3).ToList();
        }

        [HttpPost]
        public RegisterResponse Register([FromBody] RegisterRequest request)
        {
            return new RegisterResponse() { Token = playerService.RegisterPlayer(request.Name) };
        }

        [HttpPost]
        [Route("[action]")]
        public AnswerRecord Answer([FromBody] AnswerRequest request)
        {
            return quizService.ValidateRespond(request.PlayerToken, request.AnswerId).Last();
        }
    }
}