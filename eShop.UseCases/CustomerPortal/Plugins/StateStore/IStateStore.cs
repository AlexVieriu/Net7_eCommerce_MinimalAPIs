namespace eShop.UseCases.CustomerPortal.Plugins.StateStore;

public interface IStateStore
{
    void AddStateChangeListeners(Action listener);
    void RemoveStateChangeListeners(Action listener);
    void BroadCastStateChange();
}
