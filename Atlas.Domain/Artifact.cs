using System.Text.Json.Serialization;

namespace Atlas.Domain.Entities
{
    public class Artifact
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        public int SiteId { get; set; }

        [JsonIgnore]
        public Site? Site { get; set; }
    }
}