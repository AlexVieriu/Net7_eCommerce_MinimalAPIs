namespace eShop.UseCases.Web.Infrastructure.Repositories;
public interface IBaseRepository<T> where T: class
{
    Task<List<T>> GetAll(string url);
    Task<T> GetbyId(string url, int id);
    Task<bool> Create(string url, T obj);
    Task<bool> Delete(string url, int id);
    Task<bool> Update(string url, T obj, int id);
}
