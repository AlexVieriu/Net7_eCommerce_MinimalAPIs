namespace eShop.UseCases.AdminPortal.OutStandingOrdersScreen;

public interface IViewOutstandingOrdersUseCase
{
    Task<List<Order>> ExecuteAsync(string url);
}
