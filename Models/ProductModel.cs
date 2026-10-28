using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.api.Models
{
    [Table("productMaster")]
    public class ProductModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }
        public string productName { get; set; } = String.Empty;
        public double productPrice { get; set; }
        public int productCategoryId { get; set; }

        public string productDescription { get; set; } = String.Empty;

        public bool isAvailable { get; set; }
        public DateTime createdDate { get; set; }
    }

    // new code added
    public class ProductModelList
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }
        public int productCategoryId { get; set; }
        public string productDescription { get; set; }
        public bool isAvailable { get; set; }
        public DateTime createdDate { get; set; }
        public string categoryName { get; set; }
    }
}
