using Atlas.Api.Data;
using Atlas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtifactsController : ControllerBase
    {
        private readonly AtlasDbContext _context;

        public ArtifactsController(AtlasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artifact>>> GetArtifacts()
        {
            return await _context.Artifacts.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Artifact>> CreateArtifact(Artifact artifact)
        {
            _context.Artifacts.Add(artifact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtifacts), new { id = artifact.Id }, artifact);
        }
    }
}

