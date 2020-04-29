using ImenikSem.Bussines.InterfejsiZaServise;
using ImenikSem.Bussines.Servisi;
using ImenikSem.Data.UnitOfWorkSve.Interfejsi;

namespace ImenikSem.Bussines
{
    public class Biznis : IBiznis
    {
        private readonly IUnitOfWork _unitOfWork;

        private IKontaktiServis kontaktiServis;
        private IKorisniciServis korisniciServis;

        public Biznis(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IKontaktiServis KontaktiServis
        {
            get
            {
                if(kontaktiServis == null)
                {
                    kontaktiServis = new KontaktiServis(_unitOfWork);
                }
                return kontaktiServis;
            }
        }

        public IKorisniciServis KorisniciServis
        {
            get
            {
                if (korisniciServis == null)
                {
                    korisniciServis = new KorisniciServis(_unitOfWork);
                }
                return korisniciServis;
            }
        }

    }
}
