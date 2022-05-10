namespace eShop.Web.Infrastructure.RepositoriesUI;

public class OrderRepositoryUI : IOrderRepositoryUI
{
    public Task<bool> Create(string url, Order obj)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string url, int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetAll(string url)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetbyId(string url, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(string url, Order obj, int id)
    {
        throw new NotImplementedException();
    }
}
