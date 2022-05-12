namespace eShop.UseCases.CustomerPortal.SearchProductScreen;

public interface ISearchProductUseCase
{
    Task<Product> GetProduct(string url, int id);
    Task<List<Product>> GetProducts(string url, string? filter="");
}
