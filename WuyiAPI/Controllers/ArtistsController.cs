using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WuyiDAL.Models;
using WuyiServices.IServices;

namespace WuyiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IServices<Artist> _services;

        public ArtistsController(IServices<Artist> services)
        {
            _services = services;
        }

        // GET: api/Artists
        [HttpGet("get-all")]
        public async Task<ActionResult<ICollection<Artist>>> GetArtists()
        {
            var artists = await _services.GetAllAsync();
            return Ok(artists);
        }

        // GET: api/Artists/5
        [HttpGet("get-by-id")]
        public async Task<ActionResult<Artist>> GetArtist(Guid id)
        {
            try
            {
                var artist = await _services.GetByIdAsync(id);
                if (artist == null)
                {
                    return NotFound();
                }
                return Ok(artist);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Artists
        [HttpPost("create")]
        public async Task<ActionResult> PostArtist(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _services.CreateAsync(artist);
                if (!result)
                {
                    return BadRequest("Could not create the artist.");
                }

                return CreatedAtAction(nameof(GetArtist), new { id = artist.ArtistId }, artist);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error.");
            }
        }
        // PUT: api/Artists/{id}
        [HttpPut("update")]
        public async Task<ActionResult> PutArtist(Artist artist)
        {
            try
            {

                var artistEdit = await _services.GetByIdAsync(artist.ArtistId);
                if (artistEdit == null)
                {
                    return NotFound();
                }
                artistEdit.Name = artist.Name;
                artistEdit.Bio = artist.Bio;
                artistEdit.Avatar = artist.Avatar;
                await _services.UpdateAsync(artistEdit);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log the exception (ex)
                return BadRequest();
            }
        }

        // DELETE: api/Artists/{id}
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteArtist(Guid id)
        {
            try
            {
                var artist = await _services.GetByIdAsync(id);
                if (artist == null)
                {
                    return NotFound();
                }

                var result = await _services.DeleteAsync(artist);
                if (!result)
                {
                    return BadRequest("Could not delete the artist.");
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
