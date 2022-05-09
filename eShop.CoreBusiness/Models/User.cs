namespace eShop.CoreBusiness.Models;
public class User
{
    public int UserId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(20, ErrorMessage = "Number of characters need to be between {2} and {1}", MinimumLength = 3)]
    public string Password { get; set; }

    public string UserRole { get; set; }
}
