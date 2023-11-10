using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class Tempdata3
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

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name required")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }

        public List<Hospital> Hospitals { get; set; }
    }
}
