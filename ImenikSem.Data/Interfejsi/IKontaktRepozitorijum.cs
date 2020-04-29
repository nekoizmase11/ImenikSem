using ImenikSem.Data.Interfejsi.GenerickiInterfejsi;
using System.Collections.Generic;

namespace ImenikSem.Data.Interfejsi
{
    public interface IKontaktRepozitorijum : IRepozitorijum<Kontakt>
    {
        List<Kontakt> NajcescePregledaniProcedura(int id);
    }
}
