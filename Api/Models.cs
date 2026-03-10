using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace WebApplication2;

public class LoginModel
{
    public string UserName { get; set; }

    public string  Password { get; set; }
}
public class CustomerModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string PersonalNumber { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }
}

public class ApplicationModel
{
    public decimal Amount { get; set; }

    public int CurrencyId { get; set; }

    public int TypeId { get; set; }

    public long CustomerId { get; set; }

    public DateTime ActiveTill { get; set; }

    public int StatusId { get; set; }
}