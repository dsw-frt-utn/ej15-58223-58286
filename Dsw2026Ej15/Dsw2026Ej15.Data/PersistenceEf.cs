using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;
        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }
     
        public async Task AddDoctor(Doctor doctor)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Doctor>?> GetActiveDoctors()
        {
            return await _context.Doctors.
                Include(d => d.Speciality).
                Where(d => d.IsActive).
                ToListAsync();
        }

        public async Task<Doctor?> GetDoctorById(Guid id)
        {
            return await _context.Doctors.
                Include(d => d.Speciality).
                SingleOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<Speciality?> GetSpecialityById(Guid id)
        {
            return await _context.
                 Set<Speciality>().
                 SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task RemoveDoctor(Doctor doctor)
        {
            doctor.Deactivate();
            await _context.SaveChangesAsync();

        }

    }
}