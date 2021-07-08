using System;
namespace Blaash.Gaming.Service
{
    public class GameLaunchRequest
    {
        public long GameID { get; set; }

        public long EngagementID { get; set; }

        public long PlayerID { get; set; }
        public long TenantID { get; set; }
    }
}
