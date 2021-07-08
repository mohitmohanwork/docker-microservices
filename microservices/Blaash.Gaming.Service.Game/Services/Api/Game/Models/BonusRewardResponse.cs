namespace Blaash.Gaming.Service.GamePlay
{
    public class BonusRewardResponse
    {
        public string GameSessionID { get; set; }

        public BonusRewardDataResponse RewardItem { get; set; }

        public long NextRewadLevel { get; set; }

    }
}
