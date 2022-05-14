namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Services;

public class ViewShoppingCartUseCase : IViewShoppingCartUseCase
{
    private readonly IShoppingCart _shoppingCart;

    public ViewShoppingCartUseCase(IShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public async Task<Order> ExecuteAsync()
    {
        return await _shoppingCart.GetOrderAsync();
    }
}
