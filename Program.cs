using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace testeSelenium
{
    class Program
    {
        /// <summary>
        /// This Class has no comments because it was meant to
        /// Show me what i could do with selenium and .net core
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            string Email = "user email";
            string Pass = "Password";
            string BuyerEmail = "BuyerEmail";
            int Value = 1;
            string ItemName = "item name";
            string ItemDescription = "Description of the Item";
            string Notes = "THIS IS A TEST. DO NOT PAY THIS";

            using (IWebDriver driver = new FirefoxDriver())
            {
                driver.Navigate().GoToUrl("http://www.paypal.com/signin");
                IWebElement query = driver.FindElement(By.Id("email"));
                query.SendKeys(Email);
                query = driver.FindElement(By.Id("password"));
                query.SendKeys(Pass);
                query.Submit();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Url.Contains("paypal.com/myaccount/home"));
                driver.Navigate().GoToUrl("https://www.paypal.com/invoice/create");
                
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Title.StartsWith("fatura",StringComparison.OrdinalIgnoreCase));

                query = driver.FindElement(By.Id("billtotokenfield-tokenfield"));
                query.SendKeys(BuyerEmail);

                query = driver.FindElement(By.Id("defaultItemUnitTypeDiv"));
                query.Click();

                query = driver.FindElement(By.Id("justTotal"));
                query.Click();

                query = driver.FindElement(By.Id("itemName_0"));
                query.SendKeys(ItemName);

                query = driver.FindElement(By.Id("itemDescription_0"));
                query.SendKeys(ItemDescription);

                query = driver.FindElement(By.Id("itemPrice_0"));
                query.SendKeys(Value.ToString());


                IWebElement notes = driver.FindElement(By.Id("notes"));
                notes.SendKeys(Notes);

                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
                var btns = driver.FindElements(By.Id("previewInvoice"));
                IWebElement btn = btns[1];
                btn.Click();

                ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementById('header').style.display = 'none'");

                IWebElement send = driver.FindElement(By.Id("sendActionButton"));
                Actions a = new Actions(driver);
                a.MoveToElement(send).Perform();
                send.Click();
            }
        }
    }
}
