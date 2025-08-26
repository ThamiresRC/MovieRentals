using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Mvc.Controllers;

public class HomeController(IHttpClientFactory http) : Controller
{
    public async Task<IActionResult> Index(string? q = null, CancellationToken ct = default)
    {
        var client = http.CreateClient("catalog");
        var url = string.IsNullOrWhiteSpace(q) ? "/api/movies" : $"/api/movies?query={Uri.EscapeDataString(q)}";
        var json = await client.GetStringAsync(url, ct);
        var movies = JsonSerializer.Deserialize<List<MovieVm>>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? [];
        return View(movies);
    }

    [HttpPost]
    public async Task<IActionResult> Rent(int movieId, string title, string customer, int days, CancellationToken ct)
    {
        var client = http.CreateClient("rentals");
        var payload = JsonContent.Create(new { MovieId = movieId, CustomerName = customer, Days = days });
        await client.PostAsync("/api/rentals", payload, ct);
        return RedirectToAction("Index");
    }

    public record MovieVm(string externalId, string title, int year, string synopsis);
}
