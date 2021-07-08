using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Blaash.Gaming.Infrastructre.Common
{
    public class EventTrackModel
    {
   
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Invalid event_name, event_name min length 5 max 20")]
        public string event_name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid user_id, user_id min length 2 max 5")]
        public string user_id { get; set; }
        public long tenant_id { get; set; }
        public string sdk_version { get; set; }
        public string ip { get; set; }
        public string user_agent { get; set; }
        public JObject data { get; set; } = new JObject();        
        public bool is_processed { get; set; } = false;


    }

}
