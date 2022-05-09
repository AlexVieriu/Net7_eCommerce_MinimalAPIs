namespace eShop.DataStore.SQL.Dapper;
public class UserRepository : IUserRepository
{
    private readonly ISql _sql;

    public UserRepository(ISql sql)
    {
        _sql = sql;
    }

    public async Task<User> LoginUser(string userName, string pwd)
    {
        try
        {
            var user = (await _sql.LoadData<User, dynamic>("sp_LoginUser",
                                                           new { UserName = userName, Password = pwd })).FirstOrDefault();

            return user;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> RegisterUser(User user)
    {
        try
        {
            DynamicParameters registerParams = new();
            registerParams.Add("UserName", user.UserName);
            registerParams.Add("Password", user.Password);
            registerParams.Add("UserRole", user.UserRole);
            registerParams.Add("UserId", DbType.Int32, direction: ParameterDirection.Output);

            await _sql.SaveData<dynamic>("sp_RegisterUser", registerParams);

            user.UserId = registerParams.Get<int>("UserId");

            return user.UserId > 0;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
