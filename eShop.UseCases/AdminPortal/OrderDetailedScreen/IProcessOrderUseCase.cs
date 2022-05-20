namespace eShop.UseCases.AdminPortal.OrderDetailedScreen;

public interface IProcessOrderUseCase
{
    Task<bool> ExecuteAsync(string orderProcessedUrl, string orderUrl, int orderId, string adminUserName);
}
