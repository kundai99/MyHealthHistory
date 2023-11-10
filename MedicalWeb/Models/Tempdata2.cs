using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class Tempdata2
    {
        public long DId { get; set; }
        [Display(Name = " First Name")]
        [Required(ErrorMessage = "First Name required")]
        public string Name { get; set; } = null!;


        [Display(Name = " Surname")]
        [Required(ErrorMessage = "Surname required")]
        public string Surname { get; set; } = null!;


        [Display(Name = " Professional Number")]
        [Required(ErrorMessage = "Professional Number required")]
        public string ProfessionalNum { get; set; } = null!;


        [Display(Name = " Speciality")]
        [Required(ErrorMessage = "Speciality required")]
        public string Speciality { get; set; } = null!;

        public List<Hospital> Hospitals { get; set; }
    }
}
