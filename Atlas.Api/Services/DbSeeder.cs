using Atlas.Api.Data;
using Atlas.Domain;
using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Atlas.Api.Services
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AtlasDbContext context, IWebHostEnvironment env)
        {
      
            await context.Database.MigrateAsync();

          
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

        
            if (!await context.Species.AnyAsync())
            {
                var speciesPath = GetFilePath(env, "species.json");
                if (File.Exists(speciesPath))
                {
                    var json = await File.ReadAllTextAsync(speciesPath);
                    var species = JsonSerializer.Deserialize<List<Species>>(json, options);
                    if (species != null && species.Count > 0)
                    {
                        await context.Species.AddRangeAsync(species);
                        await context.SaveChangesAsync();
                    }
                }
            }

           
            if (!await context.Site.AnyAsync())
            {
                var sitesPath = GetFilePath(env, "sites.json");
                if (File.Exists(sitesPath))
                {
                    var json = await File.ReadAllTextAsync(sitesPath);
                    var sites = JsonSerializer.Deserialize<List<Site>>(json, options);
                    if (sites != null && sites.Count > 0)
                    {
                        await context.Site.AddRangeAsync(sites);
                        await context.SaveChangesAsync();
                    }
                }
            }

         
            if (!await context.Artifacts.AnyAsync())
            {
                var artifactsPath = GetFilePath(env, "artifacts.json");
                if (File.Exists(artifactsPath))
                {
                    var json = await File.ReadAllTextAsync(artifactsPath);
                    var artifacts = JsonSerializer.Deserialize<List<Artifact>>(json, options);
                    if (artifacts != null && artifacts.Count > 0)
                    {
                        await context.Artifacts.AddRangeAsync(artifacts);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }

        private static string GetFilePath(IWebHostEnvironment env, string fileName)
        {
         
            var path = Path.Combine(env.ContentRootPath, "..", "data", fileName);
            if (File.Exists(path))
            {
                return path;
            }

          
            return Path.Combine(env.ContentRootPath, "data", fileName);
        }
    }
}