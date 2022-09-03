using System;
using AspNetCore.Identity.MongoDbCore.Models;

namespace Core.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> InsertAsync(T entity);
        Task<bool> RemoveAsync(string id);
        T Update(T entity);

    }
}

