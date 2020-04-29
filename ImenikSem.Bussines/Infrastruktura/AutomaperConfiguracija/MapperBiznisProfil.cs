using AutoMapper;
using ImenikSem.Bussines.BiznisModeli;
using ImenikSem.Data;

namespace ImenikSem.Bussines.Infrastruktura.AutomaperConfiguracija
{
    public class MapperBiznisProfil : Profile
    {
        public MapperBiznisProfil()
        {
            CreateMap<Kontakt, KontaktBiznisModel>();
            CreateMap<KontaktBiznisModel, Kontakt>();

            CreateMap<Korisnik, KorisnikBiznisModel>();

        }
    }
}
