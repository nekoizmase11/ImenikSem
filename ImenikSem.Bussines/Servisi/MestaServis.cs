using ImenikSem.Bussines.BiznisModeli;
using ImenikSem.Bussines.InterfejsiZaServise;
using ImenikSem.Data.UnitOfWorkSve.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Bussines.Servisi
{
    public class MestaServis : BazniServis, IMestaServis
    {
        private readonly IUnitOfWork _unitOfWork;


        public MestaServis(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<MestoBiznisModel> SvaMesta()
        {
            List< MestoBiznisModel> listaMestaBiznisModel = Maper.Map<List<MestoBiznisModel>>(_unitOfWork.Mesta.Svi().ToList());

            return listaMestaBiznisModel;
        }
    }
}
