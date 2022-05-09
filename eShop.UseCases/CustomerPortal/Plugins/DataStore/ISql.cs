namespace eShop.UseCases.CustomerPortal.Plugins.DataStore;
public interface ISql
{
    Task<IEnumerable<T>> LoadData<T, U>(string procedure, U parameters);
    Task SaveData<U>(string procedure, U parameters);
    Task<IEnumerable<T>> LoadDataTransaction<T, U>(string procedure, U parameters);
    Task SaveDataTransaction<U>(string procedure, U parameters);
    void StartTransaction();
    void CommitTransaction();
    void RollBackTransaction();
}
