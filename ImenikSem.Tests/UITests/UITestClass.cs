using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace ImenikSem.Tests.UITests
{
    [TestFixture]
    public class UITestClass
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [TearDown] 

        protected void TearDown()
        {
            driver.Quit();
        }

        //Testovi

        [Test]
        public void Login_ShouldSuccsed()
        {
            driver.Navigate().GoToUrl("https://localhost:44368/Korisnik/Login");
            
            driver.FindElement(By.Id("Sifra")).SendKeys("sifra");
            driver.FindElement(By.Name("Email")).SendKeys("milorad@gmail.com");
            driver.FindElement(By.CssSelector(".btn")).Click();

            var searchField = driver.FindElement(By.Id("pretraga"));
            var searchFieldName = searchField.TagName;

            Assert.IsNotNull(searchField);
            StringAssert.AreEqualIgnoringCase("input", searchFieldName);
        }

        [Test]
        public void CreatePage_ShouldOpen()
        {
            driver.Navigate().GoToUrl("https://localhost:44368/Korisnik/Login");

            driver.FindElement(By.Id("Sifra")).SendKeys("sifra");
            driver.FindElement(By.Name("Email")).SendKeys("milorad@gmail.com");
            driver.FindElement(By.CssSelector(".btn")).Click();

            driver.FindElement(By.LinkText("Dodaj novi kontakt")).Click();

            var pageHeadline = driver.FindElement(By.Id("naslov"));
            var pageHeadlineElName = pageHeadline.TagName;
            var pageHeadlineText = pageHeadline.Text;

            Assert.IsNotNull(pageHeadline);
            StringAssert.AreEqualIgnoringCase("h2", pageHeadlineElName);
            StringAssert.AreEqualIgnoringCase("Dodaj novi kontakt:", pageHeadlineText);
        }

        [Test]
        public void Top10Page_shouldReturn10Contats()
        {
            driver.Navigate().GoToUrl("https://localhost:44368/Korisnik/Login");

            driver.FindElement(By.Id("Sifra")).SendKeys("sifra");
            driver.FindElement(By.Name("Email")).SendKeys("milorad@gmail.com");
            driver.FindElement(By.CssSelector(".btn")).Click();

            driver.FindElement(By.LinkText("Najcesce pretrazivani")).Click();
            var contentOfTable = driver.FindElements(By.TagName("tr"));

            Assert.IsNotNull(contentOfTable);
            Assert.AreEqual(11, contentOfTable.Count);
        }

        [Test]
        public void EditPage_ShouldOpen()
        {
            driver.Navigate().GoToUrl("https://localhost:44368/Korisnik/Login");

            driver.FindElement(By.Id("Sifra")).SendKeys("sifra");
            driver.FindElement(By.Name("Email")).SendKeys("milorad@gmail.com");
            driver.FindElement(By.CssSelector(".btn")).Click();

            driver.Navigate().GoToUrl("https://localhost:44368/Kontakti/Izmeni/42");

            var pageHeadline = driver.FindElement(By.Id("naslov"));
            var pageHeadlineElName = pageHeadline.TagName;
            var pageHeadlineText = pageHeadline.Text;

            Assert.IsNotNull(pageHeadline);
            StringAssert.AreEqualIgnoringCase("h2", pageHeadlineElName);
            StringAssert.AreEqualIgnoringCase("Izmena kontakta", pageHeadlineText);
        }

        [Test]
        public void EditPage_ShouldChangeContactLastname()
        {
            driver.Navigate().GoToUrl("https://localhost:44368/Korisnik/Login");

            driver.FindElement(By.Id("Sifra")).SendKeys("sifra");
            driver.FindElement(By.Name("Email")).SendKeys("milorad@gmail.com");
            driver.FindElement(By.CssSelector(".btn")).Click();

            driver.Navigate().GoToUrl("https://localhost:44368/Kontakti/Izmeni/42");
            var lastnameBefore = driver.FindElement(By.Id("Prezime")).GetAttribute("value");
            driver.FindElement(By.Id("Prezime")).SendKeys("c");
            driver.FindElement(By.CssSelector(".btn")).Click();

            driver.Navigate().GoToUrl("https://localhost:44368/Kontakti/Izmeni/42");
            var lastnameAfter = driver.FindElement(By.Id("Prezime")).GetAttribute("value");

            Assert.IsNotNull(lastnameBefore);
            Assert.IsNotNull(lastnameAfter);
            StringAssert.AreNotEqualIgnoringCase(lastnameBefore, lastnameAfter);
        }

        [Test]
        public void Login_ShouldFail()
        {
            driver.Navigate().GoToUrl("https://localhost:44368/Korisnik/Login");

            driver.FindElement(By.Id("Sifra")).SendKeys("pogresnaSifra");
            driver.FindElement(By.Name("Email")).SendKeys("milorad@gmail.com");
            driver.FindElement(By.CssSelector(".btn")).Click();

            var pageHeadline = driver.FindElement(By.Id("naslov")).Text;         

            StringAssert.AreEqualIgnoringCase("Ulogujte se:", pageHeadline);

        }

        [Test]
        public void Logo_ShouldLoadDefaultPage()
        {
            driver.Navigate().GoToUrl("https://localhost:44368/Korisnik/Login");

            driver.FindElement(By.Id("Sifra")).SendKeys("sifra");
            driver.FindElement(By.Name("Email")).SendKeys("milorad@gmail.com");
            driver.FindElement(By.CssSelector(".btn")).Click();

            driver.FindElement(By.LinkText("IMENIK")).Click();

            var pageHeadline = driver.FindElement(By.Id("naslov")).Text;

            Assert.IsNotNull(pageHeadline);
            StringAssert.AreEqualIgnoringCase("Svi kontakti:", pageHeadline);
        }

        [Test]
        public void Create_ShouldFail()
        {
            driver.Navigate().GoToUrl("https://localhost:44368/Korisnik/Login");

            driver.FindElement(By.Id("Sifra")).SendKeys("sifra");
            driver.FindElement(By.Name("Email")).SendKeys("milorad@gmail.com");
            driver.FindElement(By.CssSelector(".btn")).Click();

            driver.FindElement(By.LinkText("Dodaj novi kontakt")).Click();
            driver.FindElement(By.CssSelector(".btn")).Click();

            var pageHeadline = driver.FindElement(By.Id("naslov"));
            var pageHeadlineElName = pageHeadline.TagName;
            var pageHeadlineText = pageHeadline.Text;

            Assert.IsNotNull(pageHeadline);
            StringAssert.AreEqualIgnoringCase("h2", pageHeadlineElName);
            StringAssert.AreEqualIgnoringCase("Dodaj novi kontakt:", pageHeadlineText);
        }

        [Test]
        public void Logout_ShouldRedirectToLoginPage()
        {
            driver.Navigate().GoToUrl("https://localhost:44368/Korisnik/Login");

            driver.FindElement(By.Id("Sifra")).SendKeys("sifra");
            driver.FindElement(By.Name("Email")).SendKeys("milorad@gmail.com");
            driver.FindElement(By.CssSelector(".btn")).Click();

            driver.FindElement(By.Id("lgout")).Click();

            var pageHeadline = driver.FindElement(By.Id("naslov")).Text;

            Assert.IsNotNull(pageHeadline);
            StringAssert.AreEqualIgnoringCase("Ulogujte se:", pageHeadline);
        }

        [Test]
        public void BackPageButton_ShouldReturnBack()
        {
            driver.Navigate().GoToUrl("https://localhost:44368/Korisnik/Login");

            driver.FindElement(By.Id("Sifra")).SendKeys("sifra");
            driver.FindElement(By.Name("Email")).SendKeys("milorad@gmail.com");
            driver.FindElement(By.CssSelector(".btn")).Click();

            driver.FindElement(By.LinkText("Dodaj novi kontakt")).Click();
            driver.FindElement(By.LinkText("Natrag na kontakte")).Click();

            var pageHeadline = driver.FindElement(By.Id("naslov")).Text;

            Assert.IsNotNull(pageHeadline);
            StringAssert.AreEqualIgnoringCase("Svi kontakti:", pageHeadline);
        }

    }
}
