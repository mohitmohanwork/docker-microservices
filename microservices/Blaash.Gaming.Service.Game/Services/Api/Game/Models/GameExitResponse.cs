using System;
namespace Blaash.Gaming.Service.GamePlay
{
    public class GameExitResponse
    {
        public string GameSessionID { get; set; }

        public bool IsCancelled { get; set; }
    }
}
