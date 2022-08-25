using System;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Base;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		private readonly IMongoCollection<User> collection; 
		public UserRepository(ICatalogContext catalogContext) : base(catalogContext)
		{
			collection = catalogContext.GetCollection<User>("User"); 

		}
		public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
		=> await collection.Find(u => u.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
		public async Task<User?> GetUserByName(string name)
		=> await collection.Find(u => u.UserName == name).FirstOrDefaultAsync();

        public async Task<bool> AccountInfoIsAvailable(string name, string phoneNumber)
        {
			bool isNameAvailable = (await GetUserByName(name)) != null;
			bool isPhoneNumberAvailable = (await GetUserByPhoneNumber(phoneNumber)) != null;

			return (isNameAvailable || isPhoneNumberAvailable);
        }
        public async Task<List<User>?> GetPossibleUserName(string possibleUsername)
			=> await collection.Find(u => u.UserName.StartsWith(possibleUsername)).ToListAsync();
    }
}

