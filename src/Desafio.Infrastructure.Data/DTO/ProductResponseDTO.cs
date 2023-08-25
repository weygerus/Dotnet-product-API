namespace Desafio.Infrastructure.Data.DTO
{
    public class ProductResponseDTO
    {
        /// <summary>
        /// Gets or Sets da propriedade Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets da propriedade Message.
        /// </summary>
        public string? Message { get; set; }

         /// <summary>
         /// 
         /// </summary>
        public List<string>? ProductCategories { get; set; } 
    }
}