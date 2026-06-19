using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using Dsw2026Ej15.Domain.Entities;

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
            return BadRequest("Nombre y matricula son requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if(speciality is null)
        {
            return BadRequest("La especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);

        return Created();

    }

    [HttpGet("doctors")]
    public async Task<IActionResult> ObtenerDoctores()
    {

        return Ok(_persistence.GetDoctors());

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        
        var doctor = _persistence.GetDoctorById(id);

        if (doctor == null)
        {
            return NotFound(); 
        }

        
        var resultado = new
        {
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality?.Name 
        };

      
        return Ok(resultado);
    }
}
