using System;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Base;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class UserRepository :  IUserRepository
    {
        private readonly IMongoCollection<ApplicationUser> collection;
        public UserRepository(ICatalogContext catalogContext) 
        {
            collection = catalogContext.GetCollection<ApplicationUser>("ApplicationUser");

        }
        public async Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber)
        => await collection.Find(u => u.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
        public async Task<ApplicationUser> GetUserByName(string name)
        => await collection.Find(u => u.UserName == name).FirstOrDefaultAsync();

        public async Task<bool> AccountInfoIsAvailable(string name, string phoneNumber)
        {
            bool isNameAvailable = (await GetUserByName(name)) != null;
            bool isPhoneNumberAvailable = (await GetUserByPhoneNumber(phoneNumber)) != null;

            return (isNameAvailable || isPhoneNumberAvailable);
        }
        public List<ApplicationUser> GetPossibleUserName(string possibleUsername)
            => collection.Find(u => u.UserName.ToLower().StartsWith(possibleUsername)).ToList();

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await collection.Find((item) => item.Id.ToString() == id).FirstOrDefaultAsync();

        }
    }
}

