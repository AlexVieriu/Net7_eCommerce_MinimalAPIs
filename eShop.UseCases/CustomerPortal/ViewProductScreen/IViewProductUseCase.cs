namespace eShop.UseCases.CustomerPortal.ViewProductScreen;

public interface IViewProductUseCase
{
    Task<Product> Execute(string url, int id);
}
