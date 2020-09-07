using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ThinkBridge
{
    public class SignUp
    {
        public static IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            //Navigating to the URL
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://jt-dev.azurewebsites.net/#/SignUp");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void SignUpToApp()
        {

            //Initializing wait for page load up and avoiding exceptions
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            string title = driver.Title;

            if (title.Equals("Jabatalks"))
            {
                Console.WriteLine("Page loaded and Title is: " + title);
            }
            else
            {
                Console.WriteLine("Something went wrong!");
            }

            //Checking dropdown options
            string[] lang = { "English", "Dutch" };

            IWebElement listbox = driver.FindElement(By.XPath("//div[@placeholder='Choose Language']"));
            listbox.Click();
            Thread.Sleep(1000);

            //Since, Choose language is not a standard select box - Select cannot be used here
                for (int i = 0; i < lang.Length; i++)
                {
                if ((lang[i]).Equals(driver.FindElement(By.XPath("//ul[@class='ui-select-choices ui-select-choices-content ui-select-dropdown dropdown-menu ng-scope']//div[text()='" +lang[i]+"']")).Text))
                    {
                        Console.WriteLine("This value is equal to " + lang[i]);
                    }
                else
                    {
                    Console.WriteLine("This value is " + lang[i] + "not present");
                    }
                }
        //Calling SignupForm method
            SignUpForm();

            driver.Close();            
        }

        public static void SignUpForm()
        {
            //Signup
            IWebElement name = driver.FindElement(By.Id("name"));
            IWebElement orgName = driver.FindElement(By.Id("orgName"));
            IWebElement email = driver.FindElement(By.Id("singUpEmail"));
            IWebElement agreeChckbx = driver.FindElement(By.XPath("//span[text()='I agree to the']"));
            IWebElement submit = driver.FindElement(By.XPath("//button[text()='Get Started']"));
            
            name.SendKeys("Meetu Bhadoria");
            orgName.SendKeys("Meetu");
            email.SendKeys("test@gmail.com");
            agreeChckbx.Click();
            submit.Click();
            Thread.Sleep(5000);

            IWebElement msg = driver.FindElement(By.XPath("//span[text()=' A welcome email has been sent. Please check your email. ']"));

            if (msg.Displayed)
            {
                Console.WriteLine("Sign Up was successful");
            }
            else
            {
                Console.WriteLine("Please try again!");
            }
        }
    }
}