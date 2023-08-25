using Desafio.Infrastructure.Data.Contract.Jobs;
using System.Text.Json;
using Desafio.Domain;

namespace Desafio.Infrastructure.Data.Jobs
{
    public class HangfireService : IHangfireService
    {
        private readonly DatabaseContext _dbContext;

        public HangfireService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public void PopulateProductLogTableJob()
        {
            var ProductsForUpdate = _dbContext.Products.Where(p => p.HasPendingLogUpdate == true).ToList();

            foreach (var product in ProductsForUpdate)
            {
                product.HasPendingLogUpdate = false;

                var productLog = new ProductLog()
                {
                    ProductId = product.Id,
                    UpdatedAt = DateTime.Now,
                    ProductJson = JsonSerializer.Serialize(product)
                };

               _dbContext.ProductLogs.Add(productLog);
               _dbContext.SaveChanges();
            }
        }
    }
}