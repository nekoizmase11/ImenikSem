
using System;

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
        public Nullable<int> Mesto_id { get; set; }
        public string NazivMesta { get; set; }
    }
}
