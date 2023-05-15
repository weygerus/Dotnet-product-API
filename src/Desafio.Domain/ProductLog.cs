namespace Desafio.Domain
{
    public class ProductLog
    {
        public int Id { get; set; }
        public int ProductId {get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? ProductJson { get; set; }
    }
}
