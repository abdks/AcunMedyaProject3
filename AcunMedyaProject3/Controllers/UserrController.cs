using AcunMedyaProject3.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaProject3.Controllers;
public class UserrController : Controller
{
    private readonly IValidator<Userr> _validator;

    public UserrController(IValidator<Userr> validator)
    {
        _validator = validator;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Userr user)
    {
        ValidationResult result = _validator.Validate(user);

        if(result.IsValid)
        {
            ViewBag.Message("kullanıcı kaydedildi");
        }
        else
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.PropertyName,item.ErrorMessage);
            }
        }




        return View();
    }



}
