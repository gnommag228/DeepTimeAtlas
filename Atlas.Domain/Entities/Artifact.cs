using System;
using System.Collections.Generic;
using System.Text;

namespace Atlas.Domain.Entities
{
    public class Artifact
    {
        public Guid Id { get; set; } = Guid.NewGuid();
      
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Era { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}