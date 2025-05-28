using AcunMedyaProject3.Entities;
using FluentValidation;

namespace AcunMedyaProject3.Validation;

public class UserValidator : AbstractValidator<Userr>
{
    public UserValidator()
    {
        //RuleFor(x=>x.Name).NotEmpty().WithMessage("İsim boş geçilemez").MinimumLength(2).WithMessage("İsim En az 2 karakter olmalı").MaximumLength(50).WithMessage("İsim en fazla 50 karakter olmalı").tc;

        RuleFor(x=>x.Email).NotEmpty().WithMessage("Mail adresi boş geçilemez").EmailAddress().WithMessage("Mail adresini doğru girin");



        RuleFor(x=>x.Age).NotEmpty().WithMessage("yaş boş olamaz").InclusiveBetween(18,60).WithMessage("Yaş 18 ile 60 arası olmalı");

           
    }
}
