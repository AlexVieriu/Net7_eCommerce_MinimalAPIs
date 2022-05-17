namespace eShop.UseCases.CustomerPortal.PlaceOrderScreen;

public interface IPlaceOrderUseCase
{
    Task<string> ExecuteAsync(string urlOrder, Order order);
    Task<Order> GetSummaryForOrderAsync();
}
