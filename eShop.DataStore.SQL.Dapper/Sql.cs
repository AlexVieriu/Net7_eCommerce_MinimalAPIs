namespace eShop.DataStore.SQL.Dapper;

public class Sql : ISql
{
    private const string connectionName = "eCommerce";
    private readonly IConfiguration _config;

    public Sql(IConfiguration config)
    {
        _config = config;
    }

    // Single Procedure
    public async Task<IEnumerable<T>> LoadData<T, U>(string procedure, U parameters)
    {
        try
        {
            var connString = _config.GetConnectionString(connectionName);
            using IDbConnection connection = new SqlConnection(connString);

            var entities = await connection.QueryAsync<T>(procedure,
                                                          parameters,
                                                          commandType: CommandType.StoredProcedure);

            return entities;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task SaveData<U>(string procedure, U parameters)
    {
        try
        {
            var connString = _config.GetConnectionString(connectionName);
            using IDbConnection connection = new SqlConnection(connString);

            await connection.ExecuteAsync(procedure,
                                          parameters,
                                          commandType: CommandType.StoredProcedure);
        }
        catch (Exception)
        {
            throw;
        }
    }

    // Multiple Store Procedure
    private IDbConnection? _connection;
    private IDbTransaction? _transaction;
    private bool isClosed = false;

    public void StartTransaction()
    {
        string? connectionString = _config.GetConnectionString(connectionName);

        _connection = new SqlConnection(connectionString);
        _connection.Open();

        _transaction = _connection.BeginTransaction();

        isClosed = false;
    }

    public void CommitTransaction()
    {
        _transaction?.Commit();
        _connection?.Close();

        isClosed = true;
    }

    public void RollBackTransaction()
    {
        _transaction?.Rollback();
        _connection?.Close();

        isClosed = true;
    }

    public void Dispose()
    {
        if (isClosed == false)
        {
            try
            {
                CommitTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }

        _transaction = null;
        _connection = null;
    }

    public async Task<IEnumerable<T>> LoadDataTransaction<T,U>(string procedure, U parameters)
    {
        try
        {
            return await _connection.QueryAsync<T>(procedure,
                                                   parameters,
                                                   commandType: CommandType.StoredProcedure,
                                                   transaction: _transaction);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task SaveDataTransaction<U>(string procedure, U parameters)
    {
        try
        {
            await _connection.ExecuteAsync(procedure,
                                           parameters,
                                           commandType: CommandType.StoredProcedure,
                                           transaction: _transaction);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
