using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zad8.Models
{
    public class Medicament
    {
        [Key]
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; }
    }
}
