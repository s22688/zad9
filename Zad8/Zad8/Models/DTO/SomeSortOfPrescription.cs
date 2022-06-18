using System;
using System.Collections.Generic;

namespace Zad8.Models.DTO
{
    public class SomeSortOfPrescription
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }

        public IEnumerable<Patient> Patient { get; set; }
        public IEnumerable<Doctor> Doctor { get; set; }
        public IEnumerable<Medicament> Medicaments { get; set; }
    }
}
