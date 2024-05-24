using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WLISBackend.models;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;
using WLISBackWITHOUTCOMMUNIKATION___.requests;
using WLISBackWITHOUTCOMMUNIKATION___.Response;

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
            var albums =  await CX.Albums.Include(a => a.Songs).ThenInclude(s => s.Artists).ToListAsync();

            var AlbumsResponse = albums.Select(alb => new AlbumResponse
            {
                Id = alb.Id,
                Title = alb.Title,
                Description = alb.Description,
                Songs = alb.Songs.Select(sng => new SongResponse
                {
                    Id = sng.Id,
                    Title = sng.Title,
                    Artists = sng.Artists.Select(art => new ArtistResponse
                    {
                        Id = art.Id,
                        Name = art.Name,
                        Role = art.Role
                    }).ToList(),
                    
                }).ToList()
            });

            return Ok(AlbumsResponse);
        }

        [HttpPost]
        public async Task<ActionResult<AlbumResponse>> CreateAlbum([FromBody] AlbumRequest request)
        {
            var newAlbum = new Album(){ Id = Guid.NewGuid(), Title = request.Title, Description = request.Description };

            await CX.Albums.AddAsync(newAlbum);
            await CX.SaveChangesAsync();

            var AlbumResponse = new AlbumResponse()
            {
                Id = newAlbum.Id,
                Title = newAlbum.Title,
                Description = newAlbum.Description
            };

            return Ok(AlbumResponse);
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
