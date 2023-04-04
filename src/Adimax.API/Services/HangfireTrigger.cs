using Adimax.API;
using Adimax.Domain;
using Adimax.Infrastructure.Data;
using Adimax.Infrastructure.Data.Contract.Interfaces;
using Hangfire;

namespace Adimax.API.Services
{
    public class HangfireTrigger
    {
        private readonly IRecurringJobManager _jobManager;
        private readonly IProductRepository _productRepository;
        private readonly DatabaseContext _dbContext;
        public HangfireTrigger(IRecurringJobManager jobManager, IProductRepository productRepository, DatabaseContext databaseContext)
        {
            _jobManager = jobManager;
            _productRepository = productRepository;
            _dbContext = databaseContext;
        }

        public void Trigger()
        {
            _jobManager.AddOrUpdate("PopulateProductLog", () => PopulateProductLog(), "*/ 1 * ***");
        }

        public void PopulateProductLog()
        {
            var ProductsForUpdate = _dbContext.Products.Where(p => p.HasPendingLogUpdate);

            foreach (var Product in ProductsForUpdate)
            {
                var productLog = new ProductLog(Product.Id,Product.Id,DateTime.Now,"Ok");

                _dbContext.ProductLogs.Add(productLog);
                _dbContext.SaveChanges();
            }
        }
    }
}
