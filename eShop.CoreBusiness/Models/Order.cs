namespace eShop.CoreBusiness.Models;

public class Order
{
    public Order()
    {
        LineItems = new();
    }

    public int? OrderId { get; set; }
    public DateTime? DatePlaced { get; set; }
    public DateTime? DateProcessing { get; set; }
    public DateTime? DateProcessed { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerCity { get; set; }
    public string CustomerStateProvince { get; set; }
    public string CustomerCountry { get; set; }
    public string AdminUser { get; set; }
    public string UniqueId { get; set; }

    public List<OrderLineItem> LineItems { get; set; }

    public void AddProduct(int productId, int qty, double price, Product product)
    {
        var item = LineItems.Where(q => q.ProductId == productId).FirstOrDefault();

        if (qty > 0 && price > 0)
        {
            if (item == null)
                LineItems.Add(new OrderLineItem() { ProductId = productId, Price = price, Quantity = qty, Product = product });
            else
                item.Quantity += qty;
        }
    }

    public void RemoveProduct(int productId)
    {
        var item = LineItems.Where(q => q.ProductId == productId).FirstOrDefault();

        if (item != null)
            LineItems.Remove(item);
    }
}
