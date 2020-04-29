using AutoMapper;
using ImenikSem.Prezentation.Web.AutoMapperConfiguracija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImenikSem.Prezentation.Web.Controllers
{
    public class BazniController : Controller
    {
        public IMapper Maper { get; }
        public BazniController()
        {
            var configuracija = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapperPrezentacioniProfil>();
            });

            Maper = configuracija.CreateMapper();
        }        
    }
}