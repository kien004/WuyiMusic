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
    public class PlaylistSongsController : Controller
    {
        private HttpClient _httpClient;
        private readonly AppDbContext _context;

        public PlaylistSongsController(AppDbContext context, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _context = context;           
        }

        // GET: Admin/PlaylistSongs
        public async Task<IActionResult> Index()
        {
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "PlaylistId", "Title");
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Title");
            List<PlaylistSong> playlistSongs = new List<PlaylistSong>();
            string url = "https://localhost:44348/api/PlaylistSongs/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            playlistSongs = JsonConvert.DeserializeObject<List<PlaylistSong>>(reponse);
            return View(playlistSongs);
        }

        // GET: Admin/PlaylistSongs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            string url = "https://localhost:44348/api/PlaylistSongs/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            var playlistSong = JsonConvert.DeserializeObject<PlaylistSong>(reponse);
            return View(playlistSong);
        }

        // GET: Admin/PlaylistSongs/Create
        public IActionResult Create()
        {
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "PlaylistId", "Title");
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Title");
            return View();
        }

        // POST: Admin/PlaylistSongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlaylistSongId,PlaylistId,SongId,AddedAt")] PlaylistSong playlistSong)
        {
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "PlaylistId", "Title");
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Title");
            string url = $"https://localhost:44348/api/PlaylistSongs/create";
            var reponse = await _httpClient.PostAsJsonAsync(url, playlistSong);
            return RedirectToAction("Index", "PlaylistSongs");
        }

        // GET: Admin/PlaylistSongs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "PlaylistId", "Title");
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Title");
            string url = $"https://localhost:44348/api/PlaylistSongs/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var playlistSong = JsonConvert.DeserializeObject<UserLikedSong>(reponse);
            return View(playlistSong);
        }

        // POST: Admin/PlaylistSongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PlaylistSongId,PlaylistId,SongId,AddedAt")] PlaylistSong playlistSong)
        {
            string url = $"https://localhost:44348/api/PlaylistSongs/update";
            var reponse = await _httpClient.PutAsJsonAsync(url, playlistSong);
            return RedirectToAction("Index", "PlaylistSongs");
        }

        // GET: Admin/PlaylistSongs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            string url = $"https://localhost:44348/api/PlaylistSongs/delete?id={id}";
            var reponse = await _httpClient.DeleteAsync(url);
            return RedirectToAction("Index", "PlaylistSongs");
        }
    }
}
