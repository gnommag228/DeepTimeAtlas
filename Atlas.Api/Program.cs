using Atlas.Api.Data;
using Atlas.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



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
        logger.LogError(ex, "Помилка під час автоматичного заповнення бази даних.");
    }
}

        
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();