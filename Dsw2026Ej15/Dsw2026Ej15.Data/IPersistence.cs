using Dsw2026Ej15.Domain;
using System.Collections.Generic;

namespace Dsw2026Ej15.Data;

public interface IPersistence
{

    List<Speciality> Specialities { get; }
    List<Doctor> Doctors { get; }

    // Aquí luego agregaremos los métodos para insertar, buscar o eliminar médicos
}