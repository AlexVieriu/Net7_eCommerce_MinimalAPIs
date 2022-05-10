namespace eShop.Web.Infrastructure.RepositoriesUI;

public class ProductRepositoryUI : IProductRepositoryUI
{
    public Task<bool> Create(string url, Product obj)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string url, int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAll(string url)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetbyId(string url, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(string url, Product obj, int id)
    {
        throw new NotImplementedException();
    }
}
