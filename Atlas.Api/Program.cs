using Atlas.Api.Data;
using Atlas.Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Встроенный в .NET 10 генератор OpenAPI
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AtlasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Автоматический запуск сидера БД
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AtlasDbContext>();
        var env = services.GetRequiredService<IWebHostEnvironment>();
        await DbSeeder.SeedAsync(context, env);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during automatic database seeding.");
    }
}

// Включаем эндпоинт OpenAPI и визуальный интерфейс Scalar
app.MapOpenApi();
app.MapScalarApiReference();

// Автоматический переход с корня (/) на визуальный интерфейс
app.MapGet("/", async context =>
{
    context.Response.Redirect("/scalar/v1");
    await Task.CompletedTask;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();