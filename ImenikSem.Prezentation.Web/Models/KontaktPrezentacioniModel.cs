using System.ComponentModel.DataAnnotations;

namespace ImenikSem.Prezentation.Web.Models
{
    public class KontaktPrezentacioniModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Niste uneli broj telefona!")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Samo slova su dozvoljena!")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Niste uneli broj telefona!")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Samo slova su dozvoljena!")]
        public string Prezime { get; set; }

        [Display(Name = "Broj telefona")]
        [Required(ErrorMessage = "Niste uneli broj telefona!")]
        [RegularExpression("^[+0][0-9]+", ErrorMessage = "Broj mora pocinjati sa nulom ili '+', slova nisu dozvoljena!")]
        [MinLength(6, ErrorMessage = "Minimalni broj cifara je 6!")]
        [MaxLength(10, ErrorMessage = "Maksimalni broj cifara je 10!")]
        public string Broj { get; set; }
    }
}