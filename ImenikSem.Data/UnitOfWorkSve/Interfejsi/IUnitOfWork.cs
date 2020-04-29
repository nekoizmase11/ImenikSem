using ImenikSem.Data.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Data.UnitOfWorkSve.Interfejsi
{
    public interface IUnitOfWork :IDisposable
    {
        IKorisnikRepozitorijum Korisnici { get; }
        IKontaktRepozitorijum Kontakti { get; }
        int KomitujIzmene();
    }
}
