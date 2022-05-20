namespace eShop.Web.Infrastructure.Routes;
public static class Endpoints
{
    public static string BaseUrl = "https://localhost:7173";

    // User
    public static string LoginUrl = $"{BaseUrl}/login";
    public static string RegisterUrl = $"{BaseUrl}/register";

    // Product
    public static string ProductUrl = $"{BaseUrl}/product";

    // Order
    public static string OrderUrl = $"{BaseUrl}/order";
    public static string OrderProcessedUrl = $"{BaseUrl}/order/processed";    
    public static string OutstandingOrdersUrl = $"{BaseUrl}/order/outstrandingsorders";    

}
