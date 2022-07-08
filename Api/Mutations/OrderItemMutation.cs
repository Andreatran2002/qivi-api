using System;
using Core.Entities;
using Core.Repositories;
using HotChocolate.Subscriptions;

namespace Api.Mutations
{
    [ExtendObjectType(nameof(Mutation))]

    public class OrderItemMutation
	{
        private readonly ILogger<OrderItemMutation> _logger;
        public OrderItemMutation(ILogger<OrderItemMutation> logger)
        {
            _logger = logger;
        }

        public async Task<OrderItem> CreateOrderItemAsync(string priceId, string orderId, int quantity,

            [Service] IOrderItemRepository orderItemRepository, [Service] IOrderDetailsRepository orderRepository, [Service] IProductPriceRepository priceRepository, [Service] ITopicEventSender eventSender)
        {
            try
            {
                OrderDetails order = await orderRepository.GetByIdAsync(orderId);
                var price = await priceRepository.GetByIdAsync(priceId);
               
                var result = await orderItemRepository.InsertAsync(new OrderItem(orderId, price.Id, quantity));
                order.ModifiedAt = DateTime.Now;
                order.Total = order.Total + quantity * price.Price;
                orderRepository.Update(order);
                return result;
            }catch(Exception e)
            {
                _logger.LogError("Create order item false . ERROR : {error}  ", e.ToString());
                return null;
            }
            
        }
        public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem,

            [Service] IOrderItemRepository orderItemRepository, [Service] IOrderDetailsRepository orderRepository,
            [Service] IProductPriceRepository priceRepository, [Service] ITopicEventSender eventSender)
        {
            try
            {
                OrderItem orderInDB = await orderItemRepository.GetByIdAsync(orderItem.Id);
                OrderDetails order = await orderRepository.GetByIdAsync(orderItem.OrderId);

                var price = await priceRepository.GetByIdAsync(orderItem.PriceId);
               
                order.ModifiedAt = DateTime.Now;
                order.Total = order.Total + (orderItem.Quantity - orderInDB.Quantity) * price.Price;
                orderRepository.Update(order);
                _logger.LogInformation("Update order item {id} ", orderItem.Id);
                var result = orderItemRepository.Update(orderItem);
                return result;


            }
            catch (Exception e)
            {
                _logger.LogError("Update order item {id} false ", orderItem.Id);
                _logger.LogError(e.ToString());
                return null;
            }

        }
        public async Task<OrderItem?> RemoveCartItemAsync(string id,

            [Service] IOrderItemRepository orderItemRepository, [Service] IOrderDetailsRepository orderRepository,
            [Service] IProductPriceRepository priceRepository, [Service] ITopicEventSender eventSender)
        {
            try
            {
                var orderItem = await orderItemRepository.GetByIdAsync(id);
                var price = await priceRepository.GetByIdAsync(orderItem.PriceId);
                var result = await orderItemRepository.RemoveAsync(id);
                OrderDetails order = await orderRepository.GetByIdAsync(orderItem.OrderId);

                order.ModifiedAt = DateTime.Now;
                order.Total = order.Total - orderItem.Quantity * price.Price;
                orderRepository.Update(order);
                _logger.LogInformation("Remove order item {id} ", id);
                return orderItem;
            }
            catch (Exception e)
            {
                _logger.LogError("Remove order item {id} false . Cart Item {id} does not exist ", id);
                _logger.LogError(e.ToString());
                return null;
            }
           


        }

    }
}

