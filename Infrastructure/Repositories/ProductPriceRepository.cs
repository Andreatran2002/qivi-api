using System;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Base;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class ProductPriceRepository : BaseRepository<ProductPrice>, IProductPriceRepository
    {
        private readonly IMongoCollection<ProductPrice> collection;

        public ProductPriceRepository(ICatalogContext catalogContext) : base(catalogContext)
        {
            collection = catalogContext.GetCollection<ProductPrice>("ProductPrice");
        }

        public async Task<IEnumerable<ProductPrice>> GetByProductId(string id)
        {
            return await collection.Find((a) => a.ProductId == id).ToListAsync(); 
        }
    }
}

