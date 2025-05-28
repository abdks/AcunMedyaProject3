using AcunMedyaProject3.Context;
using AcunMedyaProject3.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using System.Security.Claims;

namespace AcunMedyaProject3.Controllers;
public class AccountController : Controller
{
    //Dependency ınjection  c# 
    //asenkron yapı

    private readonly ProjectContext _projectContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AccountController(ProjectContext projectContext, IWebHostEnvironment webHostEnvironment)
    {
        _projectContext = projectContext;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Users users)
    {

        if(!ModelState.IsValid)
        {

        }







        //abdks 123
        //abdks 1234
        var user = _projectContext.Users.FirstOrDefault(x=>x.UserName == users.UserName && x.Password == users.Password);

        if(user == null)
        {
            ModelState.AddModelError(string.Empty,"kullanıcı adı ve şifre hatalı");
            return View(users);
        }

        //claim 

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name ,user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.UsersID.ToString())

        };

        //Cookie giriş işlemleri yapıyoruz
        var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

        //Default olarak cookie temasını kullan istersek buraya 2 saat sonra silin gibi şeyler ekleyebiliriz.
        var authProperties = new AuthenticationProperties();

        //Kullanıcıyı giriş yapmış olarak işaretleyeceğiz.

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


        //kullanıcının resimini geçici olarak kaydet (viewbag gibi)
        TempData["ProfileImage"] = user.ProfileImagePath;  

        return RedirectToAction("Profile", new {id = user.UsersID});

    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(Users users)
    {
        if(users.ProfileImage != null & users.ProfileImage.Length > 0)
        {
            var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Resimler");
            Directory.CreateDirectory(uploadFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(users.ProfileImage.FileName);

            var filePath = Path.Combine(uploadFolder, uniqueFileName);  //wwwrooot/resimler/2545a6s4asdsad4_test.jpg

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await users.ProfileImage.CopyToAsync(stream);
            }

            users.ProfileImagePath = "/Resimler/" + uniqueFileName;
        }

        _projectContext.Users.Add(users);
        await _projectContext.SaveChangesAsync();

        return RedirectToAction("Profile", new { id = users.UsersID });



        //uniq = benzersiz, { ıd gibi düşünebiliriz} 

        //test.jpg
        //test.jpg

        //2545a6s4asdsad4_test.jpg
        //214245454sadasda_test.jpg




    }

}
