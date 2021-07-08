using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blaash.Gaming.Service.GamePlay.DBService.Models
{
    [Table(GamePlayTables.GAME_SCORE)]
    public class GameScoreDbo
    {
        [Dapper.Contrib.Extensions.Key]
        public long game_score_id { set; get; }
        public long game_id { set; get; }
        public long player_id { set; get; }
        public long tenant_id { set; get; }
        public int current_score { set; get; }
        public int current_level { set; get; }
        public DateTime last_played_at { set; get; }
    }
}
