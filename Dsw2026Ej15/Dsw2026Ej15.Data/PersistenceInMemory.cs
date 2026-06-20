using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Data.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private List<Speciality> _specialities = [];
    private List<Doctor> _doctors = [];

    public PersistenceInMemory()
    {
        LoadSpecialities();
        LoadDoctors();
    }

    private void LoadSpecialities()
    {
        try
        {
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
            var json = File.ReadAllText(jsonPath);
            var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];
            _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
        }
        catch (Exception)
        {

        }
    }
    private void LoadDoctors()
    {
        try
        {
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "doctors.json");
            var json = File.ReadAllText(jsonPath);
            var doctors = JsonSerializer.Deserialize<List<DoctorDto>>(json,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];
            foreach(var doc in doctors)
            {
                var speciality = _specialities.FirstOrDefault(s => s.Id == doc.SpecialityId);
                SaveDoctor(new Doctor(doc.id, doc.Name, doc.LicenseNumber,doc.IsActive ,speciality));
            }
        }
        catch (Exception)
        {

        }
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return _specialities.SingleOrDefault(s => s.Id == id);
    }

    public void SaveDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
    }


    public IEnumerable<Doctor> GetDoctors()
    {
        return _doctors.Where(d => d.IsActive == true);
    }

    public Doctor GetDoctorById(Guid id)
    {
        return _doctors.SingleOrDefault(d => d.Id == id);
    }

    public void DeleteDoctor(Guid id)
    {
        var doctor = GetDoctorById(id);
        doctor.IsActive = false;
    }

}