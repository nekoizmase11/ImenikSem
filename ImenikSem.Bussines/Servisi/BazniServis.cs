using AutoMapper;
using ImenikSem.Bussines.Infrastruktura.AutomaperConfiguracija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Bussines.Servisi
{
    public class BazniServis
    {
        public IMapper Maper { get; }
        
        public BazniServis()
        {
            var configuracija = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapperBiznisProfil>();
            });

            Maper = configuracija.CreateMapper();
        }
    }
}
