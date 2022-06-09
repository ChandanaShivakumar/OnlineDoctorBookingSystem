using System;

namespace Sprint.ViewModel
{
    public class PatientViewModel
    {
        public int Patient_Id { get; set; }
        public string Patient_Name { get; set; }
        public string Patient_Address { get; set; }
        public string Patient_City { get; set; }
        public string Patient_State { get; set; }
        public string Patient_Postal_Code { get; set; }
        public string Patient_Phone_No { get; set; }
        public string Patient_Gender { get; set; }
        public DateTime Patient_DOB { get; set; }
        public int Patient_Age { get; set; }
    }
}
