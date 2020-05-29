using ImenikSem.Bussines;
using ImenikSem.Bussines.BiznisModeli;
using ImenikSem.Data;
using ImenikSem.Prezentation.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ImenikSem.Prezentation.Web.Controllers
{
    public class KontaktiController : BazniController
    {
        private readonly IBiznis _biznis;

        public KontaktiController(IBiznis biznis)
        {
            _biznis = biznis;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Pocetna()
        {
            string emailKorisnika = System.Web.HttpContext.Current.User.Identity.Name;
            int idTrenutnogKorisnika = _biznis.KorisniciServis.KorisnikPoEmailu(emailKorisnika).Id;

            Session["UlogovanKorisnik"] = emailKorisnika;

            List<KontaktBiznisModel> kontaktiBiznisModel = _biznis.KontaktiServis.SviKontatiKorisnika(idTrenutnogKorisnika);

            List<KontaktPrezentacioniModel> kontaktiPrezentacioniModel =
                Maper.Map<List<KontaktPrezentacioniModel>>(kontaktiBiznisModel);

            return View(kontaktiPrezentacioniModel);
        }

        //Web api
        [HttpGet]
        public ActionResult GetStrana(int strana, int brKontakataPoStrani)
        {
            string emailKorisnika = System.Web.HttpContext.Current.User.Identity.Name;
            if (!String.IsNullOrWhiteSpace(emailKorisnika))
            {
                int idTrenutnogKorisnika = _biznis.KorisniciServis.KorisnikPoEmailu(emailKorisnika).Id;

                List<KontaktPrezentacioniModel> kontaktiPrezentacioniModel;

                kontaktiPrezentacioniModel =
                    Maper.Map<List<KontaktPrezentacioniModel>>(_biznis.KontaktiServis.SviKontatiKorisnika(idTrenutnogKorisnika));

                PaginacijaBiznisModel<KontaktPrezentacioniModel> vracanje = _biznis.KontaktiServis.KontaktiPoStrani(kontaktiPrezentacioniModel, strana, brKontakataPoStrani);

                return Json(vracanje, JsonRequestBehavior.AllowGet); 
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Morate biti ulogovani u aplikaciju 'Imenik'");

        }

        //Web api
        [HttpGet]
        public ActionResult GetPretraga(int strana, int brKontakataPoStrani, string stringPretrage)
        {
            string emailKorisnika = System.Web.HttpContext.Current.User.Identity.Name;
            if (!String.IsNullOrWhiteSpace(emailKorisnika))
            {
                int idTrenutnogKorisnika = _biznis.KorisniciServis.KorisnikPoEmailu(emailKorisnika).Id;

                List<KontaktPrezentacioniModel> kontaktiPrezentacioniModel;

                kontaktiPrezentacioniModel =
                    Maper.Map<List<KontaktPrezentacioniModel>>(_biznis.KontaktiServis.Pretraga(idTrenutnogKorisnika, stringPretrage));

                PaginacijaBiznisModel<KontaktPrezentacioniModel> vracanje = _biznis.KontaktiServis.KontaktiPoStrani(kontaktiPrezentacioniModel, strana, brKontakataPoStrani);

                return Json(vracanje, JsonRequestBehavior.AllowGet); 
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Morate biti ulogovani u aplikaciju 'Imenik'");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Kreiraj()
        {

            List<MestoBiznisModel> list = _biznis.MestaServis.SvaMesta();

            ViewBag.NazivMesta = new SelectList(list, "Id", "NazivMesta");

            return View(new KontaktPrezentacioniModel());
        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Kreiraj(KontaktPrezentacioniModel kontaktPM)
        {
            if (ModelState.IsValid)
            {
                string emailKorisnika = System.Web.HttpContext.Current.User.Identity.Name;
                int idTrenutnogKorisnika = _biznis.KorisniciServis.KorisnikPoEmailu(emailKorisnika).Id;

                KontaktBiznisModel kontaktDataModel = Maper.Map<KontaktBiznisModel>(kontaktPM);
                kontaktDataModel.Korisnik_id = idTrenutnogKorisnika;

                if(kontaktPM.NazivMesta != null)
                {
                    kontaktDataModel.Mesto_id = Int32.Parse(kontaktPM.NazivMesta);
                }
                
                bool rezultat = _biznis.KontaktiServis.KreirajKontakt(kontaktDataModel);

                if(rezultat == true) return RedirectToAction("Pocetna", "Kontakti");

                ViewData["Greska"] = "Nije uspelo kreiranje novog kontakta";
            }

            return View(kontaktPM);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Izmeni(int id)
        {
            KontaktBiznisModel kontaktBM = _biznis.KontaktiServis.KontaktPoId(id);
            KontaktPrezentacioniModel kontaktPM = Maper.Map<KontaktPrezentacioniModel>(kontaktBM);

            List<MestoBiznisModel> list = _biznis.MestaServis.SvaMesta();
            SelectList SelectItemLista;
            if (kontaktBM.Mesto_id != null)
            {
                SelectItemLista = new SelectList(list, "Id", "NazivMesta", kontaktBM.Mesto_id);
            }
            else
            {
                 SelectItemLista = new SelectList(list, "Id", "NazivMesta");
            }

            ViewBag.NazivMesta = SelectItemLista;

            return View(kontaktPM);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Izmeni(KontaktPrezentacioniModel kontaktPM)
        {
            if (ModelState.IsValid)
            {
                KontaktBiznisModel kontaktBM = Maper.Map<KontaktBiznisModel>(kontaktPM);
                if (kontaktPM.NazivMesta != null)
                {
                    kontaktBM.Mesto_id = Int32.Parse(kontaktPM.NazivMesta);
                }

                bool rezultat =_biznis.KontaktiServis.IzmeniKontakt(kontaktBM);

                if (rezultat == true) return RedirectToAction("Pocetna", "Kontakti");

                ViewData["Greska"] = "Nije uspela izmena kontakta";
            }

            return View();
        }

        [Authorize]
        public ActionResult Obrisi(int id)
        {
            bool rezultat =_biznis.KontaktiServis.ObrisiKontakt(id);

            if(rezultat == true) return RedirectToAction("Pocetna", "Kontakti");

            ViewData["Greska"] = "Nije uspelo brisanje kontakta";
            return RedirectToAction("Pocetna", "Kontakti");
        }

        [Authorize]
        public ActionResult NajcescePretrazivani()
        {
            string emailKorisnika = System.Web.HttpContext.Current.User.Identity.Name;
            int idTrenutnogKorisnika = _biznis.KorisniciServis.KorisnikPoEmailu(emailKorisnika).Id;

            List<KontaktBiznisModel> listaBM = _biznis.KontaktiServis.NajcescePregledaniKontakti(idTrenutnogKorisnika);
            List<KontaktPrezentacioniModel> listaKontakataPM = Maper.Map<List<KontaktPrezentacioniModel>>(listaBM);

            return View(listaKontakataPM);
        }

        public void PreuzmiKontakte()
        {
            string emailKorisnika = System.Web.HttpContext.Current.User.Identity.Name;
            int idTrenutnogKorisnika = _biznis.KorisniciServis.KorisnikPoEmailu(emailKorisnika).Id;

            var listaKorisnika = _biznis.KontaktiServis.SviKontatiKorisnika(idTrenutnogKorisnika);
            var s = JsonConvert.SerializeObject(listaKorisnika);

            var putanjaFajla = Server.MapPath("~/PodaciZaPreuzimanje/kontakti.json");
            System.IO.File.WriteAllText(putanjaFajla, s.ToString());

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "filename=kontakti.json");
            Response.TransmitFile(Server.MapPath("~/PodaciZaPreuzimanje/kontakti.json"));
            Response.End();
        }

    }
}