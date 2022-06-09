using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sprint.Models;
using Sprint.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprint.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository<Patient> patientRepository;

        public PatientController(IPatientRepository<Patient> patient)
        {
            patientRepository = patient;
        }
        [HttpPost]
        [Route("AddPatient")]
        //call addpatient method from repository
        public async Task<IActionResult> AddPatient(Patient model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var patientid = await patientRepository.AddPatient(model);
                    if (patientid > 0)
                    {
                        return Ok("Patient Details Added Successfully with ID : " + patientid);
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
        [Route("DeletePatient/{patientid}")]
        //call delete patient from repository
        public async Task<IActionResult> DeletePatient(int? patientid)
        {
            int result = 0;

            if (patientid == null)
            {
                return BadRequest();
            }

            try
            {
                result = await patientRepository.DeletePatient(patientid);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok("Patient Details Deleted Successfully.");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpPut]
        [Route("UpdatePatient")]
        //call update patient from repository
        public async Task<IActionResult> UpdatePatient(Patient model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var patientid = await patientRepository.UpdatePatient(model);
                    return Ok("Patient Details Successfully Updated with ID : " + patientid);
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
        [Route("GetPatients")]
        //call getpatients from repository
        public async Task<IActionResult> GetPatients()
        {
            try
            {
                var patients = await patientRepository.GetPatients();
                if (patients == null)
                {
                    return NotFound("No Patient Found.");
                }

                return Ok(patients);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetPatientById/{patientid}")]
        //call getpatient from repository by passing id
        public async Task<IActionResult> GetPatientById(int? patientid)
        {
            if (patientid == null)
            {
                return BadRequest();
            }

            try
            {
                var patient = await patientRepository.GetPatientById(patientid);

                if (patient == null)
                {
                    return NotFound();
                }

                return Ok(patient);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}