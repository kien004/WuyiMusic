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
    public class UserLikedSongsController : ControllerBase
    {
        private readonly IServices<UserLikedSong> _services;

        public UserLikedSongsController(IServices<UserLikedSong> services)
        {
            _services = services;
        }

        // GET: api/UserLikedSongs
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<UserLikedSong>>> GetUserLikedSongs()
        {
            return Ok(await _services.GetAllAsync());
        }

        // GET: api/UserLikedSongs/5
        [HttpGet("get-by-id")]
        public async Task<ActionResult<UserLikedSong>> GetUserLikedSong(Guid id)
        {
            var userLikedSong = await _services.GetByIdAsync(id);

            if (userLikedSong == null)
            {
                return NotFound();
            }

            return Ok(userLikedSong);
        }

        // PUT: api/UserLikedSongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutUserLikedSong(UserLikedSong userLikedSong)
        {
            try
            {
                var userLikedSongEdit = await _services.GetByIdAsync(userLikedSong.UserLikedSongId);
                if (userLikedSongEdit == null)
                {
                    return NotFound();
                }

                userLikedSongEdit.UserId = userLikedSong.UserId;
                userLikedSongEdit.SongId = userLikedSong.SongId;
                userLikedSongEdit.LikedAt = userLikedSong.LikedAt;

                await _services.UpdateAsync(userLikedSongEdit);
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

        // POST: api/UserLikedSongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<UserLikedSong>> PostUserLikedSong(UserLikedSong userLikedSong)
        {
            try
            {
                var createUserLikedSong = await _services.CreateAsync(userLikedSong);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/UserLikedSongs/5
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUserLikedSong(Guid id)
        {
            var userLikedSong = await _services.GetByIdAsync(id);
            if (userLikedSong == null)
            {
                return NotFound();
            }

            await _services.DeleteAsync(userLikedSong);
            return Ok();
        }
    }
}
