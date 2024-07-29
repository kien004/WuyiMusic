using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WuyiDAL.Models;
using WuyiServices.IServices;

namespace WuyiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly IServices<Song> _services;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SongsController(IServices<Song> services, IWebHostEnvironment webHostEnvironment)
        {
            _services = services;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Songs/get-all
        [HttpGet("get-all")]
        public async Task<ActionResult<ICollection<Song>>> GetSongs()
        {
            try
            {
                return Ok(await _services.GetAllAsync());
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Songs/get-by-id/{id}
        [HttpGet("get-by-id")]
        public async Task<ActionResult<Song>> GetSong(Guid id)
        {
            try
            {
                var song = await _services.GetByIdAsync(id);
                if (song == null)
                {
                    return NotFound();
                }
                return Ok(song);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Songs/update
        [HttpPut("update")]
        public async Task<IActionResult> PutSong(Song song)
        {
            try
            {
                var SongEdit = await _services.GetByIdAsync(song.SongId);
                if (SongEdit == null)
                {
                    return NotFound();
                }
                SongEdit.AlbumId = song.AlbumId;
                SongEdit.Title = song.Title;
                SongEdit.Duration = song.Duration;
                SongEdit.FilePath = song.FilePath;
                await _services.UpdateAsync(SongEdit);
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest("Concurrency error occurred while updating the song.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Songs/create
        [HttpPost("create")]
        public async Task<ActionResult<Song>> PostSong( Song song, IFormFile songFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var songs = new Song
                {
                    SongId= new Guid(),
                    ArtistId = song.ArtistId,
                    AlbumId = song.AlbumId,
                    GenreId = song.GenreId,
                    Title = song.Title,
                    Duration = song.Duration,
                    FilePath = song.FilePath,
                    CreatedAt= DateTime.UtcNow,  
                    UpdatedAt= DateTime.UtcNow,
                    
                    // You can set CreatedAt and UpdatedAt in the service or here as needed
                };
                var createdSong = await _services.CreateAsync(songs);
                    return Ok(createdSong);
                
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        // DELETE: api/Songs/delete/{id}
        [HttpGet("delete")]
        public async Task<IActionResult> DeleteSong(Guid id)
        {
            try
            {
                var song = await _services.GetByIdAsync(id);
                if (song == null)
                {
                    return NotFound();
                }
                return Ok(await _services.DeleteAsync(song));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
