namespace eShop.UseCases.AdminPortal.OrderDetailedScreen;

public interface IProcessOrderUseCase
{
    Task<bool> ExecuteAsync(string url, int orderId, string adminUserName);
}
