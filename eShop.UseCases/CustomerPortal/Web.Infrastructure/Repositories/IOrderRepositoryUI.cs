namespace eShop.UseCases.Web.Infrastructure.Repositories;
public interface IOrderRepositoryUI: IBaseRepositoryUI<Order> 
{
    Task<Order> GetOrderByUniqueId(string url);
    Task<List<Order>> GetOutstandingOrders(string url);
    Task<bool> GetProcessedOrderById(string url, int id, Order orderId);
    Task<List<Order>> GetProcessedOrders(string url);
}
