using ImenikSem.Bussines.BiznisModeli;
using ImenikSem.Data;
using System.Collections.Generic;

namespace ImenikSem.Bussines.InterfejsiZaServise
{
    public interface IKontaktiServis
    {
        List<KontaktBiznisModel> SviKontatiKorisnika(int korisnikId);
        PaginacijaBiznisModel<T> KontaktiPoStrani<T>(List<T> sviKontakti, int brojStrane, int pageSize);
        List<KontaktBiznisModel> Pretraga(int korisnikId, string stringPretrage);
        bool KreirajKontakt(KontaktBiznisModel kontakt);
        KontaktBiznisModel KontaktPoId(int id);
        bool IzmeniKontakt(KontaktBiznisModel kontaktBM);
        bool ObrisiKontakt(int id);
        //procedura
        List<KontaktBiznisModel> NajcescePregledaniKontakti(int idKorisnika);
        bool InkrementBrojaPregleda(IEnumerable<Kontakt> ListaZaInkrement);
    }
}
