namespace Domain.Entities;

public class Customer
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string PersonalNumber { get; set; }

    public DateTime BirthDate { get; set; }

    public string Password { get; set; }

    public int StatusId { get; set; }
}
