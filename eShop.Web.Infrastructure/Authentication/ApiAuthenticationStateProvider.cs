namespace eShop.Web.Infrastructure.Authentication;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly JwtSecurityTokenHandler _jwt;
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;

    public ApiAuthenticationStateProvider(JwtSecurityTokenHandler jwt,
                                          ILocalStorageService localStorage,
                                          HttpClient httpClient)
    {
        _jwt = jwt;
        _localStorage = localStorage;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        try
        {
            var token = await _localStorage.GetItemAsync<string>(Token.TokenName);
            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(user);

            var tokenContent = _jwt.ReadJwtToken(token);
            var expiry = tokenContent.ValidTo;
            
            if(expiry < DateTime.UtcNow)
            {
                await _localStorage.RemoveItemAsync(Token.TokenName);
                return new AuthenticationState(user);
            }

            var claims = GetClaims(tokenContent);

            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            return await Task.FromResult(new AuthenticationState(user));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new AuthenticationState(user));
        }
    }

    public async Task LoggedIn()
    {
        var token = await _localStorage.GetItemAsync<string>(Token.TokenName);
        var tokenContent = _jwt.ReadJwtToken(token);
        var claims = GetClaims(tokenContent);
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        var authState  = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        var nobody = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(nobody));
        NotifyAuthenticationStateChanged(authState);
    }

    private List<Claim> GetClaims(JwtSecurityToken token)
    {
        var claims = token.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, token.Subject));

        return claims;
    }
}
