namespace eShop.UseCases.AdminPortal.ProccesedOrderScreen;

public class ViewProccessedOrdersUseCase : IViewProccessedOrdersUseCase
{
    private readonly IOrderRepositoryUI _orderRepoUI;

    public ViewProccessedOrdersUseCase(IOrderRepositoryUI orderRepoUI)
    {
        _orderRepoUI = orderRepoUI;
    }

    public async Task<List<Order>> ExecuteAsync(string url)
    {
        return await _orderRepoUI.GetProcessedOrders(url);
    }
}
