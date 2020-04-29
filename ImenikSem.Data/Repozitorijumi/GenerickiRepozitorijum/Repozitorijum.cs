using ImenikSem.Data.Interfejsi.GenerickiInterfejsi;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ImenikSem.Data.Repozitorijumi.GenerickiRepozitorijum
{
    public class Repozitorijum<T> : IRepozitorijum<T> where T : class
    {
        private readonly DbContext _context;

        public Repozitorijum(DbContext context)
        {
            _context = context;
        }


        public void Dodaj(T entitet)
        {
            _context.Set<T>().Add(entitet);
        }

        public void Obrisi(T entitet)
        {
            _context.Set<T>().Remove(entitet);
        }

        public T PretragaPoId(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> Svi()
        {
            return _context.Set<T>().ToList();
        }
        

    }
}
