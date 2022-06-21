using Core.Base;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Base;
using Infrastructure.Configurations;
using Infrastructure.Data;
using Infrastructure.Data.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Api.Mutations;
using Api.Queries;
using Api.Resolvers;
using Api.Types;
using Api.Subscriptions;
using HotChocolate.Language;
using Serilog;
using Core.Hubs;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.Identity;
using Ninject;
using Microsoft.AspNetCore.ResponseCompression;
using FireflySoft.RateLimit.Core.InProcessAlgorithm;
using FireflySoft.RateLimit.AspNetCore;
using FireflySoft.RateLimit.Core.Rule;

var builder = WebApplication.CreateBuilder(args);


//Configuration
builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection("MongoDbConfiguration"));

builder.Services.AddScoped<MongoDbConfiguration>();
var mongoDbSettings = builder.Configuration.GetSection("MongoDbConfiguration").Get<MongoDbConfiguration>();



// MongoIdentity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
      .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
      (
          mongoDbSettings.ConnectionString, mongoDbSettings.Database
      );

// Repositories
builder.Services.AddScoped< UserManager<ApplicationUser>>()
    .AddScoped<ICatalogContext, CatalogContext>()
    .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
    .AddScoped<ICategoryRepository, CategoryRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IMessageRepository, MessageRepository>()
    .AddScoped<ICartItemRepository, CartItemRepository>()
    .AddScoped<IBillRepository, BillRepository>()
    .AddScoped<IDiscountRepository, DiscountRepository>()
    .AddScoped<IShoppingSessionRepository, ShoppingSessionRepository>()
    .AddScoped<IOrderItemRepository, OrderItemRepository>()
    .AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();


builder.Services.AddMemoryCache();
builder.Services.AddSha256DocumentHashProvider(HashFormat.Hex);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Serilog
var logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
.Enrich.FromLogContext()
.CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//SignalR

builder.Services.AddSignalR(e => {
    e.EnableDetailedErrors = true; 
    e.MaximumReceiveMessageSize = 102400000;
});
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.MimeTypes =
    ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://example.com")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});


//rate limit token bucket
builder.Services.AddRateLimit(new InProcessTokenBucketAlgorithm(
                new[] {
                    new TokenBucketRule(30,10,TimeSpan.FromSeconds(1))
                    {
                        ExtractTarget = context =>
                        {
                            return (context as HttpContext).Request.Path.Value;
                        },
                        CheckRuleMatching = context =>
                        {
                            return true;
                        },
                        Name="default limit rule",
                    }
                })
            );


// GraphQL
builder.Services
   .AddGraphQLServer()
   .AddFiltering()
   .AddProjections()
    .AddSorting()
    .AddQueryType(d => d.Name(nameof(Query)))
                .AddTypeExtension<BillQuery>()
                .AddTypeExtension<ProductQuery>()
                .AddTypeExtension<CategoryQuery>()
                .AddTypeExtension<UserQuery>()
                .AddTypeExtension<OrderItemQuery>()
                .AddTypeExtension<CartItemQuery>()
                .AddTypeExtension<OrderDetailsQuery>()
                .AddTypeExtension<DiscountQuery>()
                .AddTypeExtension<ShoppingSessionQuery>()
                .UseAutomaticPersistedQueryPipeline()
                .AddReadOnlyFileSystemQueryStorage("./persisted_queries")
                .AddInMemoryQueryStorage()
            .AddMutationType(d => d.Name(nameof(Mutation)))
                .AddTypeExtension<BillMutation>()
                .AddTypeExtension<DiscountMutation>()
                .AddTypeExtension<OrderItemMutation>()
                .AddTypeExtension<OrderDetailsMutation>()
                .AddTypeExtension<ProductMutation>()
                .AddTypeExtension<CategoryMutation>()
                .AddTypeExtension<UserMutation>()
                .AddTypeExtension<CartItemMutation>()
                .AddTypeExtension<ShoppingSessionMutation>()
            .AddSubscriptionType(d => d.Name("Subscription"))
                .AddTypeExtension<CustomerSubscription>()
            .AddType<ProductType>()
            .AddType<BillType>()
            .AddType<UserType>()
            .AddType<ShoppingSessionType>()
            .AddType<CategoryType>()
            .AddType<CartItemType>()
            .AddType<CategoryResolver>()
            .AddType<ProductResolver>()
            .AddType<UserResolver>() 

            .AddInMemorySubscriptions();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseWebSockets();
app.UseStaticFiles();
app.UseRateLimit(); 
//app.UseAuthorization();
app.UseRouting();
app.UseCors();
app.MapGraphQL("/api/graphql");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
    
});
app.MapHub<ChatHub>("/chathub");


app.UseResponseCompression(); 

app.Run();


