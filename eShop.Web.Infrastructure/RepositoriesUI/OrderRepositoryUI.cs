namespace eShop.Web.Infrastructure.RepositoriesUI;

public class OrderRepositoryUI : BaseRepositoryUI<Order>, IOrderRepositoryUI
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;

    public OrderRepositoryUI(HttpClient client, ILocalStorageService localStorage)
        : base(client, localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task<Order> GetOrderByUniqueId(string url)
    {
        _client.DefaultRequestHeaders.Authorization = await HeaderValue();
        var order =  await _client.GetFromJsonAsync<Order>(url);

        return order;
    }
        private async Task<AuthenticationHeaderValue> HeaderValue()
    {
        var bearerToken = await _localStorage.GetItemAsync<string>(Token.TokenName);
        return new AuthenticationHeaderValue("bearer", bearerToken);
    }
}
