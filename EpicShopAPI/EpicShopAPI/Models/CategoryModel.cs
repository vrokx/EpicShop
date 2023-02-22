using System.ComponentModel.DataAnnotations;

namespace EpicShopAPI.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
