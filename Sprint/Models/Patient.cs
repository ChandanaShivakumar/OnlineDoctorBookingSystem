using System;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class Patient
    {
        [Key]
        public int Patient_Id { get; set; }
        [Required, MaxLength(20)]
        public string Patient_Name { get; set; }
        [Required, MaxLength(50)]
        public string Patient_Address { get; set; }
        [Required, MaxLength(20)]
        public string Patient_City { get; set; }
        [Required, MaxLength(20)]
        public string Patient_State { get; set; }
        [Required, MaxLength(6)]
        public string Patient_Postal_Code { get; set; }
        [Required, MaxLength(10)]
        public string Patient_Phone_No { get; set; }
        [Required, MaxLength(1)]
        public string Patient_Gender { get; set; }
        [Required]
        public DateTime Patient_DOB { get; set; }
        [Required]
        public int Patient_Age { get; set; }
    }
}
