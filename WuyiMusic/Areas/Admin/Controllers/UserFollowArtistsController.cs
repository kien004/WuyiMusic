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
    public class UserFollowArtistsController : Controller
    {
        private HttpClient _httpClient;
        private readonly AppDbContext _context;

        public UserFollowArtistsController(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        // GET: Admin/UserFollowArtists
        public async Task<IActionResult> Index()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            List<UserFollowArtist> userFollowArtists = new List<UserFollowArtist>();         
            string url = "https://localhost:44348/api/UserFollowArtists/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            userFollowArtists = JsonConvert.DeserializeObject<List<UserFollowArtist>>(reponse);
            return View(userFollowArtists);
        }

        // GET: Admin/UserFollowArtists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            string url = "https://localhost:44348/api/UserFollowArtists/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            var userFollowArtists = JsonConvert.DeserializeObject<UserFollowArtist>(reponse);
            return View(userFollowArtists);
        }

        // GET: Admin/UserFollowArtists/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Admin/UserFollowArtists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ArtistId,FollowedAt")] UserFollowArtist userFollowArtist)
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            string url = $"https://localhost:44348/api/UserFollowArtists/create";
            var reponse = await _httpClient.PostAsJsonAsync(url, userFollowArtist);
            return RedirectToAction("Index", "UserFollowArtists");
        }

        // GET: Admin/UserFollowArtists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            string url = $"https://localhost:44348/api/UserFollowArtists/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var userFollowArtist = JsonConvert.DeserializeObject<UserFollowArtist>(reponse);
            return View(userFollowArtist);
        }

        // POST: Admin/UserFollowArtists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,ArtistId,FollowedAt")] UserFollowArtist userFollowArtist)
        {
            string url = $"https://localhost:44348/api/UserFollowArtists/update";
            var reponse = await _httpClient.PutAsJsonAsync(url, userFollowArtist);
            return RedirectToAction("Index", "UserFollowArtists");
        }

        // GET: Admin/UserFollowArtists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            string url = $"https://localhost:44348/api/UserFollowArtists/delete?id={id}";
            var reponse = await _httpClient.DeleteAsync(url);
            return RedirectToAction("Index", "UserFollowArtists");
        }       
    }
}
