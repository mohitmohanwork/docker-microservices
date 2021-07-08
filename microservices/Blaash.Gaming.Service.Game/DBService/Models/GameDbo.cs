using Blaash.Gaming.Infrastructre.Common;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;
using ZNxt.Net.Core.Model;

namespace Blaash.Gaming.Service.GamePlay.DBService.Models
{

    [Table(GamePlayTables.GAME)]
    public class GameDbo : BaseModelDbo
    {
        [Dapper.Contrib.Extensions.Key]
        public long game_id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Game Name min length 5 max length 150")]
        public string name { get; set; }

        [Required]
        public GameType game_type { get; set; }

        public string description { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "game_url  min length 2 max length 200")]
        public string game_url { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "banner_image_url  min length 2 max length 200")]
        public string banner_image_url { get; set; }

        public bool is_active { get; set; } = true;

        public bool is_external { get; set; } = false;

        public long game_play_rule { get; set; } = 0;

    }
}
