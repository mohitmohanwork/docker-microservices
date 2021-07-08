using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blaash.Gaming.Service.GamePlay.DBService.Models
{
    [Table(GamePlayTables.PLAYER_POINTS_BALANCE)]
    public class PlayerPointsBalanceDbo
    {
        [Dapper.Contrib.Extensions.Key]
        public long player_balance_id { set; get; }
        public long tenant_id { set; get; }
        public long player_id { set; get; }
        public long activity_type_id { set; get; }
        public string activity_name { set; get; }
        public long value { set; get; }
        public DateTime added_on { set; get; }

    }
}
