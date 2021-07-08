using System.ComponentModel.DataAnnotations;
namespace Blaash.Gaming.Infrastructre.Common
{
    public class CreateUserModel
    {

        [Required]
        public long tenant_id { get; set; }
        
        public string user_id{ get; set; }
        [Required]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "user name min length 5 max length 40")]
        public string user_name { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "First name min length 3 max length 150")]
        public string first_name { get; set; }

        public string last_name { get; set; }

        public string middle_name { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Email min length 3 max length 150")]
        public string email { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile number 10 max length 10")]
        public string mobile_number { get; set; }

    }
}
