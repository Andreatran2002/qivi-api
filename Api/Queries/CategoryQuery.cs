using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Queries
{
	[ExtendObjectType(nameof(Query))]
	public class CategoryQuery
	{
		public CategoryQuery()
		{
		}
		public Task<IEnumerable<Category>> GetCategoryAsync([Service] ICategoryRepository categoryRepository) =>
			categoryRepository.GetAllAsync();

		public Task<Category> GetCategoryById(string id, [Service] ICategoryRepository categoryRepository) =>
			categoryRepository.GetByCategoryId(id);
	}
}

