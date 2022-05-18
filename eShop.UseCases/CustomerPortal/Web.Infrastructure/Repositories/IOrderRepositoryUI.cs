namespace eShop.UseCases.Web.Infrastructure.Repositories;
public interface IOrderRepositoryUI: IBaseRepositoryUI<Order> 
{
    Task<Order> GetOrderByUniqueId(string url);
}
