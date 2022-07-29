using System;
using Core.Base;

namespace Core.Entities
{
	public class ProductPrice : BaseEntity
	{
		public ProductPrice()
        {

        }
		public ProductPrice( string sku , decimal price , string productId)
		{
			SKU = sku;
			Price = price;
			ProductId = productId;
		}
		public string SKU { set; get; }
		public decimal Price { get; set; } 
		public string ProductId { get; set; }

	}
}

