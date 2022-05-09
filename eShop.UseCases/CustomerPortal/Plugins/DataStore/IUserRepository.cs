namespace eShop.UseCases.CustomerPortal.Plugins.DataStore;
public interface IUserRepository
{
    Task<User> LoginUser(string userName, string pwd);
    Task<bool> RegisterUser(User user);
}
