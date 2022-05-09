namespace eShop.Web.Infrastructure.Authentication;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly JwtSecurityTokenHandler _jwt;

    public ApiAuthenticationStateProvider(JwtSecurityTokenHandler jwt)
    {
        _jwt = jwt;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        try
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJWaWVyaXUgQWxleGFuZHJ1IiwianRpIjoiMTA1YjA1OGItNWUwNC00MjYyLWE1ZWUtNjI2M2FmNzk1MTEzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW5pc3RyYXRvciIsImV4cCI6MTY1MjA5NTU1NSwiaXNzIjoiY2xlYW5hcmhpdGVjdHVyZS5taW5pbWFsQXBpc0BnbWFpbC5jb20iLCJhdWQiOiJjbGVhbmFyaGl0ZWN0dXJlLm1pbmltYWxBcGlzQGdtYWlsLmNvbSJ9._8jwlusYZp9Ybp1PwsibXAAOQpRMEDPMw4uNSB9ahCE";
            if (string.IsNullOrWhiteSpace(token))
                new AuthenticationState(user);

            var tokenContent = _jwt.ReadJwtToken(token);
            user = new ClaimsPrincipal(new ClaimsIdentity(tokenContent.Claims, "jwt"));

            return await Task.FromResult(new AuthenticationState(user));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new AuthenticationState(user));
        }
    }

    public async Task LoggedIn()
    {
        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJWaWVyaXUgQWxleGFuZHJ1IiwianRpIjoiMTA1YjA1OGItNWUwNC00MjYyLWE1ZWUtNjI2M2FmNzk1MTEzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW5pc3RyYXRvciIsImV4cCI6MTY1MjA5NTU1NSwiaXNzIjoiY2xlYW5hcmhpdGVjdHVyZS5taW5pbWFsQXBpc0BnbWFpbC5jb20iLCJhdWQiOiJjbGVhbmFyaGl0ZWN0dXJlLm1pbmltYWxBcGlzQGdtYWlsLmNvbSJ9._8jwlusYZp9Ybp1PwsibXAAOQpRMEDPMw4uNSB9ahCE";
        var user = GetClaimsPrincipal(token);
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        var nobody = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(nobody));
        NotifyAuthenticationStateChanged(authState);
    }

    private ClaimsPrincipal GetClaimsPrincipal(string token)
    {
        var tokenContent = _jwt.ReadJwtToken(token);
        var user = new ClaimsPrincipal(new ClaimsIdentity(tokenContent.Claims, "jwt"));

        return user;
    }
}
