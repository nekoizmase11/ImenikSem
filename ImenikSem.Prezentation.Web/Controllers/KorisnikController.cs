using ImenikSem.Bussines;
using ImenikSem.Prezentation.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ImenikSem.Prezentation.Web.Controllers
{
    public class KorisnikController : BazniController
    {
        private readonly IBiznis _biznis;

        public KorisnikController(IBiznis biznis)
        {
            _biznis = biznis;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            KorisnikPrezentacioniModel loginPM = new KorisnikPrezentacioniModel();

            return View(loginPM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KorisnikPrezentacioniModel unetiKredencijali)
        {
            if (ModelState.IsValid)
            {
                if (_biznis.KorisniciServis.ProveriKorisnika(unetiKredencijali.Email, unetiKredencijali.Sifra))
                {
                    FormsAuthentication.SetAuthCookie(unetiKredencijali.Email, false);
                    Session["UlogovanKorisnik"] = unetiKredencijali.Email;

                    return RedirectToAction("Pocetna", "Kontakti");
                }
                else
                {
                    ViewData["Greska"] = "Pogresan email ili sifra!";
                    return View(unetiKredencijali);
                }
            }
            else
            {
                return View(unetiKredencijali);
            }
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }

    }
}