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

        public async Task<OrderItem> CreateOrderItemAsync(string productId, string orderId, int quantity,

            [Service] IOrderItemRepository orderItemRepository, [Service] IOrderDetailsRepository orderRepository, [Service] IProductRepository productRepository, [Service] ITopicEventSender eventSender)
        {
            OrderDetails order = await orderRepository.GetByIdAsync(orderId);
            Product product = await productRepository.GetByIdAsync(productId);
            if (order == null || product == null )
            {
                _logger.LogError("Create order item false . Product {productId}  or orderId {orderId} does not exist  ", productId,  orderId);
                return null;
            }
            var result = await orderItemRepository.InsertAsync(new OrderItem(orderId, productId, quantity));
            order.ModifiedAt = DateTime.Now;
            order.Total = order.Total + quantity * product.Price;
            orderRepository.Update(order);
            return result;
        }
        public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem,

            [Service] IOrderItemRepository orderItemRepository, [Service] IOrderDetailsRepository orderRepository,
            [Service] IProductRepository productRepository, [Service] ITopicEventSender eventSender)
        {
            try
            {
                OrderItem orderInDB = await orderItemRepository.GetByIdAsync(orderItem.Id);
                OrderDetails order = await orderRepository.GetByIdAsync(orderItem.OrderId);

                try
                {
                    var product = await productRepository.GetByIdAsync(orderItem.ProductId);
                    if (product != null)
                    {
                        order.ModifiedAt = DateTime.Now;
                        order.Total = order.Total + (orderItem.Quantity - orderInDB.Quantity) * product.Price;
                        orderRepository.Update(order);
                        _logger.LogInformation("Update order item {id} ", orderItem.Id);
                        var result = orderItemRepository.Update(orderItem);
                        return result;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("Update cart item {id} false . Product {productId} does not exist ", orderItem.Id, orderItem.ProductId);
                    _logger.LogError(e.ToString());
                    return null;

                }


            }
            catch (Exception e)
            {
                _logger.LogError("Update order item {id} false . Order Item {id} does not exist ", orderItem.Id);
                _logger.LogError(e.ToString());

                return null;
            }
            return null;

        }
        public async Task<OrderItem?> RemoveCartItemAsync(string id,

            [Service] IOrderItemRepository orderItemRepository, [Service] IOrderDetailsRepository orderRepository,
            [Service] IProductRepository productRepository, [Service] ITopicEventSender eventSender)
        {
            var orderItem = await orderItemRepository.GetByIdAsync(id);
            if (orderItem != null)
            {
                var product = await productRepository.GetByIdAsync(orderItem.ProductId);
                if (product != null)
                {
                    var result = await orderItemRepository.RemoveAsync(id);
                    OrderDetails order = await orderRepository.GetByIdAsync(orderItem.OrderId);

                    order.ModifiedAt = DateTime.Now;
                    order.Total = order.Total - orderItem.Quantity * product.Price;
                    orderRepository.Update(order);
                    _logger.LogInformation("Remove order item {id} ", id);
                    return orderItem;
                }
                else
                {
                    _logger.LogError("Remove order item {id} false . Product {productId} does not exist ", id, orderItem.ProductId);
                    return null;
                }
            }
            else
            {
                _logger.LogError("Remove order item {id} false . Cart Item {id} does not exist ", id);
                return null;
            }


        }

    }
}

