using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ValidationException = Dsw2026Ej15.Domain.Exceptions.ValidationException;
using NotFoundException = Dsw2026Ej15.Domain.Exceptions.NotFoundException;
using System.Xml.Linq;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorController : ControllerBase
{
    private readonly IPersistence _persistenceDoctores;

    public DoctorController(IPersistence doctorsData)
    {
        _persistenceDoctores = doctorsData;
    }
   
    [HttpPost]
    public async Task<IActionResult> AddDoctor([FromBody] DoctorModel.Request request)
    {
        var speciality = await _persistenceDoctores.GetSpecialityById(request.SpecialityId);

        if (string.IsNullOrWhiteSpace(request.Name) ||
        string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("No se permiten campos vacíos");
        }
        if (speciality is null) throw new ValidationException("No existe la especialidad indicada");

        var newDoctor = new Doctor(request.Name, request.LicenseNumber, speciality);

        await _persistenceDoctores.AddDoctor(newDoctor);
        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveDoctors()
    {
        var doctors = await _persistenceDoctores.GetActiveDoctors();

        var doctoresListados = new List<DoctorModel.Response>();

        foreach (Doctor doctor  in doctors)
        {
            var doctormodel = new DoctorModel.Response(doctor.Id,doctor.Name,doctor.LicenseNumber, doctor.Speciality?.Name , doctor.Speciality.Description, doctor.Speciality.Id);
            doctoresListados.Add(doctormodel);
        }

        return Ok(doctoresListados);
    }

    [HttpGet("doctors/{id}")]
    public async Task<IActionResult> GetDoctorById([FromRoute] Guid id)
    {
        var doctor = await _persistenceDoctores.GetDoctorById(id);

        if (doctor == null)
        {

            throw new NotFoundException("No se encontro un doctor");
        }

        var response = new DoctorModel.Response
        (
           doctor.Id,
           doctor.Name,
           doctor.LicenseNumber,
           doctor.Speciality?.Name,
           doctor.Speciality.Description,
           doctor.Speciality.Id
        );

        return Ok(response);
    }

    [HttpDelete("doctors/{id}")]
    public async Task<IActionResult> DeleteDoctor([FromRoute] Guid id)
    {
        var doctor = await _persistenceDoctores.GetDoctorById(id);

        if (doctor == null || !doctor.IsActive)
        {
            throw new NotFoundException("El doctor debe existir y estar activo");
        }
        _persistenceDoctores.RemoveDoctor(doctor);

        return NoContent();
    }
}
