using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Pi.API.Models;

namespace X.Pi.API.Services.Interfaces
{
    public interface IPlayerService
    {
        int RegistratedPlayerCount { get; set; }

        Guid RegisterPlayer(string nickname);

        Player GetPlayer(Guid id);

        IEnumerable<Player> participants { get; }
    }
}
