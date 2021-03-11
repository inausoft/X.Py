using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Pi.API.Models;
using X.Pi.API.Services.Interfaces;

namespace X.Pi.API.Services
{
    public class InMemoryPlayerService : IPlayerService
    {
        [Obsolete]
        public int ActiveConnectionsCount { get; set; }
        public int RegistratedPlayerCount { get; set; }

        public List<Player> Players { get; set; }

        public IEnumerable<Player> participants => Players;

        public InMemoryPlayerService()
        {
            Players = new List<Player>();
        }

        public Guid RegisterPlayer(string name)
        {
            Guid playerToken = Guid.NewGuid();

            Players.Add(new Player(playerToken, name));

            return playerToken;
        }

        public Player GetPlayer(Guid id)
        {
            return Players.First(x => x.Id == id);
        }
    }
}
