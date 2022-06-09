using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleRepository<Doctor_Schedule> doctorscheduleRepository;

        public DoctorScheduleController(IDoctorScheduleRepository<Doctor_Schedule> doctorschedule)
        {
            doctorscheduleRepository = doctorschedule;
        }
        [HttpPost]
        [Route("AddDoctorSchedule")]
        public async Task<IActionResult> AddDoctorSchedule(DoctorScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var scheduleid = await doctorscheduleRepository.AddDoctorSchedule(model);
                    if (scheduleid > 0)
                    {
                        return Ok("Doctor Schedule Added Successfully with ID : " + scheduleid);
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
        [Route("DeleteDoctorSchedule/{scheduleid}")]
        public async Task<IActionResult> DeleteDoctorSchedule(int? scheduleid)
        {
            int result = 0;

            if (scheduleid == null)
            {
                return BadRequest();
            }

            try
            {
                result = await doctorscheduleRepository.DeleteDoctorSchedule(scheduleid);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok("Doctor Schedule Deleted Successfully.");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpPut]
        [Route("UpdateDoctorSchedule")]
        public async Task<IActionResult> UpdateDoctorSchedule(Doctor_Schedule model)
        {
            int scheduleid = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    scheduleid = await doctorscheduleRepository.UpdateDoctorSchedule(model);
                    return Ok("Doctor Schedule Successfully Updated with ID : " + scheduleid);
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
        [Route("GetDoctorSchedules")]
        public async Task<IActionResult> GetDoctorSchedules()
        {
            try
            {
                var doctorschedules = await doctorscheduleRepository.GetDoctorSchedules();
                if (doctorschedules == null)
                {
                    return NotFound();
                }

                return Ok(doctorschedules);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("GetDoctorScheduleById/{scheduleid}")]
        public async Task<IActionResult> GetDoctorScheduleById(int? scheduleid)
        {
            if (scheduleid == null)
            {
                return BadRequest();
            }

            try
            {
                var doctorschedule = await doctorscheduleRepository.GetDoctorScheduleById(scheduleid);

                if (doctorschedule == null)
                {
                    return NotFound();
                }

                return Ok(doctorschedule);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}