using Dsw2026Ej15.Data.Utils;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf: IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;
        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
            InitializeData();
        }
        public void InitializeData()
        {
            _context.Seedwork<Speciality>("specialities");
            _context.Seedwork<Doctor>("doctors");
        }


        public async Task SaveDoctor(Doctor doctor)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Doctor>?> GetDoctors()
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

        public async Task DeleteDoctor(Doctor doctor)
        {
            doctor.Deactivate();
            await _context.SaveChangesAsync();

        }
    }
}
