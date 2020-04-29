using ImenikSem.Data.Interfejsi;
using ImenikSem.Data.Repozitorijumi.GenerickiRepozitorijum;
using System.Data.Entity;

namespace ImenikSem.Data.Repozitorijumi
{
    public class KorisnikRepozitorijum : Repozitorijum<Korisnik>, IKorisnikRepozitorijum
    {
        public KorisnikRepozitorijum(DbContext context) : base(context)
        {
        }
    }
}
