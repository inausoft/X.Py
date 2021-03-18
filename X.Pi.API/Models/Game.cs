using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class Game
    {
        [JsonIgnore]
        public Quiz Quiz { get; set; }

        public GameState State { get; set; }

        public Question ActiveQuestion { get; set; }

        public Player Player { get; set; }

        public TimeSpan TimeLeft { get; set; }

        public List<Player> Players { get; set; }

        public Game()
        {
            Players = new List<Player>();
        }
    }
}
