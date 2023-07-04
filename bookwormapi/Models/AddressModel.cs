using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookwormapi.Models
{
    public class AddressModel
    {
        [Key]
        public int AddressId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public UserModel User { get; set; }

    }
}
