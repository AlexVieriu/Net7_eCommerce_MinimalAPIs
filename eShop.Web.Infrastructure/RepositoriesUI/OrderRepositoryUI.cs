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
        var order = await _client.GetFromJsonAsync<Order>(url);

        return order;
    }

    public async Task<bool> GetProcessedOrderById(string url, int id, Order order)
    {
        _client.DefaultRequestHeaders.Authorization = await HeaderValue();
        var response =  await _client.PutAsJsonAsync($"{url}/{id}", order);
        if (response.IsSuccessStatusCode)
            return true;

        return false;
    }

    public async Task<List<Order>> GetOutstandingOrders(string url)
    {
        _client.DefaultRequestHeaders.Authorization = await HeaderValue();

        var orders = await _client.GetFromJsonAsync<List<Order>>(url);

        return orders;
    }

    public async Task<List<Order>> GetProcessedOrders(string url)
    {
        _client.DefaultRequestHeaders.Authorization = await HeaderValue();
        var orders = await _client.GetFromJsonAsync<List<Order>>(url);

        return orders;
    }

    private async Task<AuthenticationHeaderValue> HeaderValue()
    {
        var bearerToken = await _localStorage.GetItemAsync<string>(Token.TokenName);
        return new AuthenticationHeaderValue("bearer", bearerToken);
    }
}
