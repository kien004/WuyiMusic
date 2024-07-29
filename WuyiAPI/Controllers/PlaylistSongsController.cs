using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WuyiDAL.Models;
using WuyiServices.IServices;

namespace WuyiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistSongsController : ControllerBase
    {
        private readonly IServices<PlaylistSong> _services;

        public PlaylistSongsController(IServices<PlaylistSong> services)
        {
            _services = services;
        }

        // GET: api/PlaylistSongs
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<PlaylistSong>>> GetPlaylistSongs()
        {
            return Ok(await _services.GetAllAsync());
        }

        // GET: api/PlaylistSongs/5
        [HttpGet("get-by-id")]
        public async Task<ActionResult<PlaylistSong>> GetPlaylistSong(Guid id)
        {
            var playlistSong = await _services.GetByIdAsync(id);

            if (playlistSong == null)
            {
                return NotFound();
            }

            return Ok(playlistSong);
        }

        // PUT: api/PlaylistSongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutPlaylistSong(PlaylistSong playlistSong)
        {
            try
            {
                var playlistSongEdit = await _services.GetByIdAsync(playlistSong.PlaylistSongId);
                if (playlistSongEdit == null)
                {
                    return NotFound();
                }

                playlistSongEdit.PlaylistId = playlistSong.PlaylistId;
                playlistSongEdit.SongId = playlistSong.SongId;
                playlistSongEdit.AddedAt = playlistSong.AddedAt;

                await _services.UpdateAsync(playlistSongEdit);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Concurrency conflict occurred.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PlaylistSongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<PlaylistSong>> PostPlaylistSong(PlaylistSong playlistSong)
        {
            try
            {
                var createPlaylistSong = await _services.CreateAsync(playlistSong);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/PlaylistSongs/5
        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePlaylistSong(Guid id)
        {
            var playlistSong = await _services.GetByIdAsync(id);
            if (playlistSong == null)
            {
                return NotFound();
            }

            await _services.DeleteAsync(playlistSong);
            return Ok();
        }      
    }
}
