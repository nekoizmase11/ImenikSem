using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImenikSem.Prezentation.Web.Models
{
    public class KontaktSaMestimaPrezentacioniModel
    {
        //[Required(ErrorMessage = "Niste uneli ime!")]
        //[RegularExpression("[a-zA-Z]+", ErrorMessage = "Samo slova su dozvoljena!")]
        public string Ime { get; set; }

        //[Required(ErrorMessage = "Niste uneli prezime!")]
        //[RegularExpression("[a-zA-Z]+", ErrorMessage = "Samo slova su dozvoljena!")]
        public string Prezime { get; set; }

        //[Display(Name = "Broj telefona")]
        //[Required(ErrorMessage = "Niste uneli broj telefona!")]
        //[RegularExpression("^[+0][0-9]+", ErrorMessage = "Broj mora pocinjati sa nulom ili '+', slova nisu dozvoljena!")]
        //[MinLength(6, ErrorMessage = "Minimalni broj cifara je 6!")]
        //[MaxLength(10, ErrorMessage = "Maksimalni broj cifara je 10!")]
        public string Broj { get; set; }
        public string NazivMesta { get; set; }
        //spublic List<Mesto> ListaMesta { get; set; }
    }
}