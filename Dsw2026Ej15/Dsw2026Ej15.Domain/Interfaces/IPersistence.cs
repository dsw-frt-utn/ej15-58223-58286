using Dsw2026Ej15.Domain.Entities;
using System.Collections.Generic;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    Speciality? GetSpecialityById(Guid id);
    void SaveDoctor(Doctor doctor);
    IEnumerable<Doctor> GetDoctors();

    // Aquí luego agregaremos los métodos para insertar, buscar o eliminar médicos
}