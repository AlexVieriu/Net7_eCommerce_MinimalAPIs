namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Contracts;

public interface IUpdateQuantityUseCase
{
    Task<Order> ExecuteAsync(int productId, int qty);
}
