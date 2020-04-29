using System.Collections.Generic;

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
