using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
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

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if(speciality is null)
        {
            throw new ValidationException("La especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);

        return Created();

    }

    [HttpGet("doctors")]
    public async Task<IActionResult> GetDoctors()
    {

        return Ok(_persistence.GetDoctors());

    }

    [HttpGet("doctors/{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        
        var doctor = _persistence.GetDoctorById(id);

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
        var doctor = _persistence.GetDoctorById(id);
        if(!doctor.IsActive || doctor is null)
        {
            throw new ValidationException("El doctor debe existir y estar activo");
        }
        _persistence.DeleteDoctor(id);

        return NoContent ();
    }


}
