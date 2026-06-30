using Dsw2026Ej15.Domain.Entities;
using System.Collections.Generic;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    Task<List<Doctor>?> GetActiveDoctors();
    Task<Doctor?> GetDoctorById(Guid id);
    Task<Speciality?> GetSpecialityById(Guid id);
    Task AddDoctor(Doctor doctor);
    Task RemoveDoctor(Doctor doctor);


}