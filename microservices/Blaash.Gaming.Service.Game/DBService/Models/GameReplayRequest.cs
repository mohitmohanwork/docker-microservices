using System;
using System.Collections.Generic;
using System.Text;

namespace Blaash.Gaming.Service.GamePlay.DBService.Models
{
    public class GameReplayRequest
    {
        public int GameID { get; set; }
        public string GameSessionID { get; set; }
        public long EngagementID { get; set; }
    }
}
