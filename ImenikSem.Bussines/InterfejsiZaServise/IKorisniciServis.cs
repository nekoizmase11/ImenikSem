using ImenikSem.Bussines.BiznisModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Bussines.InterfejsiZaServise
{
    public interface IKorisniciServis
    {
        bool ProveriKorisnika(string Email, string Sifra);
        KorisnikBiznisModel KorisnikPoEmailu(string email);
    }
}
