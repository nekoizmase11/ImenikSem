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
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImenikSem.Tests.UnitTests
{
    [TestFixture]
    public class KontaktiServisTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private IKontaktiServis _kontaktiServis;
        private List<Kontakt> listaKontakata;
        private Mesto mesto;
        private IMapper _Maper;

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

            mesto = new Mesto
            {
                Id = 1,
                NazivMesta = "Zrenjanin"
            };

            listaKontakata = new List<Kontakt> { kontakt1, kontakt2, kontakt3, kontakt4 };

            _unitOfWork = new Mock<IUnitOfWork>();

            var configuracija = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapperBiznisProfil>();
            });

            _Maper = configuracija.CreateMapper();
        }


        //Tests

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void SviKontatiKorisnika_ShouldReturnListOfKontakt(int korisnikId)
        {
            //arange
            _unitOfWork.Setup(X => X.Kontakti.Svi()).Returns(listaKontakata);
            _unitOfWork.Setup(x => x.Mesta.PretragaPoId(It.IsAny<int>())).Returns(mesto);
            _kontaktiServis = new KontaktiServis(_unitOfWork.Object);

            Kontakt kontakt = listaKontakata.First(x => x.Korisnik_id.Equals(korisnikId));

            //act
            var result = _kontaktiServis.SviKontatiKorisnika(korisnikId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(kontakt.Id, result[0].Id);
            Assert.AreEqual(korisnikId, result[0].Korisnik_id);
            Assert.IsInstanceOf(typeof(List<KontaktBiznisModel>), result);
            //Assert.Contains(kontakt, result);

        }

        [Test]
        public void InkrementBrojaPregleda_ShouldReturnTrue()
        {
            //arange
            _unitOfWork.Setup(X => X.KomitujIzmene()).Returns(listaKontakata.Count);
            _kontaktiServis = new KontaktiServis(_unitOfWork.Object);

            //act
            bool result = _kontaktiServis.InkrementBrojaPregleda(listaKontakata);

            //Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(bool), result);
        }

        [Test]
        public void InkrementBrojaPregleda_ShouldReturnFalse()
        {
            //arange
            _unitOfWork.Setup(X => X.KomitujIzmene()).Throws(new DbUpdateException());
            _kontaktiServis = new KontaktiServis(_unitOfWork.Object);

            //act
            bool result = _kontaktiServis.InkrementBrojaPregleda(listaKontakata);

            //Assert
            Assert.IsFalse(result);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(bool), result);
        }
        
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void KontaktPoId_ShouldReturnKontakt(int id)
        {
            //arange
            Kontakt kontakt = listaKontakata.First(x => x.Id.Equals(id));
            _unitOfWork.Setup(x => x.Kontakti.PretragaPoId(It.IsAny<int>())).Returns(kontakt);
            _kontaktiServis = new KontaktiServis(_unitOfWork.Object);

            //act
            var result = _kontaktiServis.KontaktPoId(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(kontakt.Id, result.Id);
            Assert.IsInstanceOf(typeof(KontaktBiznisModel),result);
        }

        [Test]
        public void KreirajKontakt_ShouldReturnTrue()
        {
            //arange
            _unitOfWork.Setup(X => X.Kontakti.Dodaj(listaKontakata[0]));
            _unitOfWork.Setup(X => X.KomitujIzmene()).Returns(1);
            _kontaktiServis = new KontaktiServis(_unitOfWork.Object);
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
            _unitOfWork.Setup(X => X.Kontakti.Dodaj(listaKontakata[0]));
            _unitOfWork.Setup(X => X.KomitujIzmene()).Throws(new DbUpdateException());
            _kontaktiServis = new KontaktiServis(_unitOfWork.Object);
            KontaktBiznisModel kontaktDto = _Maper.Map<KontaktBiznisModel>(listaKontakata[0]);


            //act
            bool result = _kontaktiServis.KreirajKontakt(kontaktDto);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
            Assert.IsInstanceOf(typeof(bool), result);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void NajcescePregledaniKontakti_ShouldReturnListOfKorisnikBiznisModel(int idKorisnika)
        {
            //arange
            List<Kontakt> ListaKontakataPoId = listaKontakata.Where(x => x.Korisnik_id.Equals(idKorisnika)).ToList();
            List<KontaktBiznisModel> listaKontakataDto = _Maper.Map<List<KontaktBiznisModel>>(ListaKontakataPoId);

            _unitOfWork.Setup(X => X.Kontakti.NajcescePregledaniProcedura(idKorisnika)).Returns(ListaKontakataPoId);
            _unitOfWork.Setup(x => x.Mesta.PretragaPoId(It.IsAny<int>())).Returns(mesto);
            _kontaktiServis = new KontaktiServis(_unitOfWork.Object);

            //act
            var result = _kontaktiServis.NajcescePregledaniKontakti(idKorisnika);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ListaKontakataPoId[0].Id, result[0].Id);
            Assert.IsInstanceOf(typeof(KontaktBiznisModel), result[0]);
            Assert.AreEqual(idKorisnika, result[0].Korisnik_id);

        }

    }
}
