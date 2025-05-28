using AcunMedyaProject3.Context;
using AcunMedyaProject3.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaProject3.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ProjectContext _projectContext;

        public ReservationController(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            await _projectContext.Reservations.AddAsync(reservation);
            await _projectContext.SaveChangesAsync();
            TempData["SuccessMessage"] = "Randevunuz başarıyla oluşturuldu.";
            return RedirectToAction("Index","Default");
        }
    }
}
