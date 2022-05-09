namespace eShop.MinimalAPIs.Static;
public static class ErrorMsg
{
    public static string InternalServerError500(Exception ex)
    {
        return $"{ex.Message} - {ex.InnerException}";
    }
}
