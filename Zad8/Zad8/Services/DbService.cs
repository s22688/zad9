using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Zad8.Models;
using Zad8.Models.DTO;

namespace Zad8.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _dbContext;

        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddDoctor(Doctor doctor)
        {
            var doc = new Doctor { FirstName = doctor.FirstName, LastName = doctor.LastName, Email = doctor.Email };
            _dbContext.Add(doc);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDoctor(int id)
        {
            var check = await _dbContext.Doctors.AnyAsync(e => e.IdDoctor == id);
            if (!check) { return false; }

            var client = new Doctor() { IdDoctor = id };

            _dbContext.Attach(client);
            _dbContext.Remove(client);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<object>> GetDoctor(int id)
        {
            return await _dbContext.Doctors.Where(e => e.IdDoctor == id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetPerciption(int id)
        {
            return await _dbContext.Prescriptions
                .Include(e => e.Patient)
                .Include(e => e.Doctor)
                .Include(e => e.Prescription_Medicaments)
                .Where(e => e.IdPrescription == id)
                .Select(e => new SomeSortOfPrescription
                {
                    IdPrescription = e.IdPrescription,
                    Date = e.Date,
                    DueDate = e.DueDate,
                    Patient = new List<Patient> { e.Patient },
                    Doctor = new List<Doctor> { e.Doctor },
                    Medicaments = e.Prescription_Medicaments.Select(e =>  e.Medicament).ToList()

                }).ToListAsync();
        }

        public async Task<bool> PostDoctor(Doctor doctor, int id)
        {
            var doc = await _dbContext.Doctors.Where(e => e.IdDoctor == id).FirstOrDefaultAsync();

            if (!doctor.FirstName.Equals("")) doc.FirstName = doctor.FirstName;
            if (!doctor.LastName.Equals("")) doc.LastName = doctor.LastName;
            if (!doctor.Email.Equals("")) doc.Email = doctor.Email;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SendLoginData(LoginRequest loginRequest)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[8];
            provider.GetBytes(salt);

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(loginRequest.Password, salt, 100);


            var klient = await _dbContext.ProgramUser.Where(e => e.Login.Equals(loginRequest.Login)).FirstOrDefaultAsync();
            if (klient is null)
            {
                var userr = new ProgramUser { Login = loginRequest.Login, Password = BitConverter.ToString(pbkdf2.GetBytes(8))};
                _dbContext.ProgramUser.Add(userr);
                await _dbContext.SaveChangesAsync();
            }
            return true;

        }
    }
}


