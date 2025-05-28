namespace AcunMedyaProject3.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set;}
    public ICollection<Doctor> Doctors { get;set;}
    public ICollection<Reservation> Reservations { get;set;}

}
