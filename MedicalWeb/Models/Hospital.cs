using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class Hospital
    {
        public long HId { get; set; }
        [Display(Name = "Hospital Name")]
        [Required(ErrorMessage = "Hosptial Name required")]
        public string HospitalName { get; set; }

        [Display(Name = "Hospital Location")]
        [Required(ErrorMessage = "Hosptial Location required")]
        public string Location { get; set; }
        public long? Type { get; set; }
    }
}
