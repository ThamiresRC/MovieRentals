using Core.Domain.Entities;
using Core.Domain.Repositories;
using Core.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Repositories;

public class RentalRepository(MovieRentalsContext ctx) : IRentalRepository
{
    public async Task<Rental> AddAsync(Rental rental, CancellationToken ct)
    {
        ctx.Rentals.Add(rental);
        await ctx.SaveChangesAsync(ct);
        return rental;
    }

    public Task<Rental?> GetAsync(int id, CancellationToken ct)
        => ctx.Rentals.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<Rental?> ReturnAsync(int id, DateTime returnedAt, CancellationToken ct)
    {
        var r = await ctx.Rentals.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (r is null) return null;
        r.ReturnedAt = returnedAt;
        await ctx.SaveChangesAsync(ct);
        return r;
    }
}
