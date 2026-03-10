namespace Domain.Entities;

public class Application
{
    public long Id { get; set; }

    public decimal Amount { get; set; }

    public int CurrencyId { get; set; }

    public int TypeId { get; set; }

    public long CustomerId { get; set; }

    public DateTime ActiveUntil { get; set; }

    public DateTime CreateDate { get; set; }

    public int StatusId { get; set; }
}