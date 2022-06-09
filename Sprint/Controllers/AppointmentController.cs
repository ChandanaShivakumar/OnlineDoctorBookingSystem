using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sprint.Models;
using Sprint.Repository;
using Sprint.ViewModel;
using System;
using System.Threading.Tasks;

namespace Sprint.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository<Appointment> appointmentRepository;

        public AppointmentController(IAppointmentRepository<Appointment> appointment)
        {
            appointmentRepository = appointment;
        }
        [HttpPost]
        [Route("AddAppointment")]
        public async Task<IActionResult> AddAppointment(AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var appointmentno = await appointmentRepository.AddAppointment(model);
                    if (appointmentno > 0)
                    {
                        return Ok("Appointment Booked Successfully with ID : " + appointmentno);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("DeleteAppointment/{appointmentno}")]
        public async Task<IActionResult> DeleteAppointment(int? appointmentno)
        {
            int result = 0;

            if (appointmentno == null)
            {
                return BadRequest();
            }

            try
            {
                result = await appointmentRepository.DeleteAppointment(appointmentno);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok("Appointment Cancelled Successfully.");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpPut]
        [Route("UpdateAppointment")]
        public async Task<IActionResult> UpdateAppointment(Appointment model)
        {
            int appointmentno = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    appointmentno = await appointmentRepository.UpdateAppointment(model);
                    return Ok("Appointment Successfully Updated with ID : " + appointmentno);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetAppointments")]
        public async Task<IActionResult> GetAppointments()
        {
            try
            {
                var appointments = await appointmentRepository.GetAppointments();
                if (appointments == null)
                {
                    return NotFound();
                }

                return Ok(appointments);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("GetAppointmentByNo/{appointmentno}")]
        public async Task<IActionResult> GetAppointmentById(int? appointmentno)
        {
            if (appointmentno == null)
            {
                return BadRequest();
            }

            try
            {
                var appointment = await appointmentRepository.GetAppointmentByNo(appointmentno);

                if (appointment == null)
                {
                    return NotFound();
                }

                return Ok(appointment);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}