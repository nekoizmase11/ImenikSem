using ImenikSem.Data.Interfejsi;
using ImenikSem.Data.Repozitorijumi;
using ImenikSem.Data.UnitOfWorkSve.Interfejsi;
using System.Data.Entity;

namespace ImenikSem.Data.UnitOfWorkSve
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public IKontaktRepozitorijum kontakti;
        public IKorisnikRepozitorijum korisnici;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IKorisnikRepozitorijum Korisnici
        {
            get
            {
                if (korisnici == null)
                {
                    korisnici = new KorisnikRepozitorijum(_context);
                }
                return korisnici;
            }
        }
        public IKontaktRepozitorijum Kontakti
        {
            get
            {
                if (kontakti == null)
                {
                    kontakti = new KontaktRepozitorijum(_context);
                }
                return kontakti;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int KomitujIzmene()
        {
            return _context.SaveChanges();
        }
    }
}
