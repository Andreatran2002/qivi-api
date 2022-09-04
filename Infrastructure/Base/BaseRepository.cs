using System;
using AspNetCore.Identity.MongoDbCore.Models;
using Core.Base;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> collection;

        public BaseRepository(ICatalogContext catalogContext)
        {
            if (catalogContext == null)
            {
                throw new ArgumentNullException(nameof(catalogContext));
            }

            this.collection = catalogContext.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.collection.Find(_ => true).ToListAsync();
        }

        

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(_ => _.Id, id);

            return await this.collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await this.collection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var result = await this.collection.DeleteOneAsync(Builders<T>.Filter.Eq(_ => _.Id, id));

            return result.DeletedCount > 0;
        }

        public T Update(T entity)
        {
            var document = collection.Find(x => x.Id == entity.Id).FirstOrDefault();
            if (document == null)
                throw new Exception("This document doesn't exists in database!");
            document.Update(entity);
            collection.ReplaceOne(x => x.Id == entity.Id, document);
            return document;
        }

        
    }
}


