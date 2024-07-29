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
    public class ArtistsController : Controller
    {
        private HttpClient _httpClient;
        public ArtistsController(HttpClient hp)
        {
            _httpClient = hp;
        }
   
        // GET: Admin/Artists
        public async Task<IActionResult> Index()
        {
            List<Artist> artist = new List<Artist>();
            string url = "https://localhost:44348/api/Artists/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            artist = JsonConvert.DeserializeObject<List<Artist>>(reponse);
            return View(artist);
        }

            // GET: Admin/Artists/Details/5
            public async Task<IActionResult> Details(Guid id)
            {
                string url = $"https://localhost:44348/api/Artists/get-by-id?id={id}";
                var reponse = await _httpClient.GetStringAsync(url);
               var artist = JsonConvert.DeserializeObject<Artist>(reponse);
                return View(artist);
            }

            // GET: Admin/Artists/Create
            public IActionResult Create()
            {                
                return View();
            }

            // POST: Admin/Artists/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("ArtistId,Name,Bio,Avatar,CreatedAt,UpdatedAt")] Artist artist)
            {
            string url = $"https://localhost:44348/api/Artists/create";
            var reponse = await _httpClient.PostAsJsonAsync(url, artist);
            return RedirectToAction("Index", "Artists");
            }

        // GET: Admin/Artists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            string url = $"https://localhost:44348/api/Artists/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var artist = JsonConvert.DeserializeObject<Notification>(reponse);
            return View(artist);
        }

        // POST: Admin/Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ArtistId,Name,Bio,Avatar,CreatedAt,UpdatedAt")] Artist artist)
        {
            string url = $"https://localhost:44348/api/Artists/update";
            var reponse = await _httpClient.PutAsJsonAsync(url, artist);
            return RedirectToAction("Index", "Artists");
        }

        // GET: Admin/Artists/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            string url = $"https://localhost:44348/api/Artists/delete?id={id}";
            var reponse = await _httpClient.DeleteAsync(url);
            return RedirectToAction("Index", "Artists");
        }
    }
}
