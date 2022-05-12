namespace eShop.UseCases.CustomerPortal.SearchProductScreen;

public class SearchProductUseCase : ISearchProductUseCase
{
    private readonly IProductRepositoryUI _productRepoUI;

    public SearchProductUseCase(IProductRepositoryUI productRepoUI)
    {
        _productRepoUI = productRepoUI;
    }

    public async Task<Product> GetProduct(string url, int id)
    {
        return await _productRepoUI.GetbyId(url, id);
    }

    public async Task<List<Product>> GetProducts(string url, string? filter)
    {
        return await _productRepoUI.GetProductsWithFilter(url, filter);
    }
}
