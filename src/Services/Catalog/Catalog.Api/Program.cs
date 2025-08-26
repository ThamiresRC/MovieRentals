using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.Api", Version = "v1" });
});

static IAsyncPolicy<HttpResponseMessage> RetryPolicy() =>
    HttpPolicyExtensions.HandleTransientHttpError()
        .WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(200 * Math.Pow(2, attempt)));

builder.Services.AddHttpClient("movies", c =>
{
    c.BaseAddress = new Uri("https://ghibliapi.vercel.app/");
    c.Timeout = TimeSpan.FromSeconds(5);
})
.AddPolicyHandler(RetryPolicy())
.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(3));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
