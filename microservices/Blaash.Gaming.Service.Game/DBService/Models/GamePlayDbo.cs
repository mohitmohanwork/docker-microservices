using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blaash.Gaming.Service.GamePlay.DBService.Models
{
    [Table(GamePlayTables.GAME_PLAY)]
    public class GamePlayDbo
    {
        [Dapper.Contrib.Extensions.Key]
        public long game_play_id { set; get; }
        public string game_session_id { set; get; }
        public long game_id { set; get; }
        public long player_id { set; get; }
        public long engagement_id { set; get; }
        public bool is_cancelled { set; get; }
        public long external_session_id { set; get; }
        public long final_score { set; get; }
        public DateTime game_session_start { set; get; }
        public DateTime? game_session_end { set; get; }
        public DateTime? game_exit_datetime { set; get; }
    }
}
