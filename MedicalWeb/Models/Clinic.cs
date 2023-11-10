using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class Clinic
    {
        public long HId { get; set; }
        [Display(Name = "Clinic Name")]
        [Required(ErrorMessage = "Clinic Name required")]
        public string HospitalName { get; set; }

        [Display(Name = "Clinic Location")]
        [Required(ErrorMessage = "Clinic Location required")]
        public string Location { get; set; }
    }
}
