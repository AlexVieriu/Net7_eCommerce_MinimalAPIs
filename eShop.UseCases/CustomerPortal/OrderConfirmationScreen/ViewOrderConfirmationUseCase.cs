namespace eShop.UseCases.CustomerPortal.OrderConfirmationScreen;

public class ViewOrderConfirmationUseCase : IViewOrderConfirmationUseCase
{
    private readonly IOrderRepositoryUI _orderRepoUI;

    public ViewOrderConfirmationUseCase(IOrderRepositoryUI orderRepoUI)
    {
        _orderRepoUI = orderRepoUI;
    }

    public async Task<Order> ExecuteAsync(string url, string uniqueId)
    {
        return await _orderRepoUI.GetOrderByUniqueId(url + "/" + uniqueId);
    }
}
