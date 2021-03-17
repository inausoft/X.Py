using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace X.Pi.API.Hubs
{
    public class QuizHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
