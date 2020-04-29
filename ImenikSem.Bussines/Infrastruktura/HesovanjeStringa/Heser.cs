using System;
using System.Security.Cryptography;
using System.Text;

namespace ImenikSem.Bussines.Infrastruktura.HesovanjeStringa
{
    public static class Heser
    {
        public static string GenerisiHes(string password)
        {
            using (MD5 _md5 = MD5.Create())
            {
                string hes = "";
                byte[] sifrabajtovi = Encoding.UTF8.GetBytes(password);
                byte[] heshbajtovi = _md5.ComputeHash(sifrabajtovi);

                for (int i = 0; i < heshbajtovi.Length; i++)
                {
                    hes += heshbajtovi[i].ToString("x2");
                }

                return hes;
            }
        }

        public static bool VerifikujSifru(string unetaSifra, string hesovanaSifraKorisnika)
        {
            string unetaSifraHesovana = GenerisiHes(unetaSifra);

            string trimovanaHesovanaSifraKorisnika = hesovanaSifraKorisnika.Trim();

            var rez = string.Equals(unetaSifraHesovana, trimovanaHesovanaSifraKorisnika, StringComparison.Ordinal);

            return rez;
        }
    }
}
