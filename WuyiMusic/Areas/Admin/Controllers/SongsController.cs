using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WuyiDAL.Models;

namespace WuyiMusic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SongsController : Controller
    {
        private readonly AppDbContext _context;
        private HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public SongsController(AppDbContext context, HttpClient hp, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = hp;
            _httpClientFactory = httpClientFactory;
        }

        // GET: Admin/Songs
        public async Task<IActionResult> Index()
        {
            List<Song> songs = new List<Song>();
            string url = "https://localhost:44348/api/Songs/get-all";
            var response = await _httpClient.GetStringAsync(url);
            songs = JsonConvert.DeserializeObject<List<Song>>(response);
            return View(songs);

        }

        // GET: Admin/Songs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            string url = $"https://localhost:44348/api/Songs/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var song = JsonConvert.DeserializeObject<Song>(reponse);
            return View(song);
        }

        // GET: Admin/Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "CoverImage");
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name");
            return View();
        }

        // POST: Admin/Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongId,ArtistId,AlbumId,GenreId,Title,Duration,CreatedAt,UpdatedAt")] Song song, IFormFile songFile)
        {
            { 
                try
                {
                    if (songFile == null || songFile.Length == 0)
                    {
                        ModelState.AddModelError(string.Empty, "Please upload a file.");
                        return View(song);
                    }

                    var fileName = Path.GetFileName(songFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "SongPath", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        songFile.CopyTo(stream);
                    }

                    // Lưu đường dẫn tệp vào thuộc tính FilePath của đối tượng song
                    song.FilePath = fileName;


                    _context.Songs.Add(song);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Internal server error: {ex.Message}");
                }
            }
            

                // Load danh sách Album, Artist, Genre để binding dropdown
                ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "CoverImage", song.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name", song.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name", song.GenreId);
            return View(song);
        }


        // GET: Admin/Songs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "CoverImage", song.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name", song.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name", song.GenreId);
            return View(song);
        }

        // POST: Admin/Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SongId,ArtistId,AlbumId,GenreId,Title,Duration,FilePath,CreatedAt,UpdatedAt")] Song song)
        {
            if (id != song.SongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.SongId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "CoverImage", song.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name", song.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name", song.GenreId);
            return View(song);
        }

        // GET: Admin/Songs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.artist)
                .Include(s => s.genre)
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Admin/Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(Guid id)
        {
            return _context.Songs.Any(e => e.SongId == id);
        }
    }
}
