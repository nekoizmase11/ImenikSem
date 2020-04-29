using ImenikSem.Bussines.BiznisModeli;

namespace ImenikSem.Bussines.InterfejsiZaServise
{
    public interface IKorisniciServis
    {
        bool ProveriKorisnika(string Email, string Sifra);
        KorisnikBiznisModel KorisnikPoEmailu(string email);
    }
}
