using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class TempMedicalVisit
    {
        public long MId { get; set; }
        [Display(Name = "Patient Name")]
        [Required(ErrorMessage = "Patient Name required")]
        public string FName { get; set; }

        [Display(Name = "Patient Surname")]
        [Required(ErrorMessage = "Patient Surname required")]
        public string SName { get; set; }

        [Display(Name = "Patient Age")]
        [Required(ErrorMessage = "Patient Age required")]
        public long Age { get; set; }
        [Display(Name = "Patient ID Number")]
        [Required(ErrorMessage = "Patient ID Number required")]
        public string PatientIdnumber { get; set; }

        [Display(Name = "Visit Date")]
        [Required(ErrorMessage = "Visit Date required")]
        public DateTime Date { get; set; } 
       
        [Display(Name = " BloodPressure")]
        [Required(ErrorMessage = "First Name required")]
        public long BloodPressure { get; set; }
        
        [Display(Name = " Visit or Stay")]
        [Required(ErrorMessage = "Visit or Stay required")]
        public string Stay { get; set; } = null!;

        [Display(Name = "Patient Temperature in degrees Celsius")]
        [Required(ErrorMessage = "Patient Temperature required")]
        public long? PatientTemperature { get; set; }
        [Display(Name = "Patient Weight in KG")]
        [Required(ErrorMessage = "Patient Weight required")]
        public long? PatientWeight { get; set; }
       

    }
}

