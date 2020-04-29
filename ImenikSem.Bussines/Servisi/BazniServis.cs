using AutoMapper;
using ImenikSem.Bussines.Infrastruktura.AutomaperConfiguracija;

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
