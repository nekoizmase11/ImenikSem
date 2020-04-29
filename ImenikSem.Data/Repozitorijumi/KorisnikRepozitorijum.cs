using ImenikSem.Data.Interfejsi;
using ImenikSem.Data.Repozitorijumi.GenerickiRepozitorijum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Data.Repozitorijumi
{
    public class KorisnikRepozitorijum : Repozitorijum<Korisnik>, IKorisnikRepozitorijum
    {
        public KorisnikRepozitorijum(DbContext context) : base(context)
        {
        }
    }
}
