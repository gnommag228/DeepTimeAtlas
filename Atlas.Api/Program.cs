using Atlas.Api.Data;
using Atlas.Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddOpenApi();

builder.Services.AddDbContext<AtlasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


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


app.MapOpenApi();
app.MapScalarApiReference();


app.MapGet("/", async context =>
{
    context.Response.Redirect("/scalar/v1");
    await Task.CompletedTask;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();