using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaProject3.Areas.Admin.Controllers;
public class ServicesController : Controller
{
    [Area("Admin")]
    public IActionResult Index()
    {
        return View();
    }
}
