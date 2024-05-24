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
    public class SongsController(FullContext CX) : ControllerBase
    {
        private readonly FullContext CX = CX;

        [HttpGet]
        public async Task<ActionResult<List<Song>>> GetSongs()
        {
            var songs = await CX.Songs.Include(s=>s.Artists).Include(s=>s.Album).ToListAsync();
            var SongsResponce = songs.Select(sng => new SongResponse
            {
                Id = sng.Id,
                Title = sng.Title,
                Album = new AlbumResponse
                {
                    Id = sng.Album.Id,
                    Description = sng.Album.Description,
                    Title = sng.Album.Title
                },
                Artists = sng.Artists.Select(art => new ArtistResponse
                {
                    Id = art.Id,
                    Name = art.Name,
                    Role = art.Role

                }).ToList()
            }
            ); 
            return Ok( SongsResponce ); 
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSong([FromBody] SongRequest request)
        {
            var album = CX.Albums.FirstOrDefault(b => b.Id == request.AlbumId);
            if (album != null)
            {
                var newSong = new Song()
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title
                };

                album.Songs.Add(newSong);

                foreach (var artistID in request.ArtistsId)
                {
                    var artist = CX.Artists.FirstOrDefault(a => a.Id == artistID);
                    artist?.Songs.Add(newSong);
                }
                await CX.Songs.AddAsync(newSong);
                await CX.SaveChangesAsync();

                foreach (var song in CX.Songs)
                {
                    Console.WriteLine($"song.Artists.Count = {song.Artists.Count} \n song.Album.Title = {song.Album.Title} ");
                }
                foreach (var song in CX.Artists)
                {
                    Console.WriteLine($"Artists.Songs.Count = {song.Songs.Count}");
                }
                foreach (var song in CX.Albums)
                {
                    Console.WriteLine($"Albums.Songs.Count = {song.Songs.Count}");
                }


                return Ok();
            }
            return NotFound();

        }

        [HttpPut("{id::guid}")]
        public async Task<ActionResult<Guid>> UpdateSong(Guid id, string NewTitle)
        {
            if (CX.Songs.Where(a => a.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Songs.
            Where(a => a.Id == id).
            ExecuteUpdateAsync(a => a
            .SetProperty(a => a.Title, a => NewTitle)
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
