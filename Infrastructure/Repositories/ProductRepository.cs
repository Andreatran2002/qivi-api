using System;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Base;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Interfaces
{
    
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly IMongoCollection<Product> collection; 
        public ProductRepository(ICatalogContext catalogContext) : base(catalogContext)
        {
            collection = catalogContext.GetCollection<Product>("Product");
        }

        public async Task<IEnumerable<Product>> GetByCategoryId(string id)
        {
            return await collection.Find(a => a.CategoryId == id).ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetByDiscountId(string id)
        {
            return await collection.Find(a => a.DiscountId == id).ToListAsync();   
        }
    }


}

