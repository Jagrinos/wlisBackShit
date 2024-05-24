using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WLISBackend.models;
using WLISBackend.requests;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;
using WLISBackWITHOUTCOMMUNIKATION___.Response;

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
            var artists =  await CX.Artists.Include(a=> a.Songs).ThenInclude(S=>S.Album).ToListAsync();

            var artistsRequest = artists.Select(art => new ArtistResponse
            {
                Id = art.Id,
                Name = art.Name,
                Role = art.Role,
                Songs = art.Songs.Select(sng => new SongResponse
                {
                    Id = sng.Id,
                    Title = sng.Title,
                    Artists = sng.Artists.Select(art2 => new ArtistResponse
                    {
                        Id = art2.Id,
                        Name = art2.Name,
                        Role = art2.Role
                    }).ToList(),
                    Album = new AlbumResponse
                    {
                        Id = sng.Album.Id,
                        Description = sng.Album.Description,
                        Title = sng.Album.Title
                    }

                }).ToList()
            }).ToList();

            return Ok(artistsRequest);
        }
        

        [HttpPost]
        public async Task<ActionResult<ArtistResponse>> CreateArtist([FromBody] ArtistRequest request)
        {
            var newArtist = new Artist() { Id = Guid.NewGuid(), Name = request.Name, Role = request.Role};

            await CX.Artists.AddAsync(newArtist);
            await CX.SaveChangesAsync();

            var ArtistResponse = new ArtistResponse()
            {
                Id = newArtist.Id,
                Name = newArtist.Name,
                Role = newArtist.Role
            };
            return Ok(ArtistResponse);
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