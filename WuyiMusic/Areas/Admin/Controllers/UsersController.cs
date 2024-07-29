using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WuyiDAL.Models;
    
namespace WuyiMusic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private HttpClient _httpClient;
        public UsersController( HttpClient hp)
        {      
            _httpClient =hp;
        }

        // GET: Admin/Users
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<User> user=new List<User>();
            string url = "https://localhost:7178/api/Users/get-all";
            var reponse= await _httpClient.GetStringAsync(url);
            user= JsonConvert.DeserializeObject<List<User>>(reponse);
            return View( user);
        }

        // GET: Admin/Users/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            string url = $"https://localhost:7178/api/Users/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var user = JsonConvert.DeserializeObject<User>(reponse);
            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Email,Password,IsArtist,CreatedAt,UpdatedAt")] User user)
{
            string checkUsernameUrl = $"https://localhost:7178/api/Users/CheckUsername?username={user.Username}";

            var checkUsernameResponse = await _httpClient.GetAsync(checkUsernameUrl);

            if (checkUsernameResponse.StatusCode == HttpStatusCode.Conflict)
            {
                // Username đã tồn tại
                ModelState.AddModelError("Username", "Username already exists.");
                return View(user);
            }

            if (!checkUsernameResponse.IsSuccessStatusCode)
            {
                // Xử lý lỗi phản hồi từ việc kiểm tra username
                var error = await checkUsernameResponse.Content.ReadAsStringAsync();
                return StatusCode((int)checkUsernameResponse.StatusCode, $"Error checking username: {error}");
            }

            string url = $"https://localhost:7178/api/Users/create";
            var reponse = await _httpClient.PostAsJsonAsync(url, user);
            return RedirectToAction("Index", "Users");

}


        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            string url = $"https://localhost:7178/api/Users/get-by-id?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var user = JsonConvert.DeserializeObject<User>(reponse);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,Username,Email,Password,IsArtist,CreatedAt,UpdatedAt")] User user)
        {
            string url = $"https://localhost:7178/api/Users/update";
            var reponse = await _httpClient.PutAsJsonAsync(url,user);
            return RedirectToAction("Index", "Users");
        }

        // GET: Admin/Users/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            string url = $"https://localhost:7178/api/Users/delete?id={id}";
            var reponse = await _httpClient.GetStringAsync(url);
            return RedirectToAction("Index", "Users");
        }

     

        
    }
}
