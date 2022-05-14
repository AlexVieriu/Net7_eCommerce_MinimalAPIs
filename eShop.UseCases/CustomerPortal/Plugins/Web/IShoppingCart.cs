namespace eShop.UseCases.CustomerPortal.Plugins.Web;

public interface IShoppingCart
{
    Task<Order> GetOrderAsync();
    Task AddProductToCartAsync(Product product);
    Task<Order> RemoveLineItemFromCartAsync(int productId);
    Task EmptyAsync();
    Task UpdateOrderAsync(Order order);
    Task<Order> UpdateQuantityAsync(int productId, int quantity);
    Task SetOrderAsync(Order order);
}
