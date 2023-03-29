using Adimax.Infrastructure.Data;
using Adimax.API.Controllers;

using Hangfire;
using Adimax.Domain;
using Adimax.Infrastructure.Data.Contract.Interfaces;
using Adimax.Infrastructure.Data.Contract.Repositories;

namespace Adimax.API.Services
{
    public class HangfireService
    {
        private readonly IRecurringJobManager _jobManager;
        private readonly IProductRepository _productRepository;
        private readonly ProductLogController _productLogController;
        private readonly DatabaseContext _dbContext;

        public HangfireService
            (
            IRecurringJobManager jobManager,
            IProductRepository productRepository,
            ProductLogController productLogController,
            DatabaseContext context
            )
        {
            _jobManager = jobManager;
            _productLogController = productLogController;
            _productRepository = productRepository;
            _dbContext = context;
        }

        public Product PopulateProductLogTable(Product product)
        {
            product = _productRepository.AddAsync(product);

            if ( _dbContext.Products.FindAsync(product) == null)
            {
               _productLogController.AddProductLog(product);
            }

            var res = new Product();
            return res;
        }

        public void HangfireTrigger()
        {
            _jobManager.AddOrUpdate("PopulateProductLogTable", () => PopulateProductLogTable(product), Cron.Minutely());
        }
    }
}
