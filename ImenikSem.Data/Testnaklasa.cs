using ImenikSem.Data.UnitOfWorkSve;
using ImenikSem.Data.UnitOfWorkSve.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Data
{
    public class Testnaklasa
    {
        public IUnitOfWork unitOfWork { get; set; }
        public Testnaklasa()
        {
            unitOfWork = new UnitOfWork(new ImenikBazaContext());
        }

        public List<Kontakt> SviKontakti()
        {
            return unitOfWork.Kontakti.Svi().ToList();
        }
    }
}
