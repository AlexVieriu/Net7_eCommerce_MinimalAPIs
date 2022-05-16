namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Contracts;

public interface IPlaceOrderUseCase
{
    Task<string> ExecuteAsync(string urlOrder, Order order);
}
