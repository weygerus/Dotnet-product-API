using Adimax.Infrastructure.Data;
using Adimax.API.Controllers;

using Hangfire;
using Adimax.Domain;
using Adimax.Infrastructure.Data.Contract.Interfaces;
using Adimax.Infrastructure.Data.Contract.Repositories;

namespace Adimax.API.Services
{
    public class HangfireTrigger
    {
        private readonly IRecurringJobManager _jobManager;
        private readonly IProductRepository _productRepository;
        private readonly ProductLogController _productLogController;
        private readonly DatabaseContext _dbContext;

        public HangfireTrigger
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
        //public async Product PopulateProductLogTable(Product product)
        //{
        //    if(_dbContext.Products.FindAsync(p=>p.HasPendingLogUpdate == true))
        //    {
        //        await _productLogController.AddProductLog(product);
        //    }
        //}

        public void fake()
        {

        } 

        public void Trigger()
        {
            _jobManager.AddOrUpdate("AddProductLog", () => fake(), Cron.Minutely());
        }
    }
}
