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
    public class NotificationsController : ControllerBase
    {
        private readonly IServices<Notification> _services;

        public NotificationsController(IServices<Notification> services)
        {
            _services = services;
        }

        // GET: api/Notifications
        [HttpGet("get-all")]
        public async Task<ActionResult<ICollection<Notification>>> GetNotifications()
        {
            var notifications = await _services.GetAllAsync();
            return Ok(notifications);
        }

        // GET: api/Notifications/5
        [HttpGet("get-by-id")]
        public async Task<ActionResult<Notification>> GetNotification(Guid id)
        {
            try
            {
                var notification = await _services.GetByIdAsync(id);
                if (notification == null)
                {
                    return NotFound();
                }
                return Ok(notification);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Notifications
        [HttpPost("create")]
        public async Task<ActionResult> PostNotification(Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _services.CreateAsync(notification);
                if (!result)
                {
                    return BadRequest("Could not create the notification.");
                }

                return CreatedAtAction(nameof(GetNotification), new { id = notification.NotificationId }, notification);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error.");
            }
        }
        // PUT: api/Notifications/{id}
        [HttpPut("update")]
        public async Task<ActionResult> PutNotification(Notification notification)
        {
            try
            {

                var notificationEdit = await _services.GetByIdAsync(notification.NotificationId);
                if (notificationEdit == null)
                {
                    return NotFound();
                }
                notificationEdit.UserId = notification.UserId;
                notificationEdit.Title = notification.Title;
                notificationEdit.Message = notification.Message;
                notificationEdit.ReadAt = notification.ReadAt;
                await _services.UpdateAsync(notificationEdit);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log the exception (ex)
                return BadRequest();
            }
        }

        // DELETE: api/Notifications/{id}
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteNotification(Guid id)
        {
            try
            {
                var notification = await _services.GetByIdAsync(id);
                if (notification == null)
                {
                    return NotFound();
                }

                var result = await _services.DeleteAsync(notification);
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
    }
}
