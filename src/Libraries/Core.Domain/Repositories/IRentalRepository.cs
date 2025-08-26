using Core.Domain.Entities;

namespace Core.Domain.Repositories;

public interface IRentalRepository
{
    Task<Rental> AddAsync(Rental rental, CancellationToken ct);
    Task<Rental?> GetAsync(int id, CancellationToken ct);
    Task<Rental?> ReturnAsync(int id, DateTime returnedAt, CancellationToken ct);
}
