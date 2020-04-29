using AutoMapper;
using ImenikSem.Bussines.BiznisModeli;
using ImenikSem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
