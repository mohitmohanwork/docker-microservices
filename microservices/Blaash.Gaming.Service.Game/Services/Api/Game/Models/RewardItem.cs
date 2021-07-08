using System;
namespace Blaash.Gaming.Service.GamePlay
{ 
    public class RewardItem
    {
        public long rewardId { get; set; }
        public string rewardTitle { get; set; }
        public long rewardValue { get; set; }
        public string couponCode { get; set; }
        public string type { get; set; }
    }
}
