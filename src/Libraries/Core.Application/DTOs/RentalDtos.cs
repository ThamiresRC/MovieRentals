namespace Core.Application.DTOs;

public record CreateRentalRequest(int MovieId, string CustomerName, int Days);
public record RentalResponse(int Id, int MovieId, string CustomerName, DateTime RentedAt, DateTime DueAt, DateTime? ReturnedAt);
