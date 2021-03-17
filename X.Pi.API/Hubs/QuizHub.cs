using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using X.Pi.API.Services;

namespace X.Pi.API.Hubs
{
    public class QuizHub : Hub
    {
        private readonly GameService playerService;

        public QuizHub()
        {
            //this.playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
        }

        public override Task OnConnectedAsync()
        {
            //playerService.ActiveConnectionsCount++;
            //Clients.All.SendAsync("PlayersCountChnaged", playerService.ActiveConnectionsCount);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //playerService.ActiveConnectionsCount--;
            //Clients.All.SendAsync("PlayersCountChnaged", playerService.ActiveConnectionsCount);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
