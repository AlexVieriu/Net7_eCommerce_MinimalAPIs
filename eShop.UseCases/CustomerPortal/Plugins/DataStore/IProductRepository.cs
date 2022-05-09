namespace eShop.UseCases.CustomerPortal.Plugins.DataStore;
public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int productId);
    Task<IEnumerable<Product>> GetProductsAsync(string? filter);
}
