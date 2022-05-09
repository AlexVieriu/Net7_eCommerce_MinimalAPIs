namespace eShop.DataStore.SQL.Dapper;
public class OrderRepository : IOrderRepository
{
    private readonly ISql _sql;

    public OrderRepository(ISql sql)
    {
        _sql = sql;
    }
    public async Task<int> CreateOrderAsync(Order order)
    {
        try
        {
            _sql.StartTransaction();

            DynamicParameters orderParams = new();
            orderParams.Add("AdminUser", order.AdminUser);
            orderParams.Add("CustomerAddress", order.CustomerAddress);
            orderParams.Add("CustomerCity", order.CustomerCity);
            orderParams.Add("CustomerCountry", order.CustomerCountry);
            orderParams.Add("CustomerName", order.CustomerName);
            orderParams.Add("CustomerStateProvince", order.CustomerStateProvince);
            orderParams.Add("DatePlaced", order.DatePlaced);
            orderParams.Add("DateProcessing", order.DateProcessing);
            orderParams.Add("DateProcessed", order.DateProcessed);
            orderParams.Add("UniqueId", order.UniqueId);

            orderParams.Add("OrderId", DbType.Int32, direction: ParameterDirection.Output);

            await _sql.SaveDataTransaction("sp_CreateOrder", orderParams);

            var orderId = orderParams.Get<int>("OrderId");
            if (orderId < 0)
            {
                _sql.RollBackTransaction();
                return 0;
            }

            DynamicParameters lineItemsParams = new();

            foreach (var item in order.LineItems)
            {
                lineItemsParams.Add("Price", item.Price);
                lineItemsParams.Add("Quantity", item.Quantity);
                lineItemsParams.Add("ProductId", item.ProductId);
                lineItemsParams.Add("OrderId", item.OrderId);
                lineItemsParams.Add("LineItemId", DbType.Int32, direction: ParameterDirection.Output);

                await _sql.SaveDataTransaction("sp_CreateLineItem", lineItemsParams);

                var lineItemId = lineItemsParams.Get<int>("LineItemId");

                if (lineItemId < 0)
                {
                    _sql.RollBackTransaction();
                    return 0;
                }
            }

            _sql.CommitTransaction();
            return orderId;
        }
        catch (Exception ex)
        {
            _sql.RollBackTransaction();
            throw;
        }
    }

    public async Task<IEnumerable<OrderLineItem>> GetLineItemsByOrderIdAsync(int orderId)
    {
        try
        {
            _sql.StartTransaction();
            // get LineItems by order
            var lineItems = await _sql.LoadDataTransaction<OrderLineItem, dynamic>("sp_GetLineItemsByOrderId",
                                                                                   new { OrderId = orderId });

            // get product from eatch lineitmes
            var products = await _sql.LoadDataTransaction<Product, dynamic>("sp_GetLineItemsByOrderId",
                                                                            new { OrderId = orderId });
            _sql.CommitTransaction();

            foreach (var item in lineItems)
            {
                item.Product = (products.Where(q => q.ProductId == item.ProductId)).First();
            }

            return lineItems;
        }
        catch (Exception e)
        {
            _sql.RollBackTransaction();
            throw;
        }
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        try
        {
            _sql.StartTransaction();
            var order = (await _sql.LoadDataTransaction<Order, dynamic>("sp_GetOrderById",
                                                             new { OrderId = orderId })).First();

            var lineItems = await _sql.LoadDataTransaction<OrderLineItem, dynamic>("sp_GetLineItemsByOrderId",
                                                                        new { OrderId = orderId });

            var products = await _sql.LoadDataTransaction<Product, dynamic>("sp_GetLineItemsByOrderId",
                                                                 new { OrderId = orderId });

            _sql.CommitTransaction();

            order.LineItems = lineItems.ToList();

            foreach (var item in order.LineItems)
            {
                item.Product = products.Where(q => q.ProductId == item.ProductId).First();
            }

            return order;
        }
        catch (Exception ex)
        {
            _sql.RollBackTransaction();
            throw;
        }
    }

    public async Task<Order> GetOrderByUniqueIdAsync(string uniqueId)
    {
        try
        {
            _sql.StartTransaction();

            var order = (await _sql.LoadDataTransaction<Order, dynamic>("sp_GetOrderByUniqueId",
                                                                        new { UniqueId = uniqueId })).First();

            var lineItems = await _sql.LoadDataTransaction<OrderLineItem, dynamic>("sp_GetLineItemsByOrderId",
                                                                                    new { OrderId = order.OrderId });

            var products = await _sql.LoadDataTransaction<Product, dynamic>("sp_GetLineItemsByOrderId",
                                                                            new { OrderId = order.OrderId });

            _sql.CommitTransaction();

            order.LineItems = lineItems.ToList();

            foreach (var item in lineItems)
            {
                item.Product = products.Where(q => q.ProductId == item.ProductId).First();
            }

            return order;
        }
        catch (Exception ex)
        {
            _sql.RollBackTransaction();
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        try
        {
            var orders = await _sql.LoadData<Order, dynamic>("sp_GetOrders", new { });

            return orders;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetOutStrandingOrdersAsync()
    {
        try
        {
            var orders = await _sql.LoadData<Order, dynamic>("sp_GetOutStandingOrders", new { });

            return orders;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetProcessedOrdersAsync()
    {
        try
        {
            var orders = await _sql.LoadData<Order, dynamic>("sp_GetProcessedOrders", new { });

            return orders;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task UpdateOrderProcessedAsync(string adminUser, DateTime? dateProcced, int? orderId)
    {
        try
        {
            DynamicParameters orderParams = new();

            orderParams.Add("AdminUser", adminUser);
            orderParams.Add("DateProcced", dateProcced);
            orderParams.Add("OrderId", orderId);

            await _sql.SaveData<dynamic>("sp_UpdateProccedOrder", orderParams);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}
