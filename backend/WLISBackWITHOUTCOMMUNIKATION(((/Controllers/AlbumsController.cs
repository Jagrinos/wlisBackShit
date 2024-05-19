using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WLISBackend.models;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;
using WLISBackWITHOUTCOMMUNIKATION___.requests;

namespace WLISBackWITHOUTCOMMUNIKATION___.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumsController(FullContext CX) : ControllerBase
    {
        private readonly FullContext CX = CX;

        [HttpGet]
        public async Task<ActionResult<List<Album>>> GetAlbums()
        {
            return await CX.Albums.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAlbum([FromBody] AlbumRequest request)
        {
            var newAlbum = new Album(){ Id = Guid.NewGuid(), Title = request.Title, Description = request.Description, Songs = new List<string>(request.Songs) };

            await CX.Albums.AddAsync(newAlbum);
            await CX.SaveChangesAsync();

            return Ok(newAlbum.Id);
        }

        [HttpPut("{id::guid}")]
        public async Task<ActionResult<Guid>> UpdateAlbum(Guid id, [FromBody] AlbumRequest request)
        {
            if (CX.Albums.Where(a => a.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Albums.
            Where(a => a.Id == id).
            ExecuteUpdateAsync(a => a
            .SetProperty(a => a.Title, a => request.Title)
            .SetProperty(a => a.Description, a => request.Description)
            .SetProperty(a => a.Songs, new List<string>(request.Songs))
            );
            return Ok(id);

        }

        [HttpDelete("{id::guid}")]
        public async Task<ActionResult<Guid>> DeleteMessage(Guid id)
        {
            if (CX.Albums.Where(a => a.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Albums
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
            return Ok();
        }
    }
}
