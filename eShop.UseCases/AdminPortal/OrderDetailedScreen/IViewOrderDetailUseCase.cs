namespace eShop.UseCases.AdminPortal.OrderDetailedScreen;

public interface IViewOrderDetailUseCase
{
    Task<Order> ExecuteAsync(string url, int orderId);
}
