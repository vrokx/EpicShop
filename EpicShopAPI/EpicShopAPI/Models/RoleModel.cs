using System.ComponentModel.DataAnnotations;

namespace EpicShopAPI.Models
{
    public class RoleModel
    {
        [Key]
        public int UserTypeId { get; set; }

        [Required]
        public string UserTypeName { get; set; }
    }
}
