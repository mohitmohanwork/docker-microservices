namespace Blaash.Gaming.Service.GamePlay
{
    public class GameLaunchResponse : GameEventResponse
    {
        public string Token { set; get; }
        public string GameSessionID { get; set; }

    }

    public class GameEventResponse
    {
        public int CurrentLevel { get; set; }
        public int CurrentScore { get; set; }

        public int RewardLevel { get; set; }
        public long RewardID { get; set; }

        public string RewardText { get; set; }
        public string SocialShareMessage { get; set; }
    }


}
