namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Contracts;

public interface IAddProductToCartUseCase
{
    Task ExecuteAsync(Product product);
}
