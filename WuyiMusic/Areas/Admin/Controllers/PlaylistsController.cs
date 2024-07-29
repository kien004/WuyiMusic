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
    public class PlaylistsController : Controller
    {
        private HttpClient _httpClient;
        private readonly AppDbContext _context;

        public PlaylistsController(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        // GET: Admin/Playlists
        public async Task<IActionResult> Index()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            List<Playlist> playlists = new List<Playlist>();
            string url = "https://localhost:44348/api/Playlists/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            playlists = JsonConvert.DeserializeObject<List<Playlist>>(reponse);
            return View(playlists);
        }

        // GET: Admin/Playlists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            string url = $"https://localhost:44348/api/Playlists/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var playlists = JsonConvert.DeserializeObject<Playlist>(reponse);
            return View(playlists);
        }

        // GET: Admin/Playlists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Admin/Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlaylistId,UserId,Title,CreatedAt,UpdatedAt")] Playlist playlist)
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            string url = $"https://localhost:44348/api/Playlists/create";
            var reponses = await _httpClient.PostAsJsonAsync(url, playlist);
            return RedirectToAction("Index", "Playlists");
        }

        // GET: Admin/Playlists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            string url = $"https://localhost:44348/api/Playlists/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var playlist = JsonConvert.DeserializeObject<Playlist>(reponse);
            return View(playlist);
        }

        // POST: Admin/Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PlaylistId,UserId,Title,CreatedAt,UpdatedAt")] Playlist playlist)
        {
            string url = $"https://localhost:44348/api/Playlists/update";
            var reponse = await _httpClient.PutAsJsonAsync(url, playlist);
            return RedirectToAction("Index", "Playlists");
        }

        // GET: Admin/Playlists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            string url = $"https://localhost:44348/api/Playlists/delete?id={id}";
            var reponse = await _httpClient.DeleteAsync(url);
            return RedirectToAction("Index", "Playlists");
        }
    }
}
