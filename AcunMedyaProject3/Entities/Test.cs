using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcunMedyaProject3.Entities;

public class Test
{
    
        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string? ProfileImagePath { get; set; }

        [NotMapped]
        public IFormFile? ProfileImage { get; set; }

}


