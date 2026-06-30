using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities;

public class Doctor: EntityBase
{
    public string Name { get; init; }
    public string LicenseNumber { get; init; }
    public bool IsActive { get;  set; }
    public Guid? SpecialityId { get; private set; }
    public Speciality? Speciality { get; private set; }

    public Doctor(string name, string licenseNumber, Speciality speciality, Guid? id = null) : base(id)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        IsActive = true;
        Speciality = speciality;
    }
    public Doctor() {} // En teoria es para el arranque del EF


    public Doctor(Guid id, string name, string licenseNumber, bool isActive, Speciality speciality): base(id)
    {

        Name = name;
        LicenseNumber= licenseNumber;
        IsActive = isActive;
        Speciality= speciality;

    }
    public void Deactivate() => IsActive = false;

}
