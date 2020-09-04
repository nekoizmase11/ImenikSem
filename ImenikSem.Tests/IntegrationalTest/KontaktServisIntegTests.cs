using AutoMapper;
using ImenikSem.Bussines.BiznisModeli;
using ImenikSem.Bussines.Infrastruktura.AutomaperConfiguracija;
using ImenikSem.Bussines.InterfejsiZaServise;
using ImenikSem.Bussines.Servisi;
using ImenikSem.Data;
using ImenikSem.Data.UnitOfWorkSve;
using ImenikSem.Data.UnitOfWorkSve.Interfejsi;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Tests.IntegrationalTest
{
    [TestFixture]
    class KontaktServisIntegTests
    {
        private IKontaktiServis _kontaktiServis;
        private List<Kontakt> listaKontakata;
        private IMapper _Maper;
        private IUnitOfWork _UnitOfWork;

        [SetUp]
        public void SetUp()
        {
            Kontakt kontakt1 = new Kontakt
            {
                Id = 1,
                Ime = "Milan",
                Prezime = "Milanovic",
                Korisnik_id = 1,
                Broj = "0654879652",
                BrojPregleda = 2,
                Mesto_id = 1
            };

            Kontakt kontakt2 = new Kontakt
            {
                Id = 2,
                Ime = "Zeljana",
                Prezime = "Ivanovic",
                Korisnik_id = 1,
                Broj = "0654839652",
                BrojPregleda = 3,
                Mesto_id = null
            };

            Kontakt kontakt3 = new Kontakt
            {
                Id = 3,
                Ime = "Milos",
                Prezime = "Milosevic",
                Korisnik_id = 1,
                Broj = "0633699633",
                BrojPregleda = 2,
                Mesto_id = 1
            };

            Kontakt kontakt4 = new Kontakt
            {
                Id = 3,
                Ime = "Zeljko",
                Prezime = "Zeljkovic",
                Korisnik_id = 2,
                Broj = "0633299633",
                BrojPregleda = 2,
                Mesto_id = 1
            };

            listaKontakata = new List<Kontakt> { kontakt1, kontakt2, kontakt3, kontakt4 };

            var configuracija = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapperBiznisProfil>();
            });

            _Maper = configuracija.CreateMapper();
            _UnitOfWork = new UnitOfWork(new ImenikBazaContext());
        }

        //Testovi

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void SviKontatiKorisnika_ShouldReturnListOfKontakt(int korisnikId)
        {
            //arange
            _kontaktiServis = new KontaktiServis(_UnitOfWork);

            //act
            List<KontaktBiznisModel> result = _kontaktiServis.SviKontatiKorisnika(korisnikId);

            //Assert
            Assert.IsNotNull(result);
            foreach (var item in result)
            {
                Assert.AreEqual(korisnikId, item.Korisnik_id);
                Assert.IsInstanceOf(typeof(KontaktBiznisModel), item);
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void InkrementBrojaPregleda_ShouldReturnTrue(int idKorisnika)
        {
            //arange
            List<Kontakt> listaKontakataPoId = listaKontakata.Where(x => x.Korisnik_id.Equals(idKorisnika)).ToList();
            _kontaktiServis = new KontaktiServis(_UnitOfWork);

            //act
            bool result = _kontaktiServis.InkrementBrojaPregleda(listaKontakataPoId);

            //Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(bool), result);
        }

        [Test]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void KontaktPoId_ShouldReturnKontakt(int id)
        {
            //arange
            _kontaktiServis = new KontaktiServis(_UnitOfWork);

            //act
            var result = _kontaktiServis.KontaktPoId(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.IsInstanceOf(typeof(KontaktBiznisModel), result);
        }

        [Test]
        public void KreirajKontakt_ShouldReturnTrue()
        {
            //arange
            _kontaktiServis = new KontaktiServis(_UnitOfWork);
            KontaktBiznisModel kontaktDto = _Maper.Map<KontaktBiznisModel>(listaKontakata[0]);

            //act
            bool result = _kontaktiServis.KreirajKontakt(kontaktDto);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
            Assert.IsInstanceOf(typeof(bool), result);
        }

        [Test]
        public void KreirajKontakt_ShouldReturnFalse()
        {
            //arange
            Kontakt kontakt = new Kontakt
            {
                Ime = "Milan",
                //Fali prezime
                Korisnik_id = 1,
                Broj = "0654879652",
                BrojPregleda = 2,
                Mesto_id = 1
            };
            _kontaktiServis = new KontaktiServis(_UnitOfWork);
            KontaktBiznisModel kontaktDto = _Maper.Map<KontaktBiznisModel>(kontakt);

            //act
            bool result = _kontaktiServis.KreirajKontakt(kontaktDto);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
            Assert.IsInstanceOf(typeof(bool), result);
        }

        [Test]
        [TestCase(5)]
        [TestCase(6)]
        public void NajcescePregledaniKontakti_ShouldReturnListOfKorisnikBiznisModel(int idKorisnika)
        {
            //arange
            _kontaktiServis = new KontaktiServis(_UnitOfWork);

            //act
            var result = _kontaktiServis.NajcescePregledaniKontakti(idKorisnika);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<KontaktBiznisModel>), result);
            foreach (var item in result)
            {
                Assert.AreEqual(idKorisnika, item.Korisnik_id);
            }
            

        } 

    }
}
