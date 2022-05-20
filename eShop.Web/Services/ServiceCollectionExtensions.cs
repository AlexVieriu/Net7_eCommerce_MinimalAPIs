namespace eShop.Web.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection CustomerPortalServices(this IServiceCollection services)
    {
        services.AddTransient<ISearchProductUseCase, SearchProductUseCase>();
        services.AddTransient<IViewProductUseCase, ViewProductUseCase>();
        services.AddTransient<IAddProductToCartUseCase, AddProductToCartUseCase>();

        services.AddTransient<IViewShoppingCartUseCase, ViewShoppingCartUseCase>();
        services.AddTransient<IUpdateQuantityUseCase, UpdateQuantityUseCase>();
        services.AddTransient<IDeleteLineItemUseCase, DeleteLineItemUseCase>();

        services.AddTransient<IPlaceOrderUseCase, PlaceOrderUseCase>();
        services.AddTransient<IViewOrderConfirmationUseCase, ViewOrderConfirmationUseCase>();

        return services;
    }

    public static IServiceCollection AdminPortalServices(this IServiceCollection services)
    {
        services.AddTransient<IProcessOrderUseCase, ProcessOrderUseCase>();
        services.AddTransient<IViewOrderDetailUseCase, ViewOrderDetailUseCase>();
        services.AddTransient<IViewOutstandingOrdersUseCase, ViewOutstandingOrdersUseCase>();
        services.AddTransient<IViewProccessedOrdersUseCase, ViewProccessedOrdersUseCase>();

        return services;
    }
}
