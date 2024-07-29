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
    public class AlbumsController : Controller
    {
        private HttpClient _httpClient;
        private readonly AppDbContext _context;


        public AlbumsController(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;

        }

        // GET: Admin/Albums
        public async Task<IActionResult> Index()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            List<Album> album = new List<Album>();
            string url = "https://localhost:44348/api/Albums/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            album = JsonConvert.DeserializeObject<List<Album>>(reponse);
            return View(album);
        }

        // GET: Admin/Albums/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            string url = $"https://localhost:44348/api/Albums/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var album = JsonConvert.DeserializeObject<Album>(reponse);
            return View(album);
        }

        // GET: Admin/Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            return View();
        }

        // POST: Admin/Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumId,ArtistId,Title,ReleaseDate,CoverImage,CreatedAt,UpdatedAt")] Album album)
        {

            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            string url = $"https://localhost:44348/api/Albums/create";
            var reponses = await _httpClient.PostAsJsonAsync(url, album);
            return RedirectToAction("Index", "Albums");
        }

        // GET: Admin/Albums/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            string url = $"https://localhost:44348/api/Albums/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var album = JsonConvert.DeserializeObject<Album>(reponse);
            return View(album);
        }

        // POST: Admin/Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AlbumId,ArtistId,Title,ReleaseDate,CoverImage,CreatedAt,UpdatedAt")] Album album)
        {

            string url = $"https://localhost:44348/api/Albums/update";
            var reponse = await _httpClient.PutAsJsonAsync(url, album);
            return RedirectToAction("Index", "Albums");
        }

        // GET: Admin/Albums/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            string url = $"https://localhost:44348/api/Albums/delete?id={id}";
            var reponse = await _httpClient.DeleteAsync(url);
            return RedirectToAction("Index", "Albums");
        }
    }
}
