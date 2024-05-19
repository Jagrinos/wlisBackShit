using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WLISBackend.models;
using WLISBackend.requests;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;

namespace WLISBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongsController(FullContext CX) : ControllerBase
    {
        private readonly FullContext CX = CX;

        [HttpGet]
        public async Task<ActionResult<List<Song>>> GetSongs()
        {
            return await CX.Songs.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSong([FromBody] SongRequest request)
        {
            var newSong = new Song() { Id = Guid.NewGuid(), Title = request.Title, Albums = new List<string>(request.Albums), Artists = new List<string>(request.Artists) };

            await CX.Songs.AddAsync(newSong);
            await CX.SaveChangesAsync();

            return Ok(newSong.Id);
        }

        [HttpPut("{id::guid}")]
        public async Task<ActionResult<Guid>> UpdateSong(Guid id, [FromBody] SongRequest request)
        {
            if (CX.Songs.Where(a => a.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Songs.
            Where(a => a.Id == id).
            ExecuteUpdateAsync(a => a
            .SetProperty(a => a.Title, a => request.Title)
            .SetProperty(a => a.Artists, new List<string>(request.Artists))
            .SetProperty(a => a.Albums, new List<string>(request.Albums))
            );
            return Ok(id);

        }

        [HttpDelete("{id::guid}")]
        public async Task<ActionResult<Guid>> DeleteMessage(Guid id)
        {
            if (CX.Songs.Where(a => a.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Songs
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
            return Ok();
        }
    }
}
