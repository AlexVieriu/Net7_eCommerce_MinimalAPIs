namespace eShop.DataStore.SQL.Dapper;
public class ProductRepository : IProductRepository
{
    private readonly ISql _sql;

    public ProductRepository(ISql sql)
    {
        _sql = sql;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        try
        {
            var product =  (await _sql.LoadData<Product, dynamic>("sp_GetProductbyId", new { IdProduct = productId }))
                            .FirstOrDefault();

            return product;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(string? filter)
    {
        try
        {
            if (string.IsNullOrEmpty(filter))
                filter = "%";

            var products =  await _sql.LoadData<Product, dynamic>("sp_GetProducts", new { Filter = filter });
            return products;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
