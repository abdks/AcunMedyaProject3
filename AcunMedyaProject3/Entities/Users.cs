using System.ComponentModel.DataAnnotations.Schema;

namespace AcunMedyaProject3.Entities;

public class Users
{
    public int UsersID { get; set; }
    public string UserName { get;set;}
    public string Password { get;set;}
    

    //Dosya indirme için

    public string ProfileImagePath { get;set;}

    [NotMapped] //veri tabanına kaydetme sadece c# ta kullan
    public IFormFile ProfileImage { get;set;}


}
