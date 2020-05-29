using ImenikSem.Data.Interfejsi;
using ImenikSem.Data.Repozitorijumi.GenerickiRepozitorijum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Data.Repozitorijumi
{
    public class MestoRepozitorijum : Repozitorijum<Mesto>, IMestoRepozitorijum
    {
        public MestoRepozitorijum(DbContext context) : base(context)
        {

        }
    }
}
