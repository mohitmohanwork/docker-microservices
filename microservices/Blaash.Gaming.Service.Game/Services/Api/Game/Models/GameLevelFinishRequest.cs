using System;
namespace Blaash.Gaming.Service.GamePlay
{
    public class GameLevelCompleteRequest
    {
        public string GameSessionID { get; set; }

        public int CurrentLevel { get; set; }

        public int CurrentScore { get; set; }
    }
}
