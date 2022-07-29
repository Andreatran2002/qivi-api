using System;
using Bogus;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoDatabase database)
        {
            InsertCategoriesAndProduct(database.GetCollection<Category>(nameof(Category)), database.GetCollection<Product>(nameof(Product)), database.GetCollection<ProductPrice>(nameof(ProductPrice)));
            //InsertProducts(database.GetCollection<Product>(nameof(Product)));
        }

        private static void InsertCategories(IMongoCollection<Category> categoryCollection)
        {
            var categories = new List<Category>(); 
            var category = new Faker<Category>()
                .RuleFor(a => a.Name, f => f.Random.Word())
                .RuleFor(a => a.Description, f => f.Lorem.Paragraph(1))
                .RuleFor(a => a.CategoryId, f => f.Random.AlphaNumeric(6))
                .RuleFor(a => a.ParentCategory, f => "");

            for (int i = 0; i< 20; i++)
            {
                categories.Add(category.Generate()); 
            }
            categoryCollection.InsertMany(categories);
        }

        private static async void InsertCategoriesAndProduct(IMongoCollection<Category> categoryCollection, IMongoCollection<Product> productCollection, IMongoCollection<ProductPrice> priceCollection)
        {
            categoryCollection.DeleteMany(_ => true);
            productCollection.DeleteMany(_ => true);
            priceCollection.DeleteMany(_ => true);

            System.Random random = new System.Random();
            
            var products = new List<Product>();
            var prices = new List<ProductPrice>();
            var categories = new List<Category>();

            var productFake = new Faker<Product>()
                .RuleFor(a => a.Name, f => f.Commerce.ProductName())
                .RuleFor(a => a.Description, f => f.Commerce.ProductDescription())
                .RuleFor(a => a.Image, f => "https://firebasestorage.googleapis.com/v0/b/tinevyland.appspot.com/o/avatar%2Fdefault-avatar.png?alt=media&token=57c2019d-3687-4984-9bb4-42a7c30dea8");

            var categoryFake = new Faker<Category>()
                .RuleFor(a => a.Name, f => f.Random.Word())
                .RuleFor(a => a.Description, f => f.Lorem.Paragraph(1))
                .RuleFor(a => a.CategoryId, f => f.Random.AlphaNumeric(6));

            var priceFake = new Faker<ProductPrice>()
                .RuleFor(a => a.Price, f => f.Commerce.Price(1).First())
                .RuleFor(a => a.SKU, f => f.Commerce.ProductAdjective()); 


            int randomNum = 0;
            Category category; 
            Product product;
            ProductPrice price;
            for (int i = 0; i < 20; i++)
            {
                category = categoryFake.Generate();
                category.ParentCategory = category.CategoryId;
                randomNum = random.Next(50);

                for (int j = 0; j < randomNum; j++)
                {
                    product = productFake.Generate();
                    product.Id = ObjectId.GenerateNewId().ToString();
                    product.CategoryId = category.CategoryId;
                   
                    for (int k = 0; k < 3;k++)
                    {
                        price = priceFake.Generate();
                        price.ProductId = product.Id;
                        prices.Add(price);

                    }
                    products.Add(product);

                }
                categories.Add(category);

            }
            categoryCollection.InsertMany(categories);
            productCollection.InsertMany(products);
            priceCollection.InsertMany(prices);

        }
    }

}

