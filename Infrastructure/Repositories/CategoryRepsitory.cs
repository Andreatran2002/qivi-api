using System;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Base;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly IMongoCollection<Category> collection;

       
        public CategoryRepository(ICatalogContext catalogContext) : base(catalogContext)
        {
            if (catalogContext == null)
            {
                throw new ArgumentNullException(nameof(catalogContext));
            }

            collection = catalogContext.GetCollection<Category>("Category");

        }

        public async Task<Category> GetByCategoryId(string id)
        {
            return await collection.Find(a => a.CategoryId == id).FirstOrDefaultAsync();

        }
        
    }
}

