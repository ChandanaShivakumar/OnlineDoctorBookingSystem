using Microsoft.EntityFrameworkCore;
using Sprint.Models;
using Sprint.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Sprint.Repository
{
    public class DoctorRepository : IDoctorRepository<Doctor>
    {
        ApplicationDbContext db;
        public DoctorRepository(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        public async Task<int> AddDoctor(Doctor doctor)
        {
            if (db != null)
            {
                await db.Doctors.AddAsync(doctor);
                await db.SaveChangesAsync();
                return doctor.Doctor_Id;
            }
            return 0;
        }
        public async Task<int> DeleteDoctor(int? doctorid)
        {
            int result = 0;
            if (db != null)
            {
                var doctor = await db.Doctors.FirstOrDefaultAsync(x => x.Doctor_Id == doctorid);

                if (doctor != null)
                {
                    db.Doctors.Remove(doctor);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }
        public async Task<int> UpdateDoctor(Doctor doctor)
        {
            if (db != null)
            {
                db.Doctors.Update(doctor);
                await db.SaveChangesAsync();
                return doctor.Doctor_Id;
            }
            return 0;
        }
        public async Task<List<Doctor>> GetDoctors()
        {
            if (db != null)
            {
                return await db.Doctors.ToListAsync();
            }
            return null;
        }
        public async Task<DoctorViewModel> GetDoctorById(int? doctorid)
        {
            if (db != null)
            {
                return await (from d in db.Doctors
                              where d.Doctor_Id == doctorid
                              select new DoctorViewModel
                              {
                                  Doctor_Id = d.Doctor_Id,
                                  Doctor_Name = d.Doctor_Name,
                                  Doctor_Address = d.Doctor_Address,
                                  Doctor_City = d.Doctor_City,
                                  Doctor_State = d.Doctor_State,
                                  Doctor_Postal_Code = d.Doctor_Postal_Code,
                                  Doctor_Phone_No = d.Doctor_Phone_No,
                                  Doctor_Gender = d.Doctor_Gender,
                                  Specialization = d.Specialization,
                                  Doctor_Description = d.Doctor_Description,
                                  Doctor_Fees = (double)d.Doctor_Fees,
                              }).FirstOrDefaultAsync();
            }

            return null;
        }
    }
}