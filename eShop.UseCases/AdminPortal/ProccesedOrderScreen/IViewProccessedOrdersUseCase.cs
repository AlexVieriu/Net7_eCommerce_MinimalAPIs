namespace eShop.UseCases.AdminPortal.ProccesedOrderScreen;

public interface IViewProccessedOrdersUseCase
{
    Task<List<Order>> ExecuteAsync(string url);
}
