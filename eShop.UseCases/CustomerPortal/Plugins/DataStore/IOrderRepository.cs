namespace eShop.UseCases.CustomerPortal.Plugins.DataStore;
public interface IOrderRepository
{
    Task<int> CreateOrderAsync(Order order);
    Task<IEnumerable<OrderLineItem>> GetLineItemsByOrderIdAsync(int orderId);
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<Order> GetOrderByUniqueIdAsync(string uniqueId);
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<IEnumerable<Order>> GetOutStrandingOrdersAsync();
    Task<IEnumerable<Order>> GetProcessedOrdersAsync();
    Task UpdateOrderProcessedAsync(string adminUser, DateTime? dateProcced, int? orderId);
}
