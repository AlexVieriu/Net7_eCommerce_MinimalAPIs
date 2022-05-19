namespace eShop.UseCases.AdminPortal.OrderDetailedScreen;

public class ViewOrderDetailUseCase : IViewOrderDetailUseCase
{
    private readonly IOrderRepositoryUI _orderRepoUI;

    public ViewOrderDetailUseCase(IOrderRepositoryUI orderRepoUI)
    {
        _orderRepoUI = orderRepoUI;
    }

    public async Task<Order> ExecuteAsync(string url, int orderId)
    {
        return await _orderRepoUI.GetbyId(url, orderId);
    }
}
