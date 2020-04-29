using ImenikSem.Data.Interfejsi.GenerickiInterfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Data.Interfejsi
{
    public interface IKontaktRepozitorijum : IRepozitorijum<Kontakt>
    {
        List<Kontakt> NajcescePregledaniProcedura(int id);
    }
}
