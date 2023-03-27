using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Domain
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
        /// Gets or Sets dos produtos.
        /// </summary>
        public ICollection<Product> Products { get; set; }

        /// <summary>
        /// Gets or Sets da criação.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or Sets da atualização.
        /// </summary>
        public DateTime UpdateAt { get; set; }
    }
}
