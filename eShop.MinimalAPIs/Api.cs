﻿namespace eShop.MinimalAPIs;
public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        // All of my API endpoints
        // Orders(API & UI)
        app.MapPost("/order", CreateOrderAsync);                            // [FromBody]
        app.MapGet("/order/{uniqueId}", GetOrderByUniqueIdAsync);           // [FromRoute]
        app.MapGet("/order/{orderId:int}", GetOrderByIdAsync);              // [FromRoute]        
        app.MapPut("/order/processed/{id}", UpdateOrderProcessedAsync);     // [FromRoute], [FromQuery], [FromQuery]
        app.MapGet("/order/processed", GetProccesedOrdersAsync);
        app.MapGet("/order/outstrandingsorders", GetOutStrandingOrdersAsync);

        // For Testing API
        app.MapGet("/order/lineItems/{orderId:int}", GetLineItemsByOrderIdAsync);   // [FromRoute]       
        app.MapGet("/orders", GetOrdersAsync);

        // Product       
        app.MapGet("/product/{Id:int}", GetProductByIdAsync);       // Only for API(on the FrontEnd i get the product from localhost)
        app.MapGet("/product", GetProductsAsync);                   // [FromQuery]

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

    public static async Task<IResult> GetLineItemsByOrderIdAsync([FromRoute] int orderId, IOrderRepository orderRepo)
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

    public static async Task<IResult> GetOrderByIdAsync([FromRoute] int orderId, IOrderRepository orderRepo)
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

    public static async Task<IResult> GetOrderByUniqueIdAsync([FromRoute] string uniqueId, IOrderRepository orderRepo)
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
    public static async Task<IResult> UpdateOrderProcessedAsync([FromRoute] int id,
                                                                [FromBody] Order order,
                                                                IOrderRepository orderRepo)
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

    public static async Task<IResult> GetProductsAsync([FromQuery] string? filter, IProductRepository productRepo)
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
