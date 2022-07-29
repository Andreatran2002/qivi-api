using System;
using Core.Entities;
using Infrastructure.Configurations;
using Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class CatalogContext : ICatalogContext  
    {
        private readonly IMongoDatabase database;

        public CatalogContext(IOptions<MongoDbConfiguration> dbOptions)
        {
            var settings = dbOptions.Value;
            var client = new MongoClient(settings.ConnectionString);

            this.database = client.GetDatabase(settings.Database);

            //CatalogContextSeed.SeedData(this.database);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return this.database.GetCollection<T>(name);
        }


    }
}

