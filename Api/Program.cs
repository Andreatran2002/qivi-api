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
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    //
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;


});



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
    .AddScoped<IDiscountRepository, DiscountRepository>()
    .AddScoped<IShoppingSessionRepository, ShoppingSessionRepository>()
    .AddScoped<IOrderItemRepository, OrderItemRepository>()
    .AddScoped<IOrderDetailsRepository, OrderDetailsRepository>()
    .AddScoped<IProductPriceRepository, ProductPriceRepository>()
    .AddScoped<IUserOrderInfoRepository, UserOrderInfoRepository>();


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
            builder.WithOrigins("http://localhost:3000")
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
                .AddTypeExtension<ProductQuery>()
                .AddTypeExtension<CategoryQuery>()
                .AddTypeExtension<UserQuery>()
                .AddTypeExtension<OrderItemQuery>()
                .AddTypeExtension<CartItemQuery>()
                .AddTypeExtension<OrderDetailsQuery>()
                .AddType<ProductPriceQuery>()
                .AddTypeExtension<DiscountQuery>()
                .AddTypeExtension<UserOrderInfoQuery>()
                .AddTypeExtension<ShoppingSessionQuery>()
                    .UseAutomaticPersistedQueryPipeline()
                    .AddReadOnlyFileSystemQueryStorage("./persisted_queries")
                    .AddInMemoryQueryStorage()
                .AddMutationType(d => d.Name(nameof(Mutation)))
                .AddTypeExtension<DiscountMutation>()
                .AddTypeExtension<OrderItemMutation>()
                .AddTypeExtension<OrderDetailsMutation>()
                .AddTypeExtension<ProductMutation>()
                .AddTypeExtension<CategoryMutation>()
                .AddTypeExtension<UserMutation>()
                .AddTypeExtension<CartItemMutation>()
                .AddTypeExtension<ShoppingSessionMutation>()
                .AddTypeExtension<UserOrderInfoMutation>()
                .AddTypeExtension<ProductPriceMutation>()
                .AddSubscriptionType(d => d.Name("Subscription"))
                .AddTypeExtension<CustomerSubscription>()
            .AddType<ProductType>()
            .AddType<UserOrderInfoType>()
            .AddType<UserType>()
            .AddType<ProductPriceType>()
            .AddType<ShoppingSessionType>()
            .AddType<CategoryType>()
            .AddType<OrderDetailsType>()
            .AddType<OrderItemType>()
            .AddType<CartItemType>()
            .AddType<UserOrderInfoResolver>()
            .AddType<UserResolver>()
            .AddType<CategoryResolver>()
            .AddType<ProductResolver>()
            .AddType<ProductPriceResolver>()
            .AddType<OrderItemResolver>() 

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


