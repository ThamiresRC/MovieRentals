using Core.Application.DTOs;
using Core.Domain.Entities;
using Core.Domain.Repositories;

namespace Core.Application.Services;

public interface IRentalService
{
    Task<RentalResponse> CreateAsync(CreateRentalRequest req, CancellationToken ct);
    Task<RentalResponse?> GetAsync(int id, CancellationToken ct);
    Task<RentalResponse?> ReturnAsync(int id, CancellationToken ct);
}

public class RentalService(IRentalRepository repo) : IRentalService
{
    public async Task<RentalResponse> CreateAsync(CreateRentalRequest req, CancellationToken ct)
    {
        var rental = new Rental
        {
            MovieId = req.MovieId,
            CustomerName = req.CustomerName,
            RentedAt = DateTime.UtcNow,
            DueAt = DateTime.UtcNow.AddDays(req.Days)
        };
        var saved = await repo.AddAsync(rental, ct);
        return ToDto(saved);
    }

    public async Task<RentalResponse?> GetAsync(int id, CancellationToken ct)
        => (await repo.GetAsync(id, ct)) is { } r ? ToDto(r) : null;

    public async Task<RentalResponse?> ReturnAsync(int id, CancellationToken ct)
        => (await repo.ReturnAsync(id, DateTime.UtcNow, ct)) is { } r ? ToDto(r) : null;

    private static RentalResponse ToDto(Rental r)
        => new(r.Id, r.MovieId, r.CustomerName, r.RentedAt, r.DueAt, r.ReturnedAt);
}
