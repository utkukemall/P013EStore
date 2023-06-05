using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;

namespace P013EStore.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppUsersController : Controller
    {
        private readonly HttpClient _httpClient; // _httpClient nesnesini kullanarak api lere istek gönderebiliriz.
        private readonly string _apiAdres = "https://localhost:7032/api/AppUsers"; // API adresini web api projesini çalıştırdığımızda adres çubuğundan veya herhangi bir controller a istek atarak Request URL kısmından veya web api projesinde Properties altındaki launchSettings.json kısmından linki bulabiliriz.
        public AppUsersController(HttpClient httpClient)
        {
            _httpClient = httpClient; // _httpClient nesnesinin apiye ulaşması için api projesinin de bu projeyle birlikte çalışıyor olması lazım!!!
            // Aynı anda 2 projeyi çalıştırabilmek için Solution a sağ tıklayıp açılan menüden configure startup projects diyerek açılan ekrandan multiple alanına tıklayıp aynı anda başlatmak istediğimiz projeleri start olarak seçiyoruz!
        }

        // GET: AppUsersController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<AppUser>>(_apiAdres); // _httpClient nesnesi içindeki GetFromJsonAsync metodu kendisine verdiğimiz _apiAdres deki url e get isteği gönderir ve oradan gelen json formatındaki app user listesini List<AppUser> nesnesine dönüştürür.
            return View(model);
        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AppUser collection)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode) // api den başarılı bir istek kodu geldiyse (200 ok)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("","Hata Oluştu!");
            }
            return View(collection);
        }

        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, AppUser collection)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode) // api den başarılı bir istek kodu geldiyse (200 ok)
                {
                    return RedirectToAction(nameof(Index));
                }             
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
                return View();
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, AppUser collection)
        {
            try
            {
                var model = await _httpClient.DeleteAsync(_apiAdres + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
