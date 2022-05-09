namespace eShop.UseCases.Web.Infrastructure.Authentication;

public interface IAuthentificationRepository
{
    Task<bool> Register(User user);
    Task<bool> Login(string userLogin, string userPass);
    Task Logout();
}
