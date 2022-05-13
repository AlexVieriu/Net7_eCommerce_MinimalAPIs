var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7173") });
builder.Services.AddBlazoredLocalStorage();

// Authorization
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(o => o.GetRequiredService<ApiAuthenticationStateProvider>());
builder.Services.AddScoped<JwtSecurityTokenHandler>();
builder.Services.AddAuthorizationCore();

// DI
builder.Services.AddScoped<IAuthentificationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCartBase>();
builder.Services.AddScoped<IStateStore, StateStore>();
builder.Services.AddScoped<IShoppingCartStateStore, ShoppingCartStateStore>();

builder.Services.AddTransient<IProductRepositoryUI, ProductRepositoryUI>();
builder.Services.AddTransient<ISearchProductUseCase, SearchProductUseCase>();
builder.Services.AddTransient<IViewProductUseCase, ViewProductUseCase>();
builder.Services.AddTransient<IAddProductToCartUseCase, AddProductToCartUseCase>();


await builder.Build().RunAsync();
