namespace eShop.Web.Infrastructure.Authentication;

public class AuthenticationRepository : IAuthentificationRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly ApiAuthenticationStateProvider _apiState;

    public AuthenticationRepository(HttpClient httpClient,
                                    ILocalStorageService localStorage,
                                    ApiAuthenticationStateProvider apiState)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _apiState = apiState;
    }

    public async Task<bool> Login(string userName, string password)
    {
        var response = await _httpClient.PostAsJsonAsync($"{Endpoints.LoginUrl}?userName={userName}&pwd={password}", new { });
        if (response.IsSuccessStatusCode == false)
            return false;

        // get the token from the response and put it in the localhost
        var content = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<string>(content);

        // Store token 
        await _localStorage.SetItemAsync(Token.TokenName, token);

        // Change state of the app
        await _apiState.LoggedIn();

        // put the token in the httpheader, so every time when i send a request to the api, the authentication will be there
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return true;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(Token.TokenName);
        await _apiState.LoggedOut();
    }

    public async Task<bool> Register(User user)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterUrl, user);

        return response.IsSuccessStatusCode;
    }
}
