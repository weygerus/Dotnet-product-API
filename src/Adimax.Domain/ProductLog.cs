using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Domain
{
    public class ProductLog
    {
        public int Id { get; set; }
        public int ProductId {get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ProductJson { get; set; }
    }
}
