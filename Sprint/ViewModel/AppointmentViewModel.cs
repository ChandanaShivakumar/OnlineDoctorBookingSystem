using System;

namespace Sprint.ViewModel
{
    public class AppointmentViewModel
    {
        public int Appointment_No { get; set; }
        public DateTime Appointment_Date { get; set; }
        public DateTime Appointment_Time { get; set; }
        public string Notes { get; set; }
        public int Schedule_Id { get; set; }
        public int Patient_Id { get; set; }
    }
}
