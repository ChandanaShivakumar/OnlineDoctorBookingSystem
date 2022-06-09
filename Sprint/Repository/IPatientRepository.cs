using Sprint.Models;
using Sprint.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprint.Repository
{
    public interface IPatientRepository<T>
    {
        //declaring all methods required for CRUD, this interface is inherited by repository
        Task<List<Patient>> GetPatients();
        Task<PatientViewModel> GetPatientById(int? patientid);

        Task<int> AddPatient(Patient patient);

        Task<int> DeletePatient(int? patientid);

        Task<int> UpdatePatient(Patient patient);
    }
}
