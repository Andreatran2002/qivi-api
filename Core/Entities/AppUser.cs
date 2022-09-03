using System;
using AspNetCore.Identity.MongoDbCore.Models;
using Core.Base;
using MongoDbGenericRepository.Attributes;

namespace Core.Entities
{
    [CollectionName("ApplicationUser")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public string FullName { set; get; }

    }
}

