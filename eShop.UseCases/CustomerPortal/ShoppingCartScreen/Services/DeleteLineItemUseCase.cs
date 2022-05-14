namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Services;

public class DeleteLineItemUseCase : IDeleteLineItemUseCase
{
    private readonly IShoppingCart _shoppingCart;
    private readonly IShoppingCartStateStore _stateStore;

    public DeleteLineItemUseCase(IShoppingCart shoppingCart, IShoppingCartStateStore stateStore)
    {
        _shoppingCart = shoppingCart;
        _stateStore = stateStore;
    }

    public async Task<Order> ExecuteAsync(int productId)
    {
        var order = await _shoppingCart.RemoveLineItemFromCartAsync(productId);
        _stateStore.BroadCastStateChange();

        return order;
    }
}
