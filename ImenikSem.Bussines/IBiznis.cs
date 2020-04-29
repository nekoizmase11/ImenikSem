using ImenikSem.Bussines.InterfejsiZaServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Bussines
{
    public interface IBiznis
    {
        IKontaktiServis KontaktiServis { get; }
        IKorisniciServis KorisniciServis { get; }
    }
}
