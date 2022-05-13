namespace eShop.UseCases.CustomerPortal.ViewProductScreen;

public class ViewProductUseCase : IViewProductUseCase
{
    private readonly IProductRepositoryUI _productRepoUI;

    public ViewProductUseCase(IProductRepositoryUI productRepoUI)
    {
        _productRepoUI = productRepoUI;
    }

    public async Task<Product> Execute(string url, int id)
    {
        return await _productRepoUI.GetbyId(url, id);
    }
}
