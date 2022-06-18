using Microsoft.EntityFrameworkCore;
using System;

namespace Zad8.Models
{
    public class MainDbContext : DbContext
    {
        protected MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; } 
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; } 
        public DbSet<Medicament> Medicaments { get; set; } 
        public DbSet<ProgramUser> ProgramUser { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(p =>
            {
                p.HasKey(e => e.IdPatient);
                p.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                p.Property(e => e.BirthDate).IsRequired();

                p.HasData(
                    new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", BirthDate = DateTime.Parse("2000-01-25")},
                    new Patient { IdPatient = 2, FirstName = "Krzysztof", LastName = "Ibisz", BirthDate = DateTime.Parse("1907-07-11") },
                    new Patient { IdPatient = 3, FirstName = "Anna", LastName = "Hanna", BirthDate = DateTime.Parse("1975-11-09") }
                    );
            });

            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e => e.Email).IsRequired().HasMaxLength(100);

                d.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Zbigniew", LastName = "Zginiewowski", Email = "Zibi@gmail.com"},
                    new Doctor { IdDoctor = 2, FirstName = "Katarzyna", LastName = "Kasia", Email = "Kasia@gmail.com" }
                    );
            });

            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();
               
                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);

                p.HasData(
                    new Prescription { IdPrescription = 1, Date = DateTime.Parse("2022-01-21"), DueDate = DateTime.Parse("2022-05-22"), IdDoctor = 1, IdPatient = 1},
                    new Prescription { IdPrescription = 2, Date = DateTime.Parse("2022-02-22"), DueDate = DateTime.Parse("2022-07-22"), IdDoctor = 2, IdPatient = 3 },
                    new Prescription { IdPrescription = 3, Date = DateTime.Parse("2022-04-24"), DueDate = DateTime.Parse("2022-11-22"), IdDoctor = 2, IdPatient = 2 }
                    );
            });

            modelBuilder.Entity<Medicament>(m =>
            {
                m.HasKey(e => e.IdMedicament);
                m.Property(e => e.Name).IsRequired().HasMaxLength(100);
                m.Property(e => e.Description).IsRequired().HasMaxLength(100);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);

                m.HasData(
                    new Medicament { IdMedicament = 1, Name = "XXX", Description = "AAABBBCCCDDDEEEFFF1", Type = "ToNose"},
                    new Medicament { IdMedicament = 2, Name = "YYY", Description = "AAABBBCCCDDDEEEFFF2", Type = "ToNose" },
                    new Medicament { IdMedicament = 3, Name = "ZZZ", Description = "AAABBBCCCDDDEEEFFF3", Type = "ToNose" }
                    );
                
            });

            modelBuilder.Entity<Prescription_Medicament>(m =>
            {
                m.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                m.Property(e => e.Dose); ;
                m.Property(e => e.Details).IsRequired().HasMaxLength(100);

                m.HasOne(e => e.Medicament).WithMany(e => e.Prescription_Medicaments).HasForeignKey(e => e.IdMedicament);
                m.HasOne(e => e.Prescription).WithMany(e => e.Prescription_Medicaments).HasForeignKey(e => e.IdPrescription);

                m.HasData(
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 3, Dose = 7, Details = "AXYW"},
                    new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 8, Details = "AXYW" },
                    new Prescription_Medicament { IdMedicament = 3, IdPrescription = 1, Dose = 9, Details = "AXYW" }
                    );
            });

            modelBuilder.Entity<ProgramUser>(u =>
            {
                u.HasKey(e => e.Login);
                u.Property(e => e.Password).IsRequired().HasMaxLength(500);


            });
        }


    }
}
