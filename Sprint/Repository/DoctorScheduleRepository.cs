using Sprint.Models;
using Sprint.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Sprint.Repository
{
    public class DoctorScheduleRepository : IDoctorScheduleRepository<Doctor_Schedule>
    {
        ApplicationDbContext db;
        public DoctorScheduleRepository(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        public async Task<int> AddDoctorSchedule(DoctorScheduleViewModel model)
        {
            if (db != null)
            {
                Doctor getdoctor = await (from d in db.Doctors
                                          where d.Doctor_Id == model.Doctor_Id
                                          select d).FirstOrDefaultAsync<Doctor>();
                //doctorschedule.doctor = doctor;
                Doctor_Schedule doctor_Schedule = new Doctor_Schedule()
                {
                    Schedule_Id = model.Schedule_Id,
                    No_Of_Patients = model.No_Of_Patients,
                    Available_days = model.Available_days,
                    From_Time = model.From_Time,
                    To_Time = model.To_Time,
                    doctor = getdoctor
                };
                await db.Doctor_Schedules.AddAsync(doctor_Schedule);
                await db.SaveChangesAsync();
                return doctor_Schedule.Schedule_Id;
            }
            return 0;
        }
        public async Task<int> DeleteDoctorSchedule(int? scheduleid)
        {
            int result = 0;
            if (db != null)
            {
                var doctorschedule = await db.Doctor_Schedules.FirstOrDefaultAsync(x => x.Schedule_Id == scheduleid);

                if (doctorschedule != null)
                {
                    db.Doctor_Schedules.Remove(doctorschedule);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }
        public async Task<int> UpdateDoctorSchedule(Doctor_Schedule doctorschedule)
        {
            if (db != null)
            {
                db.Doctor_Schedules.Update(doctorschedule);
                await db.SaveChangesAsync();
                return doctorschedule.Schedule_Id;
            }
            return 0;
        }

        public async Task<List<DoctorScheduleViewModel>> GetDoctorSchedules()
        {
            if (db != null)
            {
                return await (from ds in db.Doctor_Schedules
                              join d in db.Doctors
                              on ds.doctor.Doctor_Id equals d.Doctor_Id
                              select new DoctorScheduleViewModel
                              {
                                  Schedule_Id = ds.Schedule_Id,
                                  DoctorName = d.Doctor_Name,
                                  No_Of_Patients = ds.No_Of_Patients,
                                  Available_days = ds.Available_days,
                                  From_Time = ds.From_Time,
                                  To_Time = ds.To_Time,
                                  Doctor_Id = ds.doctor.Doctor_Id
                              }).ToListAsync<DoctorScheduleViewModel>();
                //return await db.Doctor_Schedules.ToListAsync();
            }
            return null;
        }

        public async Task<DoctorScheduleViewModel> GetDoctorScheduleById(int? scheduleid)
        {
            if (db != null)
            {
                return await (from ds in db.Doctor_Schedules
                              join d in db.Doctors
                              on ds.doctor.Doctor_Id equals d.Doctor_Id
                              where ds.Schedule_Id == scheduleid
                              select new DoctorScheduleViewModel
                              {
                                  Schedule_Id = ds.Schedule_Id,
                                  DoctorName = d.Doctor_Name,
                                  No_Of_Patients = ds.No_Of_Patients,
                                  Available_days = ds.Available_days,
                                  From_Time = ds.From_Time,
                                  To_Time = ds.To_Time,
                                  Doctor_Id = ds.doctor.Doctor_Id
                              }).FirstOrDefaultAsync();
            }
            return null;
        }
    }
}