namespace eShop.UseCases.CustomerPortal.Plugins.StateStore;

public interface IShoppingCartStateStore : IStateStore
{
    Task<int> GetItemsCount();
}
