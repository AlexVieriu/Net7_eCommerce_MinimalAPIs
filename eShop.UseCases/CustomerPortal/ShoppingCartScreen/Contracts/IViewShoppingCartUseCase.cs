namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Contracts;

public interface IViewShoppingCartUseCase
{
    Task<Order> ExecuteAsync();
}
