using Dsw2026Ej15.Domain.Entities;
using System.Collections.Generic;

namespace Dsw2026Ej15.Data.Interfaces;

public interface IPersistence
{
    Task<Speciality?> GetSpecialityById(Guid id);
    Task SaveDoctor(Doctor doctor);
    Task<List<Doctor>?> GetDoctors();
    Task<Doctor?> GetDoctorById(Guid id);
    Task DeleteDoctor(Doctor doctor);

}