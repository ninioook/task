using System.ComponentModel.DataAnnotations;

namespace Core;

public class RegisterCommand
{
    public string Name { get; set; }
    
    public string LastName { get; set; }
   
    public string PersonalNumber { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public string Password { get; set; }
}

public class AddApplicationCommand
{
    public decimal Amount { get; set; }

    public int CurrencyId { get; set; }

    public int TypeId { get; set; }

    public long CustomerId { get; set; }

    public DateTime ActiveTill { get; set; }

    public int StatusId { get; set; }
}