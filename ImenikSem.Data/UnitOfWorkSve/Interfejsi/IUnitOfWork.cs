using ImenikSem.Data.Interfejsi;
using System;

namespace ImenikSem.Data.UnitOfWorkSve.Interfejsi
{
    public interface IUnitOfWork :IDisposable
    {
        IKorisnikRepozitorijum Korisnici { get; }
        IKontaktRepozitorijum Kontakti { get; }
        int KomitujIzmene();
    }
}
