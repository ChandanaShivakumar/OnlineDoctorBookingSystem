using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprint.Models;
using Sprint.Repository;
using System;
using System.Threading.Tasks;

namespace Sprint.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository<Doctor> doctorRepository;

        public DoctorController(IDoctorRepository<Doctor> doctor)
        {
            doctorRepository = doctor;
        }
        [HttpPost]
        [Route("AddDoctor")]
        public async Task<IActionResult> AddDoctor(Doctor model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var doctorid = await doctorRepository.AddDoctor(model);
                    if (doctorid > 0)
                    {
                        return Ok("Doctor Details Added Successfully with ID : " + doctorid);
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
        [Route("DeleteDoctor/{doctorid}")]
        public async Task<IActionResult> DeleteDoctor(int? doctorid)
        {
            int result = 0;

            if (doctorid == null)
            {
                return BadRequest();
            }

            try
            {
                result = await doctorRepository.DeleteDoctor(doctorid);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok("Doctor Details Deleted Successfully.");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpPut]
        [Route("UpdateDoctor")]
        public async Task<IActionResult> UpdateDoctor(Doctor model)
        {
            int doctorid = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    doctorid = await doctorRepository.UpdateDoctor(model);
                    return Ok("Doctor Details Successfully Updated with ID : " + doctorid);
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
        [Route("GetDoctors")]
        public async Task<IActionResult> GetDoctors()
        {
            try
            {
                var doctors = await doctorRepository.GetDoctors();
                if (doctors == null)
                {
                    return NotFound("No Doctor Found.");
                }

                return Ok(doctors);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetDoctor/{doctorid}")]
        public async Task<IActionResult> GetDoctor(int? doctorid)
        {
            if (doctorid == null)
            {
                return BadRequest();
            }

            try
            {
                var doctor = await doctorRepository.GetDoctorById(doctorid);

                if (doctor == null)
                {
                    return NotFound();
                }

                return Ok(doctor);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}