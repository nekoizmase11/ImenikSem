using AutoMapper;
using ImenikSem.Bussines.BiznisModeli;
using ImenikSem.Prezentation.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImenikSem.Prezentation.Web.AutoMapperConfiguracija
{
    public class MapperPrezentacioniProfil : Profile
    {
        public MapperPrezentacioniProfil()
        {
            CreateMap<KontaktBiznisModel, KontaktPrezentacioniModel>();
            CreateMap<KontaktPrezentacioniModel, KontaktBiznisModel>();

            CreateMap<MestoPrezentacioniModel, MestoBiznisModel>();
            CreateMap<MestoBiznisModel, MestoPrezentacioniModel>();
        }


    }
}