using System;

namespace Sprint.ViewModel
{
    public class DoctorScheduleViewModel
    {
        public int Schedule_Id { get; set; }
        public string DoctorName { get; set; }
        public int No_Of_Patients { get; set; }
        public string Available_days { get; set; }
        public DateTime From_Time { get; set; }
        public DateTime To_Time { get; set; }
        public int Doctor_Id { get; set; }
    }
}
