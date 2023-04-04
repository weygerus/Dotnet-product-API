using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Domain
{
    public class ProductLog
    {
        private Func<object, object> value;

        public int Id { get; set; }
        public int ProductId {get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? ProductJson { get; set; }


        public ProductLog(int id, int productId, DateTime updatedAt, string? productJson)
        {
            Id = id;
            ProductId = productId;
            UpdatedAt = updatedAt;
            ProductJson = productJson;
        }

        public ProductLog(Func<object, object> value)
        {
            this.value = value;
        }
    }
}
