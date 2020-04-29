using AutoMapper;
using ImenikSem.Bussines;
using ImenikSem.Bussines.BiznisModeli;
using ImenikSem.Data;
using ImenikSem.Prezentation.Web.AutoMapperConfiguracija;
using ImenikSem.Prezentation.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            List<KontaktBiznisModel> kontaktiBiznisModel = _biznis.KontaktiServis.SviKontatiKorisnika(idTrenutnogKorisnika);

            List<KontaktPrezentacioniModel> kontaktiPrezentacioniModel =
                Maper.Map<List<KontaktPrezentacioniModel>>(kontaktiBiznisModel);

            return View(kontaktiPrezentacioniModel);
        }

        [HttpGet]
        //[Route("api/Tablic/check")]
        public ActionResult GetStrana(int strana, int brKontakataPoStrani)
        {
            string emailKorisnika = System.Web.HttpContext.Current.User.Identity.Name;
            int idTrenutnogKorisnika = _biznis.KorisniciServis.KorisnikPoEmailu(emailKorisnika).Id;

            List<KontaktPrezentacioniModel> kontaktiPrezentacioniModel;

            kontaktiPrezentacioniModel =
                Maper.Map<List<KontaktPrezentacioniModel>>(_biznis.KontaktiServis.SviKontatiKorisnika(idTrenutnogKorisnika));

            PaginacijaBiznisModel<KontaktPrezentacioniModel> vracanje = _biznis.KontaktiServis.KontaktiPoStrani(kontaktiPrezentacioniModel, strana, brKontakataPoStrani);

            return Json(vracanje, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        //[Route("api/Tablic/check")]
        public ActionResult GetPretraga(int strana, int brKontakataPoStrani, string stringPretrage)
        {
            string emailKorisnika = System.Web.HttpContext.Current.User.Identity.Name;
            int idTrenutnogKorisnika = _biznis.KorisniciServis.KorisnikPoEmailu(emailKorisnika).Id;

            List<KontaktPrezentacioniModel> kontaktiPrezentacioniModel;

            kontaktiPrezentacioniModel =
                Maper.Map<List<KontaktPrezentacioniModel>>(_biznis.KontaktiServis.Pretraga(idTrenutnogKorisnika, stringPretrage));

            PaginacijaBiznisModel<KontaktPrezentacioniModel> vracanje = _biznis.KontaktiServis.KontaktiPoStrani(kontaktiPrezentacioniModel, strana, brKontakataPoStrani);

            return Json(vracanje, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [Authorize]
        public ActionResult Kreiraj()
        {
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
            //JObject sadrzajFajla = (JObject)JToken.FromObject(listaKorisnika);
            var s = JsonConvert.SerializeObject(listaKorisnika);

            var putanjaFajla = Server.MapPath("~/PodaciZaPreuzimanje/kontakti.json");
            System.IO.File.WriteAllText(putanjaFajla, s.ToString());

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "filename=fajl.json" );
            Response.TransmitFile(Server.MapPath("~/PodaciZaPreuzimanje/kontakti.json"));
            Response.End();
        }

    }
}