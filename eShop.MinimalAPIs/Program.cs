var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters() {
                        ValidateActor = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });


builder.Services.AddAuthorization(options => {
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});

builder.Services.AddSingleton<ISql, Sql>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddSwaggerGen(x => {
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = "JWT Authorization header using the bearer scheme(Write: Bearer eyJhbGciOiJIUzI1....)",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference {
                    Id="Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

////Minimal APIs 
app.ConfigureApi();

#region Minimal Apis

//// Minimal APIs - Original
//// Product
//#region Product
//app.MapGet("/product/{id:int}", async (int id, IProductRepository productRepo) =>
//{

//    try
//    {
//        var product = await productRepo.GetProductByIdAsync(id);

//        return product is not null ? Results.Ok(product) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).Produces<Product>();

//app.MapGet("/products", async ([FromQuery] string filter, IProductRepository productRepo) =>
//{
//    try
//    {
//        var products = await productRepo.GetProductsAsync(filter);

//        return products is not null || products.Count() <= 0 ? Results.Ok(products) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).Produces<List<Product>>();
//#endregion

////Order
//#region Order
//app.MapPost("/order/create", async ([FromBody] Order order, IOrderRepository orderRepo) =>
//{
//    try
//    {
//        var response = await orderRepo.CreateOrderAsync(order);

//        return response != 0 ? Results.Ok(response) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).Produces<int>();


//app.MapGet("/order/lineItems/{orderId:int}", async (int orderId, IOrderRepository orderRepo) =>
//{
//    try
//    {
//        var lineItems = await orderRepo.GetLineItemsByOrderIdAsync(orderId);

//        return lineItems is not null || lineItems.Count() <= 0 ? Results.Ok(lineItems) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).Produces<List<OrderLineItem>>();


//app.MapGet("/order/{orderId:int}", async (int orderId, IOrderRepository orderRepo) =>
//{
//    try
//    {
//        var order = await orderRepo.GetOrderByIdAsync(orderId);

//        return order is not null ? Results.Ok(order) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).Produces<Order>();


//app.MapGet("/order/{uniqueId}", async (string uniqueId, IOrderRepository orderRepo) =>
//{
//    try
//    {
//        var order = await orderRepo.GetOrderByUniqueIdAsync(uniqueId);

//        return order is not null ? Results.Ok(order) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).Produces<Order>();


//app.MapGet("/orders", async (IOrderRepository orderRepo) =>
//{
//    try
//    {
//        var orders = await orderRepo.GetOrdersAsync();

//        return orders is not null || orders.Count() <= 0 ? Results.Ok(orders) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).Produces<List<Order>>();


//app.MapGet("/ordersOutStanding", async (IOrderRepository orderRepo) =>
//{
//    try
//    {
//        var orders = await orderRepo.GetOutStrandingOrdersAsync();

//        return orders is not null || orders.Count() <= 0 ? Results.Ok(orders) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).Produces<List<Order>>();


//app.MapGet("/ordersProcessed", async (IOrderRepository orderRepo) =>
//{
//    try
//    {
//        var orders = await orderRepo.GetProcessedOrdersAsync();

//        return orders is not null || orders.Count() <= 0 ? Results.Ok(orders) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).Produces<List<Order>>();


//app.MapPut("/orderProcessed", async (Order order, IOrderRepository orderRepo) =>
//{
//    try
//    {
//        await orderRepo.UpdateOrderProcessedAsync(order.AdminUser, order.DateProcessed, order.OrderId);
//        return Results.Ok();
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).Produces(200);


//// Authenthification
//app.MapPost("/login", async ([FromQuery] string userName, [FromQuery] string pwd, IUserRepository userRepo) =>
//{
//    try
//    {
//        var user = await userRepo.LoginUser(userName, pwd);
//        if (user == null)
//            return Results.Unauthorized();

//        var token = JWT.GenerateJwt(builder.Configuration, user);

//        return Results.Ok(token);
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(ErrorMsg.InternalServerError500(ex));
//    }
//}).AllowAnonymous();

//app.MapPost("/register",
//    [Authorize(Roles = "Administrator")] async ([FromBody] User? userRegister, IUserRepository userRepo) =>
//    {
//        try
//        {
//            var isRegistered = await userRepo.RegisterUser(userRegister);

//            if (isRegistered == false)
//                return Results.Unauthorized();

//            return Results.Ok(isRegistered);
//        }
//        catch (Exception ex)
//        {
//            return Results.Problem(ErrorMsg.InternalServerError500(ex));
//        }
//    });

//#endregion

#endregion

app.Run();


