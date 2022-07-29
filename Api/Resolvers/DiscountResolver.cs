using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Resolvers
{

    [ExtendObjectType(typeof(Discount))]
    public class DiscountResolver
    {
        public Task<Discount?> GetDiscountByProductAsync(
          [Parent] Product product,
          [Service] IDiscountRepository discountRepository) => discountRepository.GetByIdAsync(product.DiscountId);


    }
}

