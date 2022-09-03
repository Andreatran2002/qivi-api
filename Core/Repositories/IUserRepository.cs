using System;
using Core.Base;
using Core.Entities;

namespace Core.Repositories
{
    public interface IUserRepository 
    {

        public Task<List<ApplicationUser>> GetAllAsync();
        public Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber);
        public Task<ApplicationUser> GetUserByName(string name);
        public Task<bool> AccountInfoIsAvailable(string name, string phoneNumber);
        public List<ApplicationUser> GetPossibleUserName(string possibleUsername);
        public Task<ApplicationUser> GetByIdAsync(string id);

    }
}

