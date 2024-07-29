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
    public class AlbumsController : ControllerBase
    {
        private readonly IServices<Album> _services;

        public AlbumsController(IServices<Album> services)
        {
            _services = services;
        }

        // GET: api/Albums
        [HttpGet("get-all")]
        public async Task<ActionResult<ICollection<Album>>> GetAlbums()
        {
            return Ok(await _services.GetAllAsync());
        }

        // GET: api/Albums/get-by-id/{id}
        [HttpGet("get-by-id")]
        public async Task<ActionResult<Album>> GetAlbum(Guid id)
        {
            var album = await _services.GetByIdAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        // PUT: api/Albums/update
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutAlbum(Album album)
        {
            try
            {
                var AlbumEdit = await _services.GetByIdAsync(album.AlbumId);
                if (AlbumEdit == null)
                {
                    return NotFound();
                }

                AlbumEdit.ArtistId = album.ArtistId;
                AlbumEdit.Title = album.Title;
                AlbumEdit.CoverImage = album.CoverImage;
                AlbumEdit.ReleaseDate = album.ReleaseDate;

                await _services.UpdateAsync(AlbumEdit);
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

        // POST: api/Albums/create
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<Album>> PostAlbum(Album album)
        {
            try
            {
                var createdAlbum = await _services.CreateAsync(album);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Albums/delete/{id}
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var album = await _services.GetByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            await _services.DeleteAsync(album);
            return Ok();
        }
    }
}
