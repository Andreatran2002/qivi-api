using System;
using Core.Base;
using Core.Entities;
using Core.Repositories;
using HotChocolate.Subscriptions;

namespace Api.Mutations
{
    [ExtendObjectType(nameof(Mutation))]
	public class OrderDetailsMutation
	{
		public OrderDetailsMutation()
		{
		}
        public async Task<AppResponse<OrderDetails>> CreateOrderDetailsAsync(string userId, 

            [Service] IOrderDetailsRepository orderDetailsRepository, [Service] ITopicEventSender eventSender)
        {
            try
            {
                    var result = await orderDetailsRepository.InsertAsync(new OrderDetails(userId));
                    return new AppResponse<OrderDetails>(result);

            }
            catch (Exception e)
            {
                return new AppResponse<OrderDetails>("undefined-error");
            }
            

        }
        public async Task<AppResponse<OrderDetails>> UpdateOrderDetailsAsync(OrderDetails order,

            [Service] IOrderDetailsRepository orderDetailsRepository, [Service] ITopicEventSender eventSender)
        {
            try
            {
                var orderInDB = await orderDetailsRepository.GetByIdAsync(order.Id);
                if (orderInDB != null)
                {
                    return new AppResponse<OrderDetails>(orderDetailsRepository.Update(order));

                }
                return new AppResponse<OrderDetails>("order-details-not-available");
            }
            catch (Exception e)
            {
                return new AppResponse<OrderDetails>("undefined-error");
            }


        }
        public async Task<AppResponse<bool>> RemoveOrderDetailsAsync(string id,

            [Service] IOrderDetailsRepository orderDetailsRepository ,[Service] ITopicEventSender eventSender)
        {
            try
            {
                var orderInDB = await orderDetailsRepository.GetByIdAsync(id);
                if (orderInDB != null)
                {
                    return new AppResponse<bool>(await orderDetailsRepository.RemoveAsync(id)); 
                }else
                return new AppResponse<bool>(false);
            }
            catch (Exception e)
            {
                return new AppResponse<bool>("undefined-error");
            }
            
        }
        
    }
}

