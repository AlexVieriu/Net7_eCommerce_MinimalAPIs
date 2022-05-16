namespace eShop.CoreBusiness.Services;
public interface IOrderService
{
    bool ValidateCreateOrder(Order order);
    bool ValidateCustomerInformation(Order order);
    bool ValidateProcessOrder(Order order);
    bool ValidateUpdateOrder(Order order);
    bool ValidateOrder(Order order);
}
