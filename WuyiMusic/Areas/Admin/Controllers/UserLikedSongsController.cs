using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WuyiDAL.Models;

namespace WuyiMusic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserLikedSongsController : Controller
    {
        private HttpClient _httpClient;
        private readonly AppDbContext _context;

        public UserLikedSongsController(HttpClient httpClient,AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        // GET: Admin/UserLikedSongs
        public async Task<IActionResult> Index()
        {
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            List<UserLikedSong> userLikedSongs = new List<UserLikedSong>();
            string url = "https://localhost:44348/api/UserLikedSongs/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            userLikedSongs = JsonConvert.DeserializeObject<List<UserLikedSong>>(reponse);
            return View(userLikedSongs);
        }

        // GET: Admin/UserLikedSongs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            string url = "https://localhost:44348/api/UserLikedSongs/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            var userLikedSongs = JsonConvert.DeserializeObject<UserLikedSong>(reponse);
            return View(userLikedSongs);
        }

        // GET: Admin/UserLikedSongs/Create
        public IActionResult Create()
        {
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Admin/UserLikedSongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserLikedSongId,UserId,SongId,LikedAt")] UserLikedSong userLikedSong)
        {
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            string url = $"https://localhost:44348/api/UserLikedSongs/create";
            var reponse = await _httpClient.PostAsJsonAsync(url, userLikedSong);
            return RedirectToAction("Index", "UserLikedSongs");
        }

        // GET: Admin/UserLikedSongs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            string url = $"https://localhost:44348/api/UserLikedSongs/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var userLikedSong = JsonConvert.DeserializeObject<UserLikedSong>(reponse);
            return View(userLikedSong);
        }

        // POST: Admin/UserLikedSongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserLikedSongId,UserId,SongId,LikedAt")] UserLikedSong userLikedSong)
        {
            string url = $"https://localhost:44348/api/UserFollowArtists/update";
            var reponse = await _httpClient.PutAsJsonAsync(url, userLikedSong);
            return RedirectToAction("Index", "UserLikedSongs");
        }

        // GET: Admin/UserLikedSongs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            string url = $"https://localhost:44348/api/UserLikedSongs/delete?id={id}";
            var reponse = await _httpClient.DeleteAsync(url);
            return RedirectToAction("Index", "UserLikedSongs");
        }
    }
}
