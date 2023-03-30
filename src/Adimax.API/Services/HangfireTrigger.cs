using Hangfire;
using Adimax.Domain;
using Adimax.API.Controllers;
using Adimax.Infrastructure.Data;
using Adimax.Infrastructure.Data.Contract.Interfaces;

namespace Adimax.API.Services
{
    public class HangfireService
    {
        private readonly IRecurringJobManager _jobManager;
        private readonly IProductRepository _productRepository;
        private readonly DatabaseContext _dbContext;

        public HangfireService
            (
            IRecurringJobManager jobManager,
            IProductRepository productRepository,
            DatabaseContext dbContext
            )
        {
            _jobManager = jobManager;
            _productRepository = productRepository;
            _dbContext = dbContext;
        }

        public void HangfireTrigger(Product product)
        {
            var productLog = _productRepository.AddAsync(product);

            _jobManager.AddOrUpdate("PopulateProductLogTable", () => PopulateProductLogTable(productLog), Cron.Minutely());
        }

        public Product PopulateProductLogTable(Product product)
        {
            if (_dbContext.Products.FindAsync(product.Id) == null)
            {
                throw new Exception("Sem produtos");
            }

            ProductLog productLog = new ProductLog(product.Id,product.Id,DateTime.Now,"");
            _dbContext.ProductLogs.Add(productLog);
            _dbContext.SaveChanges();

            var res = new Product();
            return res;
        }
    }
}
