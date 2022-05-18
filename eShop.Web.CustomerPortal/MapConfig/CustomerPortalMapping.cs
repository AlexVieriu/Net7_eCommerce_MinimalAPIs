namespace eShop.Web.CustomerPortal.MapConfig;

public class CustomerPortalMapping : Profile
{
    public CustomerPortalMapping()
    {
        CreateMap<Order, CustomerUI>().ReverseMap();
    }
}
