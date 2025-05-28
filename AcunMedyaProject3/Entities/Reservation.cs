using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcunMedyaProject3.Entities;

public class Reservation
{
    public int ReservationId { get;set;}
    public string FullName { get;set; }
    public string Email { get;set;}
    public string Phone { get;set;}
    public DateTime AppointmentDate { get;set;}
    public string? Message { get;set;}
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    [ForeignKey("Doctor")]
    public int DoctorId { get;set;}
    public Department Department { get;set;}
    public Doctor Doctor { get;set;}

}

