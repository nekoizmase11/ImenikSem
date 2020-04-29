using System.ComponentModel.DataAnnotations;

namespace ImenikSem.Prezentation.Web.Models
{
    public class KorisnikPrezentacioniModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Sifra:")]
        public string Sifra { get; set; }
    }
}