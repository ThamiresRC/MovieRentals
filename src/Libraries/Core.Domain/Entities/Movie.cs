namespace Core.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int Year { get; set; }
    public string Genre { get; set; } = "";
    public decimal DailyPrice { get; set; } = 7.90m;
}
