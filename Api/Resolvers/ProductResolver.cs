using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Resolvers
{

    [ExtendObjectType(typeof(Product))]
    public class ProductResolver
    {
       
        
        public Task<Product> GetProductAsync(
         [Parent] ProductPrice price,
         [Service] IProductRepository productRepository) => productRepository.GetByIdAsync(price.ProductId);


    }

}

