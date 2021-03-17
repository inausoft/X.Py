using Newtonsoft.Json;
using System;

namespace X.Pi.API.Models
{
    public class GameNotification
    {
        [JsonProperty("state")]
        public GameState State { get; set; }

        [JsonProperty("timeLeft")]
        public string TimeLeft { get; set; }

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
