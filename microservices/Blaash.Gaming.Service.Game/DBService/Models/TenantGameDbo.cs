using Blaash.Gaming.Infrastructre.Common;
using Blaash.Gaming.Service.GamePlay;
using Dapper.Contrib.Extensions;
using ZNxt.Net.Core.Model;

namespace Blaash.Gaming.Service.GamePlay.DBService.Models
{

    [Table(GamePlayTables.TENANT_GAMES)]
    public class TenantGameDbo : BaseModelDbo
    {
        [Dapper.Contrib.Extensions.Key]
        public long tenant_games_id { get; set; }

        public long game_id { get; set; }

        public long tenant_id { get; set; }

        public bool is_active { get; set; } = true;


    }
}
