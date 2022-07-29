using System;
using Core.Entities;
using Core.Repositories;
using HotChocolate;
using HotChocolate.Types;

namespace Api.Resolvers
{

    [ExtendObjectType(typeof(Category))]
    public class CategoryResolver
    {
        public Task<Category?> GetCategoryAsync(
          [Parent] Product product,
          [Service] ICategoryRepository categoryRepository) => categoryRepository.GetByCategoryId(product.CategoryId);
        public Task<Category?> GetParentCategoryAsync(
         [Parent] Category category,
         [Service] ICategoryRepository categoryRepository) => categoryRepository.GetByCategoryId(category.ParentCategory);
    }
}

