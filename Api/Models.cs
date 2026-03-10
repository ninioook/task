using System.ComponentModel.DataAnnotations;

namespace WebApplication2;

public class LoginModel
{
    public string UserName { get; set; }

    public string Password { get; set; }
}
public class CustomerModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string PersonalNumber { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    public string Password { get; set; }
}

public class ApplicationModel
{
    [Required]
    public decimal? Amount { get; set; }

    [Required]
    public int? CurrencyId { get; set; }

    [Required]
    public int? TypeId { get; set; }

    [Required]
    public long? CustomerId { get; set; }

    public DateTime? ActiveUntil { get; set; }
}