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
    public class UsersController : ControllerBase
    {
        private readonly IServices<User> _services;
        private readonly IServices<Artist> _ArtistServices;
        public UsersController(IServices<User> services, IServices<Artist> services2)
        {
            _services = services;
            _ArtistServices = services2;
        }

        // GET: api/Users
        [HttpGet("get-all")]
        public async Task<ActionResult<ICollection<User>>> GetUsers()
        {
            var users = await _services.GetAllAsync();
            return Ok(users);
        }

        // GET: api/Users/{id}
        [HttpGet("get-by-id")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            try
            {
                var user = await _services.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
        }



        // POST: api/Users
        [HttpPost("create")]
        public async Task<ActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Thực hiện tạo mới người dùng
                var result = await _services.CreateAsync(user);
                if (!result)
                {
                    return BadRequest("Could not create the user.");
                }

                // Nếu người dùng là nghệ sĩ
                if (user.IsArtist)
                {
                    // Tạo mới đối tượng nghệ sĩ và thêm vào cơ sở dữ liệu
                    Artist artist = new Artist
                    {
                        ArtistId = user.UserId,
                        Name = user.Username, // Giả định tên nghệ sĩ giống với tên đăng nhập
                        Bio = "", // Mô tả mặc định hoặc từ dữ liệu người dùng
                        Avatar = "", // Hình đại diện mặc định hoặc từ dữ liệu người dùng
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    };
                    await _ArtistServices.CreateAsync(artist);
                }

                // Trả về thành công và đường dẫn đến người dùng vừa tạo
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                return StatusCode(500, "Internal server error.");
            }
        }

        // PUT: api/Users/{id}
        [HttpPut("update")]
        public async Task<ActionResult> PutUser(User user)
        {
            try
            {
                // Lấy thông tin người dùng từ dịch vụ
                var userEdit = await _services.GetByIdAsync(user.UserId);
                if (userEdit == null)
                {
                    return NotFound();
                }

                // Lưu trữ trạng thái IsArtist ban đầu
                bool wasArtist = userEdit.IsArtist;

                // Cập nhật thông tin người dùng từ dữ liệu đầu vào
                userEdit.Username = user.Username;
                userEdit.Email = user.Email;
                userEdit.Password = user.Password;
                userEdit.IsArtist = user.IsArtist;

                // Thực hiện cập nhật người dùng
                await _services.UpdateAsync(userEdit);

                // Nếu từ người thường chuyển sang nghệ sĩ
                if (!wasArtist && userEdit.IsArtist)
                {
                    // Tạo mới đối tượng nghệ sĩ và thêm vào cơ sở dữ liệu
                    Artist artist = new Artist
                    {
                        ArtistId = userEdit.UserId,
                        Name = userEdit.Username, // Giả định tên nghệ sĩ giống với tên đăng nhập
                        Bio = "", // Mô tả mặc định hoặc từ dữ liệu người dùng
                        Avatar = "", // Hình đại diện mặc định hoặc từ dữ liệu người dùng
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    };
                    await _ArtistServices.CreateAsync(artist);
                }
                // Nếu từ nghệ sĩ chuyển về người thường
                else if (wasArtist && !userEdit.IsArtist)
                {
                    // Kiểm tra và xóa thông tin nghệ sĩ nếu tồn tại
                    var artist = await _ArtistServices.GetByIdAsync(userEdit.UserId);
                    if (artist != null)
                    {
                        await _ArtistServices.DeleteAsync(artist);
                    }
                }

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Xử lý lỗi cập nhật
                return BadRequest();
            }
        }


        // DELETE: api/Users/{id}
        [HttpGet("delete")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _services.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                if (user.IsArtist)
                {
                    await _ArtistServices.DeleteAsync(await _ArtistServices.GetByIdAsync(id));
                }
                var result = await _services.DeleteAsync(user);
                if (!result)
                {
                    return BadRequest("Could not delete the user.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("CheckUsername")]
        public async Task<IActionResult> CheckUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username is required.");
            }

            bool usernameExists = await _services.UsernameExistsAsync(username);
            if (usernameExists)
            {
                return Conflict("Username already exists.");
            }

            return Ok("Username is available.");
        }

    }
}
