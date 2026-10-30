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
        public string Species { get; set; } = string.Empty;
        public string? AlternativeTaxonomy { get; set; }
    }
}