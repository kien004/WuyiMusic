using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WuyiDAL.Models;

namespace WuyiMusic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationsController : Controller
    {
        private HttpClient _httpClient;
        private readonly AppDbContext _context;

        public NotificationsController(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        // GET: Admin/Notifications
        public async Task<IActionResult> Index()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            List<Notification> notification = new List<Notification>();
            string url = "https://localhost:44348/api/Notifications/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            notification = JsonConvert.DeserializeObject<List<Notification>>(reponse);
            return View(notification);
        }

        // GET: Admin/Notifications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            string url = $"https://localhost:44348/api/Notifications/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var notification = JsonConvert.DeserializeObject<Notification>(reponse);
            return View(notification);
        }

        // GET: Admin/Notifications/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            return View();
        }

        // POST: Admin/Notifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotificationId,UserId,Title,Message,CreatedAt,ReadAt")] Notification notification)
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            string url = $"https://localhost:44348/api/Notifications/create";
            var reponses = await _httpClient.PostAsJsonAsync(url, notification);
            return RedirectToAction("Index", "Notifications");
        }

        // GET: Admin/Notifications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            string url = $"https://localhost:44348/api/Notifications/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var notification = JsonConvert.DeserializeObject<Notification>(reponse);
            return View(notification);
        }

        // POST: Admin/Notifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("NotificationId,UserId,Title,Message,CreatedAt,ReadAt")] Notification notification)
        {

            string url = $"https://localhost:44348/api/Notifications/update";
            var reponse = await _httpClient.PutAsJsonAsync(url, notification);
            return RedirectToAction("Index", "Notifications");
        }

        // GET: Admin/Notifications/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            string url = $"https://localhost:44348/api/Notifications/delete?id={id}";
            var reponse = await _httpClient.DeleteAsync(url);
            return RedirectToAction("Index", "Notifications");
        }   
    }
}
