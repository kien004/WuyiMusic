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
    public class UserFollowArtistsController : ControllerBase
    {
        private readonly IServices<UserFollowArtist> _services;

        public UserFollowArtistsController(IServices<UserFollowArtist> services)
        {
            _services = services;
        }

        // GET: api/UserFollowArtists
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<UserFollowArtist>>> GetUserFollowArtists()
        {
            return Ok(await _services.GetAllAsync());
        }

        // GET: api/UserFollowArtists/5
        [HttpGet("get-by-id")]
        public async Task<ActionResult<UserFollowArtist>> GetUserFollowArtist(Guid id)
        {
            var userFollowArtist = await _services.GetByIdAsync(id);

            if (userFollowArtist == null)
            {
                return NotFound();
            }

            return Ok(userFollowArtist);
        }

        // PUT: api/UserFollowArtists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutUserFollowArtist(UserFollowArtist userFollowArtist)
        {           
            try
            {
                var userFollowArtistEdit = await _services.GetByIdAsync(userFollowArtist.UserFollowArtistId);
                if (userFollowArtistEdit == null)
                {
                    return NotFound();
                }
                userFollowArtistEdit.UserId = userFollowArtist.UserId;
                userFollowArtistEdit.ArtistId = userFollowArtist.ArtistId;
                userFollowArtistEdit.FollowedAt = userFollowArtist.FollowedAt;
                await _services.UpdateAsync(userFollowArtistEdit);
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

        // POST: api/UserFollowArtists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<UserFollowArtist>> PostUserFollowArtist(UserFollowArtist userFollowArtist)
        {
            try
            {
                var createduserFollowArtistm = await _services.CreateAsync(userFollowArtist);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/UserFollowArtists/5
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUserFollowArtist(Guid id)
        {
            var userFollowArtist = await _services.GetByIdAsync(id);
            if (userFollowArtist == null)
            {
                return NotFound();
            }

            await _services.DeleteAsync(userFollowArtist);
            return Ok();
        }

        
    }
}
