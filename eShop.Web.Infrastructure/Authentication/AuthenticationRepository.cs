namespace eShop.Web.Infrastructure.Authentication;

public class AuthenticationRepository
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

    public async Task<bool> Login(User user)
    {
        var token = "";
        await _apiState.LoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return true;
    }

    public async Task LogOut()
    {
        await _apiState.LoggedOut();
    }

    public async Task<bool> Register(User user)
    {       
        return true;
    }
}
