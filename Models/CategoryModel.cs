using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.api.Models
{
    [Table("categoryMaster")]
    public class CategoryModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string categoryName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string categoryLogoUrl { get; set; }
    }
}
