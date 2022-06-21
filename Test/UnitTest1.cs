//using Api.Queries;
//using Core.Base;
//using Core.Entities;
//using Core.Repositories;
//using GraphQL.Types;
//using HotChocolate.Execution;
//using Infrastructure.Base;
//using Infrastructure.Data;
//using Infrastructure.Data.Interfaces;
//using Infrastructure.Repositories;
//using Infrastructure.Repositories.Interfaces;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;

//namespace Test;

//public class UserQueryTests
//{
//    [SetUp]
//    public void Setup()
//    {
//    }

//    [Test]
//    public void Test1()
//    {
//        Assert.Pass();
//    }
//    [Test]
//    public async Task SayHello_HelloIsReturned()
//    {
//        // arrange
//        IServiceProvider serviceProvider =
//            new ServiceCollection()
//                .AddScoped<UserManager<ApplicationUser>>()
//    .AddScoped<ICatalogContext, CatalogContext>()
//    .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
//    .AddScoped<ICategoryRepository, CategoryRepository>()
//    .AddScoped<IUserRepository, UserRepository>()
//    .AddScoped<IProductRepository, ProductRepository>()
//    .AddScoped<IMessageRepository, MessageRepository>()
//    .AddScoped<ICartItemRepository, CartItemRepository>()
//    .AddScoped<IBillRepository, BillRepository>()
//    .AddScoped<IDiscountRepository, DiscountRepository>()
//    .AddScoped<IShoppingSessionRepository, ShoppingSessionRepository>()
//    .AddScoped<IOrderItemRepository, OrderItemRepository>()
//    .AddScoped<IOrderDetailsRepository, OrderDetailsRepository>()
//                .BuildServiceProvider();

//        var executor = await new ServiceCollection()
//                .AddGraphQL()
//                .AddQueryType<Query>()
//                .BuildRequestExecutorAsync();


//        IReadOnlyQueryRequest request =
//            QueryRequestBuilder.New()
//                .SetQuery("{ sayHello }")
//                .SetServices(serviceProvider)
//                .AddProperty("Key", "value")
//                .Create();

//        // act
//        IExecutionResult result = await executor.ExecuteAsync(request);

//        // assert
//        // so how do we assert this thing???
//    }

//}
