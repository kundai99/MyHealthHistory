using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalWeb.Models
{
    public class Account
    {
        public long AccId { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name required")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category required")]
        public string Category { get; set; }
    }
}
