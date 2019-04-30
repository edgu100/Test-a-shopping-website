using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
       
        IWebDriver driver;
        String emailAcc, firstname, lastname, password;

        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
            emailAcc = RandomString(8) + "@gmail.com";
            password = "12345";
            lastname = RandomString(6);
            firstname = RandomString(6);
        }

        public void login()
        {
            driver.Navigate().GoToUrl("http://automationpractice.com");
            driver.FindElement(By.ClassName("login")).Click();
            driver.FindElement(By.XPath("//input[@id='email']")).SendKeys(emailAcc);
            driver.FindElement(By.XPath("//input[@id='passwd']")).SendKeys(password);
            driver.FindElement(By.XPath("//button[@id='SubmitLogin']")).Click();
            driver.FindElement(By.XPath("//a[@class='logout']"));
        }

        private static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for(int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public void createAccount()
        {
            driver.Navigate().GoToUrl("http://automationpractice.com");
            driver.FindElement(By.ClassName("login")).Click();
            IWebElement emailAddr = driver.FindElement(By.XPath(".//*[@id='email_create']"));
            emailAddr.SendKeys(emailAcc);
            driver.FindElement(By.Id("SubmitCreate")).Click();
            driver.FindElement(By.XPath("//input[@id='id_gender1']")).Click();
            driver.FindElement(By.XPath("//input[@id='customer_firstname']")).SendKeys(firstname);
            driver.FindElement(By.XPath("//input[@id='customer_lastname']")).SendKeys(lastname);
            driver.FindElement(By.XPath("//input[@id='passwd']")).SendKeys(password);
            SelectElement bdDays = new SelectElement(driver.FindElement(By.XPath("//select[@id='days']")));
            bdDays.SelectByValue("25");
            SelectElement bdMonth = new SelectElement(driver.FindElement(By.XPath("//select[@id='months']")));
            bdMonth.SelectByValue("5");
            SelectElement bdYear = new SelectElement(driver.FindElement(By.XPath("//select[@id='years']")));
            bdYear.SelectByValue("1990");
            driver.FindElement(By.XPath("//input[@id='company']")).SendKeys("Wintec");
            driver.FindElement(By.XPath("//input[@id='address1']")).SendKeys("Tristram St");
            driver.FindElement(By.XPath("//input[@id='city']")).SendKeys("Hamilton");
            SelectElement state = new SelectElement(driver.FindElement(By.XPath("//select[@id='id_state']")));
            state.SelectByText("California");
            driver.FindElement(By.XPath("//input[@id='postcode']")).SendKeys("55555");
            SelectElement country = new SelectElement(driver.FindElement(By.XPath("//select[@id='id_country']")));
            country.SelectByText("United States");
            driver.FindElement(By.XPath("//input[@id='phone_mobile']")).SendKeys("5084561945");
            driver.FindElement(By.XPath("//button[@id='submitAccount']")).Click();
            driver.FindElement(By.XPath("//a[@class='logout']")).Click();
            return;
        }

         [TestMethod]
        public void TestCreateAccAndLogin()
        {
            Initialize();
            createAccount();
            login();
            driver.Quit();
        }

        [TestMethod]
        public void TestOrder()
        {
            Initialize();
            createAccount();
            driver.FindElement(By.ClassName("login")).Click();
            driver.FindElement(By.XPath("//input[@id='email']")).SendKeys(emailAcc);
            driver.FindElement(By.XPath("//input[@id='passwd']")).SendKeys(password);
            driver.FindElement(By.XPath("//button[@id='SubmitLogin']")).Click();
            driver.FindElement(By.XPath("//a[@title='Women']")).Click();
            driver.FindElement(By.XPath("//img[@alt='Faded Short Sleeve T-shirts']")).Click();
            driver.FindElement(By.XPath("//button[@name='Submit']")).Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//*[@id = 'layer_cart']/div[1]/div[2]/div[4]/a/span")));
            driver.FindElement(By.XPath(".//*[@id='layer_cart']/div[1]/div[2]/div[4]/a/span")).Click();
            driver.FindElement(By.XPath(".//*[@id='center_column']/p[2]/a[1]/span")).Click();
            driver.FindElement(By.XPath(".//*[@id='center_column']/form/p/button")).Click();
            driver.FindElement(By.XPath(".//input[@id='cgv']")).Click();
            driver.FindElement(By.XPath("//button[@name='processCarrier']")).Click();
            driver.FindElement(By.XPath("//a[@class='bankwire']")).Click();
            driver.FindElement(By.XPath("//button[@class='button btn btn-default button-medium']")).Click();
            IWebElement orderComplete = driver.FindElement(By.XPath("//strong[contains(.,'Your order on My Store is complete.')]"));
            String orderCompletedExpected = "Your order on My Store is complete.";
            String orderCompletedActual = orderComplete.Text;
            Assert.AreEqual(orderCompletedActual, orderCompletedExpected);
            driver.Quit();
        }
    }
}
