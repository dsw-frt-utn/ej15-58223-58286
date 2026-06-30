using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Xml.Linq;
using ValidationException = Dsw2026Ej15.Domain.Exceptions.ValidationException;

namespace Dsw2026Ej15.Api.Controllers;

public class DoctorsController : AppController
{
    private IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }


    [HttpPost("doctors")]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("Nombre y matricula son requeridos");
        }

        var speciality = await _persistence.GetSpecialityById(request.SpecialityId);
        if(speciality is null)
        {
            throw new ValidationException("La especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence?.SaveDoctor(doctor);

        return Created();

    }

    [HttpGet("doctors")]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = await _persistence.GetDoctors();
        return Ok(doctors);

    }

    [HttpGet("doctors/{id}")]
    public async Task<IActionResult> GetDoctorById([FromRoute]Guid id)
    {
        
        var doctor = await _persistence.GetDoctorById(id);

        if (doctor == null || !doctor.IsActive)
        {
            throw new ValidationException("No se encontro un doctor");
        }

        


        var response = new DoctorModel.Response
        (
           doctor.Name,
           doctor.LicenseNumber,
           doctor.Speciality?.Name
        );

        return Ok(response);
    }


    [HttpDelete("doctors/{id}")]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        var doctor = await _persistence.GetDoctorById(id);

        if (doctor == null || !doctor.IsActive)
        {
            return NotFound();
        }
        await _persistence.DeleteDoctor(doctor);
        return NoContent();
    }


}
