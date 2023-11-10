using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class TempMedicalVisit2
    {
       
        [Display(Name = "Symptoms")]
        [Required(ErrorMessage = "Symptoms required")]
        public string Symptoms { get; set; } = null!;
       
        [Display(Name = "Diagonosis")]
        [Required(ErrorMessage = "Patient Diagonosis required")]
        public string Diagnosis { get; set; } = null!;

        [Display(Name = "Prescription")]
        [Required(ErrorMessage = "Prescription required")]
        public string Prescription { get; set; } = null!;

    }
}

