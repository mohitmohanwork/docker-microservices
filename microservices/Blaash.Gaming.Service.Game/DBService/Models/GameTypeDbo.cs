using System.ComponentModel.DataAnnotations.Schema;

namespace Blaash.Gaming.Service.GamePlay.DBService.Models
{
    [Table(GamePlayTables.GAME_TYPE)]
    public class GameTypeDbo
    {
        [Dapper.Contrib.Extensions.Key]
        public long game_type_id { set; get; }
        public string game_type { set; get; }
    }
}
