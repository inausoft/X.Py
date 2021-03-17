using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Pi.API.Models
{
    public class GameNotification
    {
        [JsonProperty("state")]
        GameState State { get; set; }

        [JsonProperty("timeLeft")]
        string TimeLeft { get; set; }

        public GameNotification(GameState state, TimeSpan timeLeft)
        {
            State = state;
            if(timeLeft != null)
            {
                TimeLeft = timeLeft.ToString(@"mm\:ss");
            }
        }
    }
}
