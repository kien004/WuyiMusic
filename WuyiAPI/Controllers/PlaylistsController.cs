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
    public class PlaylistsController : ControllerBase
    {
        private readonly IServices<Playlist> _services;

        public PlaylistsController(IServices<Playlist> services)
        {
            _services = services;
        }

        // GET: api/Playlists
        [HttpGet("get-all")]
        public async Task<ActionResult<ICollection<Playlist>>> GetPlaylists()
        {
            var users = await _services.GetAllAsync();
            return Ok(users);
        }

        // GET: api/Playlists/{id}
        [HttpGet("get-by-id")]
        public async Task<ActionResult<Playlist>> GetPlaylist(Guid id)
        {
            try
            {
                var playlist = await _services.GetByIdAsync(id);
                if (playlist == null)
                {
                    return NotFound();
                }
                return Ok(playlist);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
        }



        // POST: api/Playlists
        [HttpPost("create")]
        public async Task<ActionResult> PostPlaylist(Playlist playlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _services.CreateAsync(playlist);
                if (!result)
                {
                    return BadRequest("Could not create the playlist.");
                }

                return CreatedAtAction(nameof(GetPlaylist), new { id = playlist.PlaylistId }, playlist);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error.");
            }
        }
        // PUT: api/Playlists/{id}
        [HttpPut("update")]
        public async Task<ActionResult> PutPlaylist(Playlist playlist)
        {
            try
            {

                var playlistEdit = await _services.GetByIdAsync(playlist.PlaylistId);
                if (playlistEdit == null)
                {
                    return NotFound();
                }
                playlistEdit.UserId = playlist.UserId;
                playlistEdit.Title = playlist.Title;
                await _services.UpdateAsync(playlistEdit);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log the exception (ex)
                return BadRequest();
            }
        }

        // DELETE: api/Playlists/{id}
        [HttpDelete("delete")]
        public async Task<ActionResult> DeletePlaylist(Guid id)
        {
            try
            {
                var playlist = await _services.GetByIdAsync(id);
                if (playlist == null)
                {
                    return NotFound();
                }

                var result = await _services.DeleteAsync(playlist);
                if (!result)
                {
                    return BadRequest("Could not delete the playlist.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
        }
    }
}
