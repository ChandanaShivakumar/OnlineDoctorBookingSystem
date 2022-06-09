using Sprint.Models;
using Sprint.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprint.Repository
{
    public interface IAppointmentRepository<T>
    {
        Task<List<AppointmentViewModel>> GetAppointments();
        Task<AppointmentViewModel> GetAppointmentByNo(int? appointmentno);
        Task<int> AddAppointment(AppointmentViewModel appointment);

        Task<int> DeleteAppointment(int? appointmentno);

        Task<int> UpdateAppointment(Appointment appointment);
    }
}
