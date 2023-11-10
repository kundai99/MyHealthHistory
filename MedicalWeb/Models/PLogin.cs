using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
	public class PLogin
	{
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name required")]
        public string Username { get; set; }
	}
}
