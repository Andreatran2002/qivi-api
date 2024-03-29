﻿using System;
using Core.Base;
using Core.Entities;

namespace Core.Repositories
{
	public interface IOrderDetailsRepository :IBaseRepository<OrderDetails>
	{
		public Task<OrderDetails> GetByUserId(string userId);
	}
}

