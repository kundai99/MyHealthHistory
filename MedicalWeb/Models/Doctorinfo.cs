using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class Doctorinfo
    {
        public long Did { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name required")]
        public string FName { get; set; }
        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Surname required")]
        public string SName { get; set; }
        [Display(Name = "Professional Number")]
        [Required(ErrorMessage = "Professional Number required")]
        public string ProfessionalNum { get; set; } = null!;
        [Display(Name = "Speciality")]
        [Required(ErrorMessage = "Speciality required")]
        public string Speciality { get; set; } = null!;
        [Display(Name = "Hospital Name")]
        [Required(ErrorMessage = "Hospital Name required")]
        public string HospitalName { get; set; }
    }
}
