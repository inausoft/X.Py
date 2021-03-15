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
        private readonly IPlayerService playerService;

        public PlayersController(IPlayerService playerService)
        {
            this.playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
        }

        [HttpGet]
        public List<Player> GetStandings()
        {
            return playerService.GetAllPlayers().OrderByDescending(x => x.Score).Take(3).ToList();
        }

        [HttpPost]
        public RegisterResponse Register([FromBody] RegisterRequest request)
        {
            return new RegisterResponse() { Token = playerService.RegisterPlayer(request.Name) };
        }
    }
}