namespace eShop.Web.Infrastructure.RepositoriesUI;

public class ProductRepositoryUI : BaseRepositoryUI<Product>, IProductRepositoryUI
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;

    public ProductRepositoryUI(HttpClient client, ILocalStorageService localStorage)
        : base(client, localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task<List<Product>> GetProductsWithFilter(string url, string? filter)
    {
        _client.DefaultRequestHeaders.Authorization = await HeaderValue();

        var products = await _client.GetFromJsonAsync<List<Product>>($"{url}?filter={filter}");

        if (products == null)
            return new List<Product>();

        return products;
    }

    private async Task<AuthenticationHeaderValue> HeaderValue()
    {
        var bearerToken = await _localStorage.GetItemAsync<string>(Token.TokenName);
        return new AuthenticationHeaderValue("bearer", bearerToken);
    }
}
