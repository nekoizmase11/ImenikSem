using ImenikSem.Bussines.InterfejsiZaServise;

namespace ImenikSem.Bussines
{
    public interface IBiznis
    {
        IKontaktiServis KontaktiServis { get; }
        IKorisniciServis KorisniciServis { get; }
        IMestaServis MestaServis { get; }
    }
}
