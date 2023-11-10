using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class PrivatePractice
    {
        public long HId { get; set; }
        [Display(Name = "Private Practice Name")]
        [Required(ErrorMessage = "Private Practice Name required")]
        public string HospitalName { get; set; }

        [Display(Name = "Private Practice Location")]
        [Required(ErrorMessage = "Private Practice Location required")]
        public string Location { get; set; }
    }
}
