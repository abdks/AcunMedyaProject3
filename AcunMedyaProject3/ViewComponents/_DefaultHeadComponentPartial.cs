using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaProject3.ViewComponents;

public class _DefaultHeadComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
