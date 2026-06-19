using Dsw2026Ej15.Domain;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    public List<Speciality> Specialities { get; private set; }
    public List<Doctor> Doctors { get; private set; }

    public PersistenceInMemory()
    {
   
        Specialities = new List<Speciality>();
        Doctors = new List<Doctor>();
        LoadSpecialities().Wait();
    }
    private async Task LoadSpecialities()
    {
        if (File.Exists("specialities.json"))
        {
            var json = await File.ReadAllTextAsync("specialities.json");
            Specialities = JsonSerializer.Deserialize<List<Speciality>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            });
        }
    }
}