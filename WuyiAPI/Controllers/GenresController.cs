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
    public class GenresController : ControllerBase
    {
        private readonly IServices<Genre> _services;

        public GenresController(IServices<Genre> services)
        {
            _services = services;
        }

        // GET: api/Genres
        [HttpGet("get-all")]
        public async Task<ActionResult<ICollection<Genre>>> GetGenres()
        {
            var genres = await _services.GetAllAsync();
            return Ok(genres);
        }

        // GET: api/Genres/{id}
        [HttpGet("get-by-id")]
        public async Task<ActionResult<Genre>> GetGenre(Guid id)
        {
            try
            {
                var genre = await _services.GetByIdAsync(id);
                if (genre == null)
                {
                    return NotFound();
                }
                return Ok(genre);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Genres
        [HttpPost("create")]
        public async Task<ActionResult> PostGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _services.CreateAsync(genre);
                if (!result)
                {
                    return BadRequest("Could not create the genre.");
                }

                return CreatedAtAction(nameof(GetGenre), new { id = genre.GenreId }, genre);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error.");
            }
        }
        // PUT: api/Genres/{id}
        [HttpPut("update")]
        public async Task<ActionResult> PutGenre(Genre genre)
        {
            try
            {

                var genreEdit = await _services.GetByIdAsync(genre.GenreId);
                if (genreEdit == null)
                {
                    return NotFound();
                }
                genreEdit.Name = genre.Name;
                await _services.UpdateAsync(genreEdit);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log the exception (ex)
                return BadRequest();
            }
        }

        // DELETE: api/Genres/{id}
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteGenre(Guid id)
        {
            try
            {
                var genre = await _services.GetByIdAsync(id);
                if (genre == null)
                {
                    return NotFound();
                }

                var result = await _services.DeleteAsync(genre);
                if (!result)
                {
                    return BadRequest("Could not delete the genre.");
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
