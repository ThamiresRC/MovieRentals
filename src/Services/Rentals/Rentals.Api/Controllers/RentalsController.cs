using Core.Application.DTOs;
using Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Rentals.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalsController(IRentalService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<RentalResponse>> Create(CreateRentalRequest req, CancellationToken ct)
        => Ok(await service.CreateAsync(req, ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RentalResponse>> Get(int id, CancellationToken ct)
        => (await service.GetAsync(id, ct)) is { } r ? Ok(r) : NotFound();

    [HttpPatch("{id:int}/return")]
    public async Task<ActionResult<RentalResponse>> Return(int id, CancellationToken ct)
        => (await service.ReturnAsync(id, ct)) is { } r ? Ok(r) : NotFound();
}
