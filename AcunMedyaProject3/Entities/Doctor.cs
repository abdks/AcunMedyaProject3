using System.ComponentModel.DataAnnotations.Schema;

namespace AcunMedyaProject3.Entities;

public class Doctor
{
    public int DoctorId { get; set; }
    public string Name { get;set;}
    [ForeignKey("Department")]
    public int DepartmentId { get;set;}
    public ICollection<Reservation> Reservations { get; set; }


}
