namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Services;

public class AddProductToCartUseCase : IAddProductToCartUseCase
{
    private readonly IShoppingCart _shoppingCart;
    private readonly IStateStore _stateStore;

    public AddProductToCartUseCase(IShoppingCart shoppingCart, IStateStore stateStore)
    {
        _shoppingCart = shoppingCart;
        _stateStore = stateStore;
    }

    public async Task ExecuteAsync(Product product)
    {
        await _shoppingCart.AddProductToCartAsync(product);
        _stateStore.BroadCastStateChange();
    }
}
