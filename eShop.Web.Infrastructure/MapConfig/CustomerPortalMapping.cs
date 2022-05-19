namespace eShop.Web.Infrastructure.MapConfig;

public class CustomerPortalMapping : Profile
{
    public CustomerPortalMapping()
    {
        CreateMap<Order, CustomerUI>().ReverseMap();
    }
}
