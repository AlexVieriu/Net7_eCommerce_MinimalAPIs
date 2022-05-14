namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Contracts;

public interface IPlaceOrderUseCase
{
    Task<string> ExecuteAsync(Order order);
}
