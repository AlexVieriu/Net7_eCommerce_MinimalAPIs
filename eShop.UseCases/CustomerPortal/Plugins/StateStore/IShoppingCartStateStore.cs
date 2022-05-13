namespace eShop.UseCases.CustomerPortal.Plugins.StateStore;

public interface IShoppingCartStateStore
{
    Task<int> GetItemsCount();
}
