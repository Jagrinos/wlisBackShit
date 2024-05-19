using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WLISBackend.models;
using WLISBackend.requests;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;

namespace WLISBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController(FullContext CX) : ControllerBase
    {
        private readonly FullContext CX = CX;

        [HttpGet]
        public async Task<ActionResult<List<Artist>>> GetArtists()
        {
            return await CX.Artists.ToListAsync();
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateArtist([FromBody] ArtistRequest request)
        {
            var newArtist = new Artist() { Id = Guid.NewGuid(), Name = request.Name, Role = request.Role, Songs = new List<string>(request.Songs) };

            await CX.Artists.AddAsync(newArtist);
            await CX.SaveChangesAsync();

            return Ok(newArtist.Id);
        }

        [HttpPut("{id::guid}")]
        public async Task<ActionResult<Guid>> UpdateArtist(Guid id, [FromBody] ArtistRequest request)
        {
            if (CX.Artists.Where(a => a.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Artists.
            Where(a => a.Id == id).
            ExecuteUpdateAsync(a => a
            .SetProperty(a => a.Name, a => request.Name)
            .SetProperty(a => a.Role, a => request.Role)
            .SetProperty(a => a.Songs, new List<string>(request.Songs))
            );
            return Ok(id);

        }

        [HttpDelete("{id::guid}")]
        public async Task<ActionResult<Guid>> DeleteMessage(Guid id)
        {
            if (CX.Artists.Where(a => a.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Artists
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
            return Ok();
        }
    }
}