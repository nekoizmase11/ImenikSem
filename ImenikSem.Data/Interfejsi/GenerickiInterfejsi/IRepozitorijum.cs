using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Data.Interfejsi.GenerickiInterfejsi
{
    public interface IRepozitorijum<T> where T : class
    {
        T PretragaPoId(int id);
        IEnumerable<T> Svi();

        void Dodaj(T entitet);
        void Obrisi(T entitet);
    }
}
