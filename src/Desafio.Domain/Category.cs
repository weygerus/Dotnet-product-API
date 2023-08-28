using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Desafio.Domain
{
    public class Category
    {
        /// <summary>
        /// Gets or Sets do id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets do nome.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets da descrição.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets da relação entre categorias/produtos.
        /// </summary>
        [JsonIgnore]
        public ICollection<ProductCategory>? ProductCategories { get; set; }

        /// <summary>
        /// Gets or Sets dos produtos contidos na categoria.
        /// </summary>
        [NotMapped]
        public List<string>? ProductsOnCategory { get; set; }

        /// <summary>
        /// Gets or Sets da criação.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or Sets da atualização.
        /// </summary>
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}
