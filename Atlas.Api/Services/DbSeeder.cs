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
            //await context.Database.MigrateAsync();

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
                    var speciesList = JsonSerializer.Deserialize<List<Species>>(json, options);
                    if (speciesList != null && speciesList.Count > 0)
                    {
                        await context.Species.AddRangeAsync(speciesList);
                        await context.SaveChangesAsync(); 
                    }
                }
            }

            
            var sitesPath = GetFilePath(env, "sites.json");
            if (File.Exists(sitesPath))
            {
                var json = await File.ReadAllTextAsync(sitesPath);
                var siteDtos = JsonSerializer.Deserialize<List<SiteJsonDto>>(json, options);

                if (siteDtos != null && siteDtos.Count > 0)
                {
                    var existingSiteNames = await context.Site
                        .Select(s => s.Name)
                        .ToListAsync();

                    
                    var allSpecies = await context.Species.ToListAsync();

                    foreach (var dto in siteDtos.Where(s => !existingSiteNames.Contains(s.Name)))
                    {
                        
                        var matchedSpecies = allSpecies.FirstOrDefault(sp =>
                            sp.Name.Equals(dto.Species, StringComparison.OrdinalIgnoreCase));

                        var site = new Site
                        {
                            Name = dto.Name,
                            Location = dto.Location,
                            Age = dto.Age,
                            
                            SpeciesId = matchedSpecies != null ? matchedSpecies.Id : 1,
                            AlternativeTaxonomy = dto.AlternativeTaxonomy
                        };

                        await context.Site.AddAsync(site);
                    }

                    await context.SaveChangesAsync();
                }
            }


            if (!await context.Artifacts.AnyAsync())
            {
                var artifactsPath = GetFilePath(env, "artifacts.json");
                if (File.Exists(artifactsPath))
                {
                    var json = await File.ReadAllTextAsync(artifactsPath);
                    try
                    {
                        var artifacts = JsonSerializer.Deserialize<List<Artifact>>(json, options);
                        if (artifacts != null && artifacts.Count > 0)
                        {
                            await context.Artifacts.AddRangeAsync(artifacts);
                            await context.SaveChangesAsync();
                        }
                    }
                    catch (JsonException)
                    {
                        var singleArtifact = JsonSerializer.Deserialize<Artifact>(json, options);
                        if (singleArtifact != null)
                        {
                            context.Artifacts.Add(singleArtifact);
                            await context.SaveChangesAsync();
                        }
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
    public class SiteJsonDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string? AlternativeTaxonomy { get; set; }
    }

}