var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7173") });
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAutoMapper(typeof(CustomerPortalMapping));

// Authorization
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(o => o.GetRequiredService<ApiAuthenticationStateProvider>());
builder.Services.AddScoped<JwtSecurityTokenHandler>();
builder.Services.AddAuthorizationCore();

// DI
builder.Services.AddScoped<IAuthentificationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCartBase>();
builder.Services.AddScoped<IShoppingCartStateStore, ShoppingCartStateStore>();

builder.Services.AddScoped<IOrderRepositoryUI, OrderRepositoryUI>();
builder.Services.AddScoped<IProductRepositoryUI, ProductRepositoryUI>();

builder.Services.AddTransient<IOrderService, OrderService>();

// Customer Portal Services
builder.Services.CustomerPortalServices();

// Admin Portal Services
builder.Services.AdminPortalServices();


await builder.Build().RunAsync();
