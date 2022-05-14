namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Services;

public class UpdateQuantityUseCase : IUpdateQuantityUseCase
{
    private readonly IShoppingCart _shoppingCart;
    private readonly IShoppingCartStateStore _stateStore;

    public UpdateQuantityUseCase(IShoppingCart shoppingCart, IShoppingCartStateStore stateStore)
    {
        _shoppingCart = shoppingCart;
        _stateStore = stateStore;
    }

    public async Task<Order> ExecuteAsync(int productId, int qty)
    {
        var order = await _shoppingCart.UpdateQuantityAsync(productId, qty);
        _stateStore.BroadCastStateChange();

        return order;
    }
}
