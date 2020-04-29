using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Bussines.BiznisModeli
{
    public class KontaktBiznisModel
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Korisnik_id { get; set; }
        public string Broj { get; set; }
        public int? BrojPregleda { get; set; }
    }
}
