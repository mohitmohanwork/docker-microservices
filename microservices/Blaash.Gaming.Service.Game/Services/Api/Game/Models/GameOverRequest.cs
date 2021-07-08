using System;
namespace Blaash.Gaming.Service.GamePlay
{
    /// <summary>
    /// This will be called when Player Clicks on the Close Button
    /// </summary>
    public class GameOverRequest
    {
        public string GameSessionID { get; set; }

        public int CurrentLevel { get; set; }

        public int CurrentScore { get; set; }
    }
}


