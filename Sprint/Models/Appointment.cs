using System;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class Appointment
    {
        [Key]
        public int Appointment_No { get; set; }
        [Required]
        public DateTime Appointment_Date { get; set; }
        [Required]
        public DateTime Appointment_Time { get; set; }
        [Required, MaxLength(50)]
        public string Notes { get; set; }

        //navigation property
        public Doctor_Schedule Doctor_Schedule { get; set; }
        public Patient Patient { get; set; }
    }
}
