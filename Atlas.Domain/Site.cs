using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Atlas.Domain.Entities
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public int SpeciesId { get; set; }
        public Species? Species { get; set; }
        public string? AlternativeTaxonomy { get; set; }
    }
}