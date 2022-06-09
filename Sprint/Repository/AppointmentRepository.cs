using Sprint.Models;
using Sprint.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Sprint.Repository
{
    public class AppointmentRepository : IAppointmentRepository<Appointment>
    {
        ApplicationDbContext db;
        public AppointmentRepository(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        public async Task<int> AddAppointment(AppointmentViewModel model)
        {
            if (db != null)
            {
                Doctor_Schedule getdoctorschedule = await (from d in db.Doctor_Schedules
                                                           where d.Schedule_Id == model.Schedule_Id
                                                           select d).FirstOrDefaultAsync<Doctor_Schedule>();
                Patient getpatient = await (from p in db.Patients
                                            where p.Patient_Id == model.Patient_Id
                                            select p).FirstOrDefaultAsync<Patient>();

                Appointment appointment = new Appointment()
                {
                    Appointment_No = model.Appointment_No,
                    Appointment_Date = model.Appointment_Date,
                    Appointment_Time = model.Appointment_Time,
                    Notes = model.Notes,
                    Doctor_Schedule = getdoctorschedule,
                    Patient = getpatient
                };
                await db.Appointments.AddAsync(appointment);
                await db.SaveChangesAsync();
                return appointment.Appointment_No;
            }
            return 0;
        }

        public async Task<int> DeleteAppointment(int? appointmentno)
        {
            int result = 0;
            if (db != null)
            {
                var appointment = await db.Appointments.FirstOrDefaultAsync(x => x.Appointment_No == appointmentno);

                if (appointment != null)
                {
                    db.Appointments.Remove(appointment);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<int> UpdateAppointment(Appointment appointment)
        {
            if (db != null)
            {
                db.Appointments.Update(appointment);
                await db.SaveChangesAsync();
                return appointment.Appointment_No;
            }
            return 0;
        }

        public async Task<List<AppointmentViewModel>> GetAppointments()
        {
            if (db != null)
            {
                return await (from a in db.Appointments
                              join ds in db.Doctor_Schedules
                              on a.Doctor_Schedule.Schedule_Id equals ds.Schedule_Id
                              join p in db.Patients
                              on a.Patient.Patient_Id equals p.Patient_Id
                              select new AppointmentViewModel
                              {
                                  Appointment_No = a.Appointment_No,
                                  Appointment_Date = a.Appointment_Date,
                                  Appointment_Time = a.Appointment_Time,
                                  Notes = a.Notes,
                                  Schedule_Id = a.Doctor_Schedule.Schedule_Id,
                                  Patient_Id = a.Patient.Patient_Id,

                              }).ToListAsync<AppointmentViewModel>();
                //return await db.Doctor_Schedules.ToListAsync();
            }
            return null;
        }

        public async Task<AppointmentViewModel> GetAppointmentByNo(int? appointmentno)
        {
            if (db != null)
            {
                return await (from a in db.Appointments
                              join ds in db.Doctor_Schedules
                              on a.Doctor_Schedule.Schedule_Id equals ds.Schedule_Id
                              join p in db.Patients
                              on a.Patient.Patient_Id equals p.Patient_Id
                              where a.Appointment_No == appointmentno
                              select new AppointmentViewModel
                              {
                                  Appointment_No = a.Appointment_No,
                                  Appointment_Date = a.Appointment_Date,
                                  Appointment_Time = a.Appointment_Time,
                                  Notes = a.Notes,
                                  Schedule_Id = a.Doctor_Schedule.Schedule_Id,
                                  Patient_Id = a.Patient.Patient_Id
                              }).FirstOrDefaultAsync();
            }
            return null;
        }
    }
}