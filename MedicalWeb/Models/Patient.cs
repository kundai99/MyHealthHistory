using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class Patient
    {

        public long PId { get; set; }
        [Display(Name = " First Name")]
        [Required(ErrorMessage = "First Name required")]
        public string Name { get; set; } = null!;
        [Display(Name = " Surname")]
        [Required(ErrorMessage = "Surname required")]
        public string Surname { get; set; } = null!;
        [Display(Name = "Id Number")]
        [Required(ErrorMessage = "Id Number required")]
        public string Id { get; set; } = null!;
        [Display(Name = "Sex")]
        [Required(ErrorMessage = "Sex required")]
        public string Sex { get; set; } = null!;
        [Display(Name = "Race")]
        [Required(ErrorMessage = "Race required")]
        public string Race { get; set; } = null!;
        [Display(Name = "Age")]
        [Required(ErrorMessage = "Age required")]
        public long Age { get; set; }

        public long AccId { get; set; }
    }
}
