using System;
using Core.Entities;
using Core.Repositories;
using HotChocolate.Subscriptions;

namespace Api.Mutations
{
    [ExtendObjectType(nameof(Mutation))]


    public class CartItemMutation
    {
        private readonly ILogger<CartItemMutation> _logger;
        public CartItemMutation(ILogger<CartItemMutation> logger)
        {
            _logger = logger;
        }

        public async Task<CartItem> CreateCartItemAsync(string priceId, int quantity, string sessionId,

            [Service] ICartItemRepository cartItemRepository, [Service] IShoppingSessionRepository sessionRepository, [Service] IUserRepository userRepository, [Service] IProductPriceRepository productPriceRepository, [Service] ITopicEventSender eventSender)
        {
            try
            {
                ShoppingSession session = await sessionRepository.GetByIdAsync(sessionId);
                ProductPrice price = await productPriceRepository.GetByIdAsync(priceId);

                var result = await cartItemRepository.InsertAsync(new CartItem(price.Id, quantity, sessionId));
                session.ModifiedAt = DateTime.Now;
                session.Total = session.Total + quantity * price.Price;
                sessionRepository.Update(session);
                return result;
            }
            catch(Exception e)
            {
                _logger.LogError("Create cart item false . ERROR : " + e);
                return null;
            }
           
        }
        public async Task<CartItem> UpdateCartItemAsync(CartItem cart,

            [Service] ICartItemRepository cartItemRepository, [Service] IShoppingSessionRepository sessionRepository,
            [Service] IProductPriceRepository productPriceRepository, [Service] ITopicEventSender eventSender)
        {
            try
            {
                CartItem cartInDB = await cartItemRepository.GetByIdAsync(cart.Id);
                ShoppingSession session = await sessionRepository.GetByIdAsync(cart.SessionId);
               
                var price = await productPriceRepository.GetByIdAsync(cart.PriceId);
                if (price != null)
                {
                    session.ModifiedAt = DateTime.Now;
                    session.Total = session.Total + (cart.Quantity - cartInDB.Quantity) * price.Price;
                    sessionRepository.Update(session);
                    _logger.LogInformation("Update cart item {id} ", cart.Id);
                    var result = cartItemRepository.Update(cart);
                    return result;
                }
                return null; 

            }
            catch (Exception e)
            {
                _logger.LogError("Update cart item {id} false . Cart Item {id} does not exist ", cart.Id);
                _logger.LogError(e.ToString());

                return null;
            }

        }
        public async Task<CartItem> RemoveCartItemAsync(string id,

            [Service] ICartItemRepository cartItemRepository, [Service] IShoppingSessionRepository sessionRepository,
            [Service] IProductPriceRepository priceRepository, [Service] ITopicEventSender eventSender)
        {
            try
            {
                var cart = await cartItemRepository.GetByIdAsync(id);
                var pricce = await priceRepository.GetByIdAsync(cart.PriceId);
                var result = await cartItemRepository.RemoveAsync(id);
                ShoppingSession session = await sessionRepository.GetByIdAsync(cart.SessionId);

                session.ModifiedAt = DateTime.Now;
                session.Total = session.Total - cart.Quantity * pricce.Price;

                sessionRepository.Update(session);
                _logger.LogInformation("Remove cart item {id} ", id);
                return cart;
            }
            catch(Exception e)
            {
                _logger.LogError("Remove cart item {id} false . ERROR : {error}",id, e.ToString()  );
                return null;
            }
            
           


        }
    }
}

