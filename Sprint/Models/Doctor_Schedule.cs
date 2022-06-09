using System;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class Doctor_Schedule
    {
        [Key]
        public int Schedule_Id { get; set; }

        [Required]
        public int No_Of_Patients { get; set; }
        [Required, MaxLength(9)]
        public string Available_days { get; set; }
        [Required]
        public DateTime From_Time { get; set; }
        [Required]
        public DateTime To_Time { get; set; }

        //navigation property
        public Doctor doctor { get; set; }
    }
}
