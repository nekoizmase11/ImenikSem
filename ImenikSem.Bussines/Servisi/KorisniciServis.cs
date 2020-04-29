using ImenikSem.Bussines.BiznisModeli;
using ImenikSem.Bussines.Infrastruktura.HesovanjeStringa;
using ImenikSem.Bussines.InterfejsiZaServise;
using ImenikSem.Data;
using ImenikSem.Data.UnitOfWorkSve.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Bussines.Servisi
{
    public class KorisniciServis : BazniServis, IKorisniciServis
    {
        private readonly IUnitOfWork _unitOfWork;

        public KorisniciServis(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool ProveriKorisnika(string Email, string Sifra)
        {
            //List<Korisnik> sviKorisnici = _unitOfWork.Korisnici.Svi().ToList();

            //Korisnik korisnik = sviKorisnici.Find(kor => kor.Email.Equals(Email));

            var korisnik = KorisnikPoEmailu(Email);

            if (korisnik != null)
            {
                string hasovanaSifraKorisnika = korisnik.Sifra;

                return Heser.VerifikujSifru(Sifra, hasovanaSifraKorisnika);
                
            }
            else
            {
                return false;
            }
        }

        public KorisnikBiznisModel KorisnikPoEmailu(string email)
        {
            List<Korisnik> sviKorisnici = _unitOfWork.Korisnici.Svi().ToList();
            Korisnik korisnik = sviKorisnici.Find(kor => kor.Email.Equals(email));

            return Maper.Map<KorisnikBiznisModel>(korisnik);
        }

    }
}
