using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class Doctor
    {
        [Key]
        public int Doctor_Id { get; set; }
        [Required, MaxLength(20)]
        public string Doctor_Name { get; set; }
        [Required, MaxLength(50)]
        public string Doctor_Address { get; set; }
        [Required, MaxLength(20)]
        public string Doctor_City { get; set; }
        [Required, MaxLength(20)]
        public string Doctor_State { get; set; }
        [Required, MaxLength(6)]
        public string Doctor_Postal_Code { get; set; }
        [Required, MaxLength(10)]
        public string Doctor_Phone_No { get; set; }
        [Required, MaxLength(1)]
        public string Doctor_Gender { get; set; }
        [Required, MaxLength(50)]
        public string Specialization { get; set; }
        [Required, MaxLength(200)]
        public string Doctor_Description { get; set; }
        [Required]
        public double Doctor_Fees { get; set; }
    }
}
