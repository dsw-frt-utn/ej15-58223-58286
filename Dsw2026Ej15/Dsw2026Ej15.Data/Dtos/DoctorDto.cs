using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data.Dtos
{
    public record DoctorDto(Guid id, string Name, string LicenseNumber, bool IsActive, Guid SpecialityId);
}
