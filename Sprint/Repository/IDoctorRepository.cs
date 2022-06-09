using Sprint.Models;
using Sprint.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprint.Repository
{
    public interface IDoctorRepository<T>
    {
        Task<List<Doctor>> GetDoctors();
        Task<DoctorViewModel> GetDoctorById(int? doctorid);
        Task<int> AddDoctor(Doctor doctor);

        Task<int> DeleteDoctor(int? doctorid);

        Task<int> UpdateDoctor(Doctor doctor);
    }
}
