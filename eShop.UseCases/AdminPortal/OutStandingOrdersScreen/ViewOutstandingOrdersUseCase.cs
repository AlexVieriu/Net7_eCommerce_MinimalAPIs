namespace eShop.UseCases.AdminPortal.OutStandingOrdersScreen;

public class ViewOutstandingOrdersUseCase : IViewOutstandingOrdersUseCase
{
    private readonly IOrderRepositoryUI _orderRepoUI;

    public ViewOutstandingOrdersUseCase(IOrderRepositoryUI orderRepoUI)
    {
        _orderRepoUI = orderRepoUI;
    }

    public async Task<List<Order>> ExecuteAsync(string url)
    {
        return await _orderRepoUI.GetOutstandingOrders(url);
    }
}
