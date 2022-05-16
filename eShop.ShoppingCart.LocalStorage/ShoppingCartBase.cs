namespace eShop.ShoppingCart.LocalStorage;

public class ShoppingCartBase : IShoppingCart
{
    private const string shoppingCartName = "eShop.ShoppingCart";
    private readonly ILocalStorageService _localStorage;

    public ShoppingCartBase(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task AddProductToCartAsync(Product product)
    {
        var order = await GetOrderAsync();
        order.AddProduct(product.ProductId, 1, product.Price, product);
        await SetOrderAsync(order);
    }

    public async Task EmptyAsync()
    {
        await SetOrderAsync(null);
    }

    public async Task<Order> GetOrderAsync()
    {
        var order = await _localStorage.GetItemAsync<Order>(shoppingCartName);
        if (order == null)
        {
            order = new();
            await SetOrderAsync(order);
        }

        return order;
    }

    public async Task<Order> RemoveLineItemFromCartAsync(int productId)
    {
        var order = await GetOrderAsync();
        order.RemoveLineItem(productId);
        await SetOrderAsync(order);

        return order;
    }

    public async Task SetOrderAsync(Order order)
    {
        await _localStorage.SetItemAsync(shoppingCartName, order);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        await SetOrderAsync(order);
    }

    public async Task<Order> UpdateQuantityAsync(int productId, int quantity)
    {
        var order = await GetOrderAsync();
        if (order != null)
        {
            if (quantity < 0)
                return order;
            else if (quantity == 0)
                order = await RemoveLineItemFromCartAsync(productId);

            else
            {
                var lineItem = order.LineItems.Where(q => q.ProductId == productId).FirstOrDefault();
                if (lineItem != null)
                {
                    lineItem.Quantity = quantity;
                    lineItem.Price = Math.Round(quantity * lineItem.Product.Price, 2);
                }

                await SetOrderAsync(order);
            }
        }

        return order;
    }
}