using Sprint.Models;
using Sprint.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprint.Repository
{
    public interface IDoctorScheduleRepository<T>
    {
        Task<List<DoctorScheduleViewModel>> GetDoctorSchedules();
        Task<DoctorScheduleViewModel> GetDoctorScheduleById(int? scheduleid);
        Task<int> AddDoctorSchedule(DoctorScheduleViewModel model);

        Task<int> DeleteDoctorSchedule(int? scheduleid);

        Task<int> UpdateDoctorSchedule(Doctor_Schedule doctorschedule);
    }
}