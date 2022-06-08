namespace eShop.MinimalAPIs;
public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        // All of my API endpoints

        // Product       
        // (API & UI)
        app.MapGet("/product", GetProductsAsync);                   // [FromQuery]

        // (API)
        app.MapGet("/product/{id:int}", GetProductByIdAsync);       // Only for API(on the FrontEnd i get the product from localhost)

        // Orders(API & UI)
        app.MapPost("/order", CreateOrderAsync);                            // [FromBody]
        app.MapGet("/order/{orderId:int}", GetOrderByIdAsync);              // [FromRoute]        
        app.MapGet("/order/{uniqueId}", GetOrderByUniqueIdAsync);           // [FromRoute]
        app.MapPut("/order/processed/{id}", UpdateOrderProcessedAsync);     // [FromRoute], [FromBody]
        app.MapGet("/order/processed", GetProccesedOrdersAsync);
        app.MapGet("/order/outstrandingsorders", GetOutStrandingOrdersAsync);

        // (API)
        app.MapGet("/order/lineItems/{orderId:int}", GetLineItemsByOrderIdAsync);   // [FromRoute]       
        app.MapGet("/orders", GetOrdersAsync);

        // User
        // (API & UI)
        app.MapPost("/login", Login);
        app.MapPost("/register", Register);
    }

    // Product    
    public static async Task<IResult> GetProductByIdAsync([FromRoute] int id,
                                                          IProductRepository productRepo,
                                                          ILogger<Product> logger)
    {
        try
        {
            return Results.Ok(await productRepo.GetProductByIdAsync(id));
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(GetProductByIdAsync)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    public static async Task<IResult> GetProductsAsync([FromQuery] string? filter,
                                                       IProductRepository productRepo,
                                                       ILogger<List<Product>> logger)
    {
        try
        {
            return Results.Ok(await productRepo.GetProductsAsync(filter));
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(GetProductsAsync)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    // Order
    public static async Task<IResult> CreateOrderAsync([FromBody] Order order,
                                                       IOrderRepository orderRepo,
                                                       ILogger<int> logger)
    {
        try
        {
            return Results.Ok(await orderRepo.CreateOrderAsync(order));
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(CreateOrderAsync)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    public static async Task<IResult> GetLineItemsByOrderIdAsync([FromRoute] int orderId,
                                                                 IOrderRepository orderRepo,
                                                                 ILogger<OrderLineItem> logger)
    {
        try
        {
            return Results.Ok(await orderRepo.GetLineItemsByOrderIdAsync(orderId));
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(GetLineItemsByOrderIdAsync)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    public static async Task<IResult> GetOrderByIdAsync([FromRoute] int orderId,
                                                        IOrderRepository orderRepo,
                                                        ILogger<Order> logger)
    {
        try
        {
            return Results.Ok(await orderRepo.GetOrderByIdAsync(orderId));
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(GetOrderByIdAsync)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    public static async Task<IResult> GetOrderByUniqueIdAsync([FromRoute] string uniqueId,
                                                              IOrderRepository orderRepo,
                                                              ILogger<Order> logger)
    {
        try
        {
            return Results.Ok(await orderRepo.GetOrderByUniqueIdAsync(uniqueId));
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(GetOrderByUniqueIdAsync)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    public static async Task<IResult> GetOrdersAsync(IOrderRepository orderRepo, ILogger<List<Order>> logger)
    {
        try
        {
            return Results.Ok(await orderRepo.GetOrdersAsync());
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(GetOrdersAsync)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    [Authorize(Roles = "Administrator")]
    public static async Task<IResult> GetOutStrandingOrdersAsync(IOrderRepository orderRepo, ILogger<List<Order>> logger)
    {
        try
        {
            return Results.Ok(await orderRepo.GetOutStrandingOrdersAsync());
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(GetOutStrandingOrdersAsync)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    [Authorize(Roles = "Administrator")]
    public static async Task<IResult> GetProccesedOrdersAsync(IOrderRepository orderRepo, ILogger<List<Order>> logger)
    {
        try
        {
            return Results.Ok(await orderRepo.GetProcessedOrdersAsync());
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(GetProccesedOrdersAsync)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    [Authorize(Roles = "Administrator")]
    public static async Task<IResult> UpdateOrderProcessedAsync([FromRoute] int id,
                                                                [FromBody] Order order,
                                                                IOrderRepository orderRepo,
                                                                ILogger<int> logger)
    {
        try
        {
            if (id < 1)
                return Results.BadRequest();

            await orderRepo.UpdateOrderProcessedAsync(order.AdminUser, order.DateProcessed, id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(UpdateOrderProcessedAsync)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    // Authentification
    [AllowAnonymous]
    public static async Task<IResult> Login([FromQuery] string userName,
                                            [FromQuery] string pwd,
                                            IUserRepository userRepo,
                                            IConfiguration config,
                                            ILogger<string> logger)
    {
        try
        {
            var user = await userRepo.LoginUser(userName, pwd);

            if (user is null)
                return Results.Unauthorized();

            var token = JWT.GenerateJwt(config, user);

            return Results.Ok(token);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(Login)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    [Authorize(Roles = "Administrator")]
    public static async Task<IResult> Register([FromBody] User user, IUserRepository userRepo, ILogger<string> logger)
    {
        try
        {
            return Results.Ok(await userRepo.RegisterUser(user));
        }
        catch (Exception ex)
        {
            logger.LogError($"Error at {nameof(Register)} - {ErrorMsg.InternalServerError500(ex)}");
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }
}
