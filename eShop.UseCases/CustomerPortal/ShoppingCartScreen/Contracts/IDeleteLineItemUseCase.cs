namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Contracts;

public interface IDeleteLineItemUseCase
{
    Task<Order> ExecuteAsync(int productId);
}
