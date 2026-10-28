using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.api.Models
{
    [Table("productImageMaster")]
    public class ProductImageModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productImageId { get; set; }
        public string imageUrl { get; set; } = string.Empty;
        public bool isThumbnailImage { get; set; }
        public int productId { get; set; }

    }
}
