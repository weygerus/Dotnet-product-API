using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace Adimax.Domain
{
    public class Product
    {
        /// <summary>
        /// Gets or Sets do Id.
        /// </summary>
        //[SwaggerIgnore]
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
        /// Gets or Sets do preço.
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Gets or Sets do objeto da categoria.
        /// </summary>
        ///[SwggerIgnore]
        public ICollection<Category> CategoryObject { get; set; }

        /// <summary>
        /// Gets or Sets de categoria.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or Sets da data de criação.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or Sets do (identificar campo).
        /// </summary>
        public string HasPendingLogUpdate { get; set; }
    }
}
