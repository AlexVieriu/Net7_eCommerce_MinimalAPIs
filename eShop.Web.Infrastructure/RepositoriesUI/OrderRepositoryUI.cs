namespace eShop.Web.Infrastructure.RepositoriesUI;

public class OrderRepositoryUI : BaseRepositoryUI<Order>, IOrderRepositoryUI
{
    public OrderRepositoryUI(HttpClient client, ILocalStorageService localStorage)
        : base(client, localStorage)
    {

    }


}
