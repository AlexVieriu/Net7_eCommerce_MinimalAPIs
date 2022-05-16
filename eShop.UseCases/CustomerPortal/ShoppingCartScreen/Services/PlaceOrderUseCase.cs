using eShop.CoreBusiness.Services;

namespace eShop.UseCases.CustomerPortal.ShoppingCartScreen.Services;

public class PlaceOrderUseCase : IPlaceOrderUseCase
{
    private readonly IOrderService _orderService;
    private readonly IOrderRepositoryUI _orderRepoUI;
    private readonly IShoppingCart _shoppingCart;
    private readonly IShoppingCartStateStore _satestore;

    public PlaceOrderUseCase(IOrderService orderService,
                             IOrderRepositoryUI orderRepoUI,
                             IShoppingCart shoppingCart,
                             IShoppingCartStateStore satestore)
    {
        _orderService = orderService;
        _orderRepoUI = orderRepoUI;
        _shoppingCart = shoppingCart;
        _satestore = satestore;
    }

    public async Task<string> ExecuteAsync(string url, Order order)
    {
        if (_orderService.ValidateCustomerInformation(order))
        {
            order.DatePlaced = DateTime.Now;
            order.UniqueId = Guid.NewGuid().ToString();

            // Apelam Api-ul pentru a creea Order
            var isCreated = await _orderRepoUI.Create(url, order);
            await _shoppingCart.EmptyAsync();
            _satestore.BroadCastStateChange();

            if (isCreated)
                return order.UniqueId;
            else
                return String.Empty;
        }

        return String.Empty;
    }
}
