using System;
namespace Blaash.Gaming.Service
{
    public class BonusRewardRequest
    {
        public string GameSessionID { get; set; }

        public int CurrentLevel { get; set; }
    }
}
