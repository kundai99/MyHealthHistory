using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class MedicalVisit
    {
        [Display(Name = " Visit Num")]
        public long MId { get; set; }

        public long PId { get; set; }

        [Display(Name = "Visit Date")]
        [Required(ErrorMessage = "Visit Date required")]
        public string Date { get; set; } = null!;

        [Display(Name = " BloodPressure")]
        [Required(ErrorMessage = "Blood Pressure required")]
        public long BloodPressure { get; set; }

        public long HId { get; set; }

        [Display(Name = " Visit or Stay")]
        [Required(ErrorMessage = "Visit or Stay required")]
        public string Stay { get; set; } = null!;

        public long DId { get; set; }
        [Display(Name = "Patient ID Number")]
        [Required(ErrorMessage = "Patient ID Number required")]
        public string PatientIdnumber { get; set; }
        [Display(Name = "Patient Temperature")]
        [Required(ErrorMessage = "Patient Temperature required")]
        public long? PatientTemperature { get; set; }
        [Display(Name = "Patient Weight")]
        [Required(ErrorMessage = "Patient Weight required")]
        public long? PatientWeight { get; set; }
        [Display(Name = "Symptoms")]
        [Required(ErrorMessage = "Symptoms required")]
        public string Symptoms { get; set; } = null!;
        [Display(Name = "Diagonosis")]
        [Required(ErrorMessage = "Patient Diagonosis required")]
        public string Diagnosis { get; set; } = null!;
        [Display(Name = "Prescription")]
        [Required(ErrorMessage = "Prescription required")]
        public string Prescription { get; set; }
    }
}
