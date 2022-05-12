namespace eShop.UseCases.Web.Infrastructure.Repositories;
public interface IProductRepositoryUI : IBaseRepositoryUI<Product>
{
    Task<List<Product>> GetProductsWithFilter(string url, string? filter = "");
}
