using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace Adimax.Domain
{
    public class ProductCategory
    {
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

 
        /// <summary>
        /// [SwaggerIgnore]
        /// </summary>
        public Product ProductIn { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public Category CategoryIn { get; set; }
    }
}
