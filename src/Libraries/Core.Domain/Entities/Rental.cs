namespace Core.Domain.Entities;

public class Rental
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string CustomerName { get; set; } = "";
    public DateTime RentedAt { get; set; } = DateTime.UtcNow;
    public DateTime DueAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
}
