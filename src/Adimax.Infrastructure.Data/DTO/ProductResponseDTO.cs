using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Infrastructure.Data.DTO
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public bool HasPending { get; set; }

        public ProductResponseDTO(int id, string message, bool hasPensding)
        {
            Id = id;
            Message = message;
            HasPending = hasPensding;
        }
    }
}
