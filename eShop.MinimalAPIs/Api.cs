namespace eShop.MinimalAPIs;
public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        // All of my API endpoints
        // Orders
        app.MapPost("/order", CreateOrderAsync);
        app.MapGet("/order/lineItems/{orderId:int}", GetLineItemsByOrderIdAsync);
        app.MapGet("/order/{orderId:int}", GetOrderByIdAsync);
        app.MapGet("/orders/{uniqueId}", GetOrderByUniqueIdAsync);
        app.MapGet("/orders", GetOrdersAsync);
        app.MapGet("/orders/outstrandings", GetOutStrandingOrdersAsync);
        app.MapGet("/orders/processed", GetProccesedOrdersAsync);
        app.MapPut("/orders/processed", UpdateOrderProcessedAsync);

        // Product
        app.MapGet("/product/{Id:int}", GetProductByIdAsync);
        app.MapGet("/product", GetProductsAsync);

        // User
        app.MapPost("/login", Login);
        app.MapPost("/register", Register);
    }

    // Order
    public static async Task<IResult> CreateOrderAsync(Order order, IOrderRepository orderRepo)
    {
        try
        {
            return Results.Ok(await orderRepo.CreateOrderAsync(order));
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }
  
    public static async Task<IResult> GetLineItemsByOrderIdAsync(int orderId, IOrderRepository orderRepo)
    {
        try
        {
            return Results.Ok(await orderRepo.GetLineItemsByOrderIdAsync(orderId));
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    public static async Task<IResult> GetOrderByIdAsync(int orderId, IOrderRepository orderRepo)
    {
        try
        {
            return Results.Ok(await orderRepo.GetOrderByIdAsync(orderId));
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    public static async Task<IResult> GetOrderByUniqueIdAsync(string uniqueId, IOrderRepository orderRepo)
    {
        try
        {
            return Results.Ok(await orderRepo.GetOrderByUniqueIdAsync(uniqueId));
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }
    public static async Task<IResult> GetOrdersAsync(IOrderRepository orderRepo)
    {
        try
        {
            return Results.Ok(await orderRepo.GetOrdersAsync());
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }
    public static async Task<IResult> GetOutStrandingOrdersAsync(IOrderRepository orderRepo)
    {
        try
        {
            return Results.Ok(await orderRepo.GetOutStrandingOrdersAsync());
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }
    public static async Task<IResult> GetProccesedOrdersAsync(IOrderRepository orderRepo)
    {
        try
        {
            return Results.Ok(await orderRepo.GetProcessedOrdersAsync());
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }
    public static async Task<IResult> UpdateOrderProcessedAsync(Order order, IOrderRepository orderRepo)
    {
        try
        {
            await orderRepo.UpdateOrderProcessedAsync(order.AdminUser, order.DateProcessed, order.OrderId);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    // Product
    public static async Task<IResult> GetProductByIdAsync(int id, IProductRepository productRepo)
    {
        try
        {
            return Results.Ok(await productRepo.GetProductByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    public static async Task<IResult> GetProductsAsync([FromQuery]string? filter, IProductRepository productRepo)
    {
        try
        {
            return Results.Ok(await productRepo.GetProductsAsync(filter));
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    // Authentification
    [AllowAnonymous]
    public static async Task<IResult> Login([FromQuery] string userName,
                                            [FromQuery] string pwd,
                                            IUserRepository userRepo,
                                            IConfiguration config)
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
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }

    [Authorize(Roles = "Administrator")]
    public static async Task<IResult> Register([FromBody] User user, IUserRepository userRepo)
    {
        try
        {
            return Results.Ok(await userRepo.RegisterUser(user));
        }
        catch (Exception ex)
        {
            return Results.Problem($"{ex.Message} - {ex.InnerException}");
        }
    }
}
