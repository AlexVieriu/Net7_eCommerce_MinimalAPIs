namespace eShop.Web.Infrastructure.RepositoriesUI;

public class BaseRepositoryUI<T> : IBaseRepositoryUI<T> where T : class
{
    public Task<bool> Create(string url, T obj)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string url, int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAll(string url)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetbyId(string url, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(string url, T obj, int id)
    {
        throw new NotImplementedException();
    }
}
