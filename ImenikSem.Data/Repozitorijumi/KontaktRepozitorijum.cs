using ImenikSem.Data.Interfejsi;
using ImenikSem.Data.Repozitorijumi.GenerickiRepozitorijum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Data.Repozitorijumi
{
    public class KontaktRepozitorijum : Repozitorijum<Kontakt>, IKontaktRepozitorijum
    {
        private readonly DbContext _context;

        public KontaktRepozitorijum(DbContext context) : base(context)
        {
            _context = context;
        }

        public List<Kontakt> NajcescePregledaniProcedura(int id)
        {
            
            object parametar = new SqlParameter("@Korisnik_id", id);         

            var results = _context.Database.SqlQuery<Kontakt>("NajcescePregledani @Korisnik_id", parametar);
            _context.Database.Log = query => System.Diagnostics.Debug.Write(query);
            List<Kontakt> listaKontakata = results.ToList();

            return listaKontakata;
        }
    }
}
