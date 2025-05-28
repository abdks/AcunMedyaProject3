using AcunMedyaProject3.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Threading.Tasks;

namespace AcunMedyaProject3.ViewComponents;

public class _DefaultHeadComponentPartial:ViewComponent
{
    private readonly ProjectContext _projectContext;

    public _DefaultHeadComponentPartial(ProjectContext projectContext)
    {
        _projectContext = projectContext;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var departments = await _projectContext.Departments.ToListAsync();
        var doctors = await _projectContext.Doctors.ToListAsync();
        
        ViewBag.Departments = departments;
        ViewBag.Doctors = doctors;
        
        return View();
    }
}
