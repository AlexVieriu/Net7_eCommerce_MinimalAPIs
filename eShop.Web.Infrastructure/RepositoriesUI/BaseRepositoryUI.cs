namespace eShop.Web.Infrastructure.RepositoriesUI;

public class BaseRepositoryUI<T> : IBaseRepositoryUI<T> where T : class
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;

    public BaseRepositoryUI(HttpClient client, ILocalStorageService localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task<bool> Create(string url, T obj)
    {

        _client.DefaultRequestHeaders.Authorization = await HeaderValue();
        var response = await _client.PostAsJsonAsync(url, obj);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Delete(string url, int id)
    {

        if (id < 1)
            return false;

        _client.DefaultRequestHeaders.Authorization = await HeaderValue();
        var response = await _client.DeleteAsync(url + id);

        return response.IsSuccessStatusCode;

    }

    public async Task<List<T>> GetAll(string url)
    {
        _client.DefaultRequestHeaders.Authorization = await HeaderValue();

        var items = await _client.GetFromJsonAsync<List<T>>(url);

        if (items == null)
            return new List<T>();

        return items;
    }

    public async Task<T> GetbyId(string url, int id)
    {
        if (id < 1)
            return default(T);

        _client.DefaultRequestHeaders.Authorization = await HeaderValue();

        var item = await _client.GetFromJsonAsync<T>(url + id);
        if (item == null)
            return default(T);

        return item;
    }

    public async Task<bool> Update(string url, T obj, int id)
    {
        if (id < 1 || obj is null)
            return false;

        _client.DefaultRequestHeaders.Authorization = await HeaderValue();

        var response = await _client.PutAsJsonAsync(url + id, obj);

        return response.IsSuccessStatusCode;
    }

    private async Task<AuthenticationHeaderValue> HeaderValue()
    {
        var bearerToken = await _localStorage.GetItemAsync<string>(Token.TokenName);
        return new AuthenticationHeaderValue("bearer", bearerToken);
    }
}
