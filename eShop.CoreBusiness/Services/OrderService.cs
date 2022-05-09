namespace eShop.CoreBusiness.Services;
public class OrderService : IOrderService
{
    public bool ValidateCreateOrder(Order order)
    {
        return ValidateOrder(order);
    }

    public bool ValidateCustomerInformation(string name, string address, string city, string province, string country)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(address) ||
            string.IsNullOrWhiteSpace(city) ||
            string.IsNullOrWhiteSpace(province) ||
            string.IsNullOrWhiteSpace(country))
            return false;
        return true;
    }

    public bool ValidateProcessOrder(Order order)
    {
        if (!order.DateProcessed.HasValue || string.IsNullOrWhiteSpace(order.AdminUser))
            return false;

        return true;
    }

    public bool ValidateUpdateOrder(Order order)
    {
        if (order.DateProcessing.HasValue == false || order.DateProcessed.HasValue == true)
            return false;

        if (string.IsNullOrWhiteSpace(order.UniqueId))
            return false;

        return ValidateOrder(order);
    }

    public bool ValidateOrder(Order order)
    {
        // order is null
        if (order == null)
            return false;

        // line items are null or <0
        if (order.LineItems == null || order.LineItems.Count <= 0)
            return false;

        // price, qty, productId< 0(in LineItems) 
        foreach (var item in order.LineItems)
        {
            if (item.Price < 0 || item.Quantity < 0 || item.ProductId < 0)
                return false;
        }

        // validate CustomerInformation
        if (!ValidateCustomerInformation(order.CustomerAddress,
                                        order.CustomerCity,
                                        order.CustomerCountry,
                                        order.CustomerName,
                                        order.CustomerStateProvince))
            return false;

        return true;
    }
}
