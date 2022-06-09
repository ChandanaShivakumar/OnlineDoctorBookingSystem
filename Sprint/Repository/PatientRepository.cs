using Microsoft.EntityFrameworkCore;
using Sprint.Models;
using Sprint.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Sprint.Repository
{
    public class PatientRepository : IPatientRepository<Patient>
    {
        ApplicationDbContext db;
        public PatientRepository(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        //adding patient by taking all values and age is derived attribute
        public async Task<int> AddPatient(Patient patient)
        {
            if (db != null)
            {
                int byear = patient.Patient_DOB.Year;
                Patient patientObj = new Patient()
                {
                    Patient_Id = patient.Patient_Id,
                    Patient_Name = patient.Patient_Name,
                    Patient_Address = patient.Patient_Address,
                    Patient_City = patient.Patient_City,
                    Patient_State = patient.Patient_State,
                    Patient_Postal_Code = patient.Patient_Postal_Code,
                    Patient_Phone_No = patient.Patient_Phone_No,
                    Patient_Gender = patient.Patient_Gender,
                    Patient_DOB = patient.Patient_DOB,
                    Patient_Age = DateTime.Now.Year - byear
                };
                await db.Patients.AddAsync(patientObj);
                await db.SaveChangesAsync();
                return patientObj.Patient_Id;
            }
            return 0;
        }
        //delete patient by taking patient id as input
        public async Task<int> DeletePatient(int? patientid)
        {
            int result = 0;
            if (db != null)
            {
                var patient = await db.Patients.FirstOrDefaultAsync(x => x.Patient_Id == patientid);

                if (patient != null)
                {
                    db.Patients.Remove(patient);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        //updating patient by passing necessary values to update
        public async Task<int> UpdatePatient(Patient patient)
        {
            if (db != null)
            {

                int byear = patient.Patient_DOB.Year;
                Patient patientObj = new Patient()
                {
                    Patient_Id = patient.Patient_Id,
                    Patient_Name = patient.Patient_Name,
                    Patient_Address = patient.Patient_Address,
                    Patient_City = patient.Patient_City,
                    Patient_State = patient.Patient_State,
                    Patient_Postal_Code = patient.Patient_Postal_Code,
                    Patient_Phone_No = patient.Patient_Phone_No,
                    Patient_Gender = patient.Patient_Gender,
                    Patient_DOB = patient.Patient_DOB,
                    Patient_Age = DateTime.Now.Year - byear
                };
                db.Patients.Update(patientObj);
                await db.SaveChangesAsync();
                return patientObj.Patient_Id;
            }
            return 0;
        }

        //display list of all patients
        public async Task<List<Patient>> GetPatients()
        {
            if (db != null)
            {
                return await db.Patients.ToListAsync();
            }
            return null;
        }

        //getting patient details by passing id
        public async Task<PatientViewModel> GetPatientById(int? patientid)
        {
            if (db != null)
            {
                return await (from p in db.Patients
                              where p.Patient_Id == patientid
                              select new PatientViewModel
                              {
                                  Patient_Id = p.Patient_Id,
                                  Patient_Name = p.Patient_Name,
                                  Patient_Address = p.Patient_Address,
                                  Patient_City = p.Patient_City,
                                  Patient_State = p.Patient_State,
                                  Patient_Postal_Code = p.Patient_Postal_Code,
                                  Patient_Phone_No = p.Patient_Phone_No,
                                  Patient_Gender = p.Patient_Gender,
                                  Patient_DOB = p.Patient_DOB,
                                  Patient_Age = p.Patient_Age
                              }).FirstOrDefaultAsync();
            }

            return null;
        }
    }
}