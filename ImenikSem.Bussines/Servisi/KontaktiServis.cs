using ImenikSem.Bussines.BiznisModeli;
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
    public class KontaktiServis : BazniServis,  IKontaktiServis
    {
        private readonly IUnitOfWork _unitOfWork;

        public KontaktiServis(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<KontaktBiznisModel> SviKontatiKorisnika(int korisnikId)
        {
            IEnumerable<Kontakt> listaKontakataPoKorisniku = _unitOfWork.Kontakti.Svi().Where(kontakt => kontakt.Korisnik_id.Equals(korisnikId));

            List<KontaktBiznisModel> listaBiznisModelaKontakataPoKorisniku =
                Maper.Map<List<KontaktBiznisModel>>(listaKontakataPoKorisniku);

            return listaBiznisModelaKontakataPoKorisniku.OrderBy(on => on.Ime).ToList();
        }

        public PaginacijaBiznisModel<T> KontaktiPoStrani<T>(List<T> sviKontakti, int brojStrane, int brojKontakataPoStrani)
        {

            var brojItema = sviKontakti.Count();
            var items = sviKontakti.Skip((brojStrane - 1) * brojKontakataPoStrani).Take(brojKontakataPoStrani).ToList();
            var ukupnoStrana = (int)Math.Ceiling(brojItema / (double)brojKontakataPoStrani);
            List<int> paginacija = new List<int>();

            if(brojStrane < 3)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i + 1 <= ukupnoStrana)
                        paginacija.Add(i + 1);
                }
            }else if(brojStrane > ukupnoStrana - 2 && brojStrane > 3)
            {
                for (int i = ((ukupnoStrana-1) - 4); i < ukupnoStrana; i++)
                {
                    //if (i + 1 <= ukupnoStrana)
                        paginacija.Add(i + 1);
                }
            }
            else
            {
                for (int i = brojStrane - 3; i < brojStrane + 2; i++)
                {
                    if(i < ukupnoStrana) paginacija.Add(i + 1);
                }
            }


            return new PaginacijaBiznisModel<T>(items, brojItema, brojStrane, brojKontakataPoStrani, ukupnoStrana, paginacija);

        }

        public List<KontaktBiznisModel> Pretraga(int korisnikId, string stringPretrage)
        {
            IEnumerable<Kontakt> listaKontakataPoKorisniku = _unitOfWork.Kontakti.Svi().Where(kontakt => kontakt.Korisnik_id.Equals(korisnikId));
            IEnumerable<Kontakt> FiltriranaLista = new List<Kontakt>();
            IEnumerable<Kontakt> ListaZaInkrement = new List<Kontakt>();

            if (Char.IsLetter(stringPretrage[0]))
            {
                //pretraga kontakata za prikaz
                FiltriranaLista = listaKontakataPoKorisniku.Where(kontakt => kontakt.Ime.ToLower().Contains(stringPretrage.ToLower()));

                //pretraga kontakata za inkrement najcesce pregledanih
                ListaZaInkrement = FiltriranaLista.Where(kontakt => kontakt.Ime.Equals(stringPretrage, StringComparison.OrdinalIgnoreCase));
                if (!String.IsNullOrWhiteSpace(stringPretrage))
                {
                    bool rez = InkrementBrojaPregleda(ListaZaInkrement);
                }
            }
            else if(stringPretrage[0] == '+' || Char.IsDigit(stringPretrage[0]))
            {
                FiltriranaLista = listaKontakataPoKorisniku.Where(kontakt => kontakt.Broj.ToLower().StartsWith(stringPretrage.ToLower()));

                ListaZaInkrement = FiltriranaLista.Where(kontakt => kontakt.Broj.Equals(stringPretrage, StringComparison.OrdinalIgnoreCase));
                if (!String.IsNullOrWhiteSpace(stringPretrage))
                {
                    bool rez = InkrementBrojaPregleda(ListaZaInkrement);
                }
            }

            List<KontaktBiznisModel> listaBiznisModelaKontakataPoKorisniku =
                Maper.Map<List<KontaktBiznisModel>>(FiltriranaLista);           

            return listaBiznisModelaKontakataPoKorisniku.OrderBy(on => on.Ime).ToList();
        }

        public bool InkrementBrojaPregleda(IEnumerable<Kontakt> ListaZaInkrement)
        {
            foreach (Kontakt kontakt in ListaZaInkrement)
            {
                try
                {
                    kontakt.BrojPregleda = kontakt.BrojPregleda == null ? 1 : kontakt.BrojPregleda + 1;

                    _unitOfWork.KomitujIzmene();
                }
                catch (Exception)
                {

                    return false;
                }
            }

            return true;
        }

        public bool KreirajKontakt(KontaktBiznisModel kontaktBM)
        {
            Kontakt kontakt = Maper.Map<Kontakt>(kontaktBM);

            try
            {
                _unitOfWork.Kontakti.Dodaj(kontakt);
                _unitOfWork.KomitujIzmene();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public KontaktBiznisModel KontaktPoId(int id)
        {
            Kontakt kontakt =_unitOfWork.Kontakti.PretragaPoId(id);
            KontaktBiznisModel kontaktBM = Maper.Map<KontaktBiznisModel>(kontakt);

            return (kontaktBM);
        }

        public bool IzmeniKontakt(KontaktBiznisModel kontaktBM)
        {
            Kontakt kontakt = _unitOfWork.Kontakti.PretragaPoId(kontaktBM.Id);
            kontakt.Ime = kontaktBM.Ime;
            kontakt.Prezime = kontaktBM.Prezime;
            kontakt.Broj = kontaktBM.Broj;

            try
            {
                _unitOfWork.KomitujIzmene();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool ObrisiKontakt(int id)
        {
            try
            {
                Kontakt kontakt = _unitOfWork.Kontakti.PretragaPoId(id);

                _unitOfWork.Kontakti.Obrisi(kontakt);
                _unitOfWork.KomitujIzmene();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<KontaktBiznisModel> NajcescePregledaniKontakti(int idKorisnika)
        {
            List<Kontakt> listaKontakata = _unitOfWork.Kontakti.NajcescePregledaniProcedura(idKorisnika);
            List<KontaktBiznisModel> listaBM = Maper.Map<List<KontaktBiznisModel>>(listaKontakata);

            return listaBM;

        }

    }
}
