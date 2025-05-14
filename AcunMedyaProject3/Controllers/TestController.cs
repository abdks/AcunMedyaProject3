using AcunMedyaProject3.Context;
using AcunMedyaProject3.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AcunMedyaProject3.Controllers;
public class TestController : Controller
{
    private readonly ProjectContext _projectContext;
    private readonly IWebHostEnvironment _environment;

    public TestController(ProjectContext projectContext, IWebHostEnvironment environment)
    {
        _projectContext = projectContext;
        _environment = environment;
    }

    public IActionResult Index()
    {
        return View();
    }

    // GET: /User/Login - Giriş sayfasını gösterir
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /User/Login - Giriş işlemini gerçekleştirir
    [HttpPost]
    public async Task<IActionResult> Login(Test model)
    {
        // Kullanıcı adı ve şifre kontrolü
        var user = _projectContext.Tests.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

        if (user == null)
        {
            // Hatalı giriş durumunda hata mesajı göster
            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı!");
            return View(model);
        }

        // Kullanıcı bilgilerini claim'lere dönüştür
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString())
    };

        // Cookie authentication için gerekli ayarları yap
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();

        // Kullanıcıyı giriş yapmış olarak işaretle
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        // Profil resmini TempData'ya kaydet
        TempData["ProfileImage"] = user.ProfileImagePath;

        // Profil sayfasına yönlendir
        return RedirectToAction("Profile", new { id = user.UserID });
    }

    // GET: /User/Register - Kayıt sayfasını gösterir
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: /User/Register - Kayıt işlemini gerçekleştirir
    [HttpPost]
    public async Task<IActionResult> Register(Test user)
    {
        // Eğer profil resmi yüklendiyse
        if (user.ProfileImage != null && user.ProfileImage.Length > 0)
        {
            // Uploads klasörünün yolunu al
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder); // Klasör yoksa oluştur

            // Benzersiz dosya adı oluştur
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(user.ProfileImage.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Dosyayı kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await user.ProfileImage.CopyToAsync(stream);
            }

            // Veritabanında saklanacak yolu belirle
            user.ProfileImagePath = "/uploads/" + uniqueFileName;
        }

        // Kullanıcıyı veritabanına ekle
        _projectContext.Tests.Add(user);
        await _projectContext.SaveChangesAsync();

        // Profil sayfasına yönlendir
        return RedirectToAction("Profile", new { id = user.UserID });
    }

    // GET: /User/Profile/{id} - Profil sayfasını gösterir
    [HttpGet("profile/{id}")]
    public IActionResult Profile(int id)
    {
        // Kullanıcıyı ID'ye göre bul
        var user = _projectContext.Tests.FirstOrDefault(u => u.UserID == id);
        if (user == null)
            return NotFound(); // Kullanıcı bulunamazsa 404 döndür

        return View(user);
    }

}
