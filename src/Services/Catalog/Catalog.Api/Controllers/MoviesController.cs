using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController(IHttpClientFactory httpFactory, ILogger<MoviesController> logger) : ControllerBase
{
    private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web);

    [HttpGet]
    public async Task<IActionResult> Get(string? query = null, CancellationToken ct = default)
    {
        var client = httpFactory.CreateClient("movies");
        var resp = await client.GetAsync("films", ct);
        resp.EnsureSuccessStatusCode();
        using var stream = await resp.Content.ReadAsStreamAsync(ct);
        var films = await JsonSerializer.DeserializeAsync<List<GhibliFilm>>(stream, JsonOpts, ct) ?? [];

        var result = films
            .Where(f => string.IsNullOrWhiteSpace(query) || f.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
            .Select(f => new MovieDto(f.Id, f.Title, int.TryParse(f.Release_date, out var y) ? y : 0, f.Description));

        logger.LogInformation("Returned {Count} movies", result.Count());
        return Ok(result);
    }

    private record GhibliFilm(string Id, string Title, string Description, string Release_date);
    public record MovieDto(string ExternalId, string Title, int Year, string Synopsis);
}
