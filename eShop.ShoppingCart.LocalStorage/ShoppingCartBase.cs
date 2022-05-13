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
        Order order;
        var orderStr = await _localStorage.GetItemAsync<string>(shoppingCartName);
        if (string.IsNullOrWhiteSpace(orderStr) || orderStr.ToLower() == "null")
        {
            order = new();
            await SetOrderAsync(order);
        }
        else
            order = JsonConvert.DeserializeObject<Order>(shoppingCartName);

        return order;
    }

    public async Task<Order> RemoveProductFromCartAsync(int productId)
    {
        var order = await GetOrderAsync();
        order.RemoveProduct(productId);
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
                order = await RemoveProductFromCartAsync(productId);

            else
            {
                var lineItem = order.LineItems.Where(q => q.ProductId == productId).FirstOrDefault();
                if (lineItem != null)
                    lineItem.Quantity = quantity;
                await SetOrderAsync(order);
            }
        }

        return order;
    }
}