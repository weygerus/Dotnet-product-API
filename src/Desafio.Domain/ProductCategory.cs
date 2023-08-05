using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Domain
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public Product ProductIn { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public Category CategoryIn { get; set; }
    }
}
