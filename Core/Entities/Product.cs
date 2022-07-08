using System;
using Core.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities
{
    
    public class Product : BaseEntity
    {

        public Product(string name,string description, string categoryId,string image )
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
            Image = image;
        }
        public string Name { get; set; }
        public string Description { get; set; }
      
        public string CategoryId { get; set; } 
        public string? DiscountId { set; get; } = ""; 
        public DateTime CreatedAt { set; get; } = DateTime.Now;
        public DateTime ModifiedAt { set; get; } = DateTime.Now; 
        public bool IsEmpty { get; set; } = false; 
        public string Image { set; get; } = "";
       
    }

}


