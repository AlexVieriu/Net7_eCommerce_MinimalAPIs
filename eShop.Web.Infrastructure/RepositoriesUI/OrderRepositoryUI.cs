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

    public async Task<Order> GetOrderByUniqueId(string url, string uniqueId)
    {
        var order =  await _client.GetFromJsonAsync<Order>($"{Endpoints.OrderUniqueIdUrl}?uniqueId={uniqueId}");

        return order;
    }
}
