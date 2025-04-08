using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace CloudQAAutomationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting CloudQA Practice Form Automation Test...");
            
            FormAutomationTest tester = new FormAutomationTest();
            
            try
            {
                tester.Setup();
                tester.TestPracticeForm();
                Console.WriteLine("All tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            finally
            {   
                Console.WriteLine("Test execution completed. Press any key to exit...");
                Console.ReadKey();
                tester.Cleanup();
            }
        }
    }

    public class FormAutomationTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public void Setup()
        {
            Console.WriteLine("Setting up Chrome driver...");

            //Set up Chrome driver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            // Navigate to the practice form
            Console.WriteLine("Navigating to the practice form...");
            driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void TestPracticeForm()
        {
            // Test Field 1: First Name field in the iframe without id inner nested form

            Console.WriteLine("Testing first name field in the iframe without id inner nested form ==>");

            var iframeWithoutId = driver.FindElements(By.TagName("iframe"))[1];               //Outer iframe

            driver.SwitchTo().Frame(iframeWithoutId);

            var iframeWithoutIdNested = driver.FindElements(By.TagName("iframe"))[0];         //nested iframe

            driver.SwitchTo().Frame(iframeWithoutIdNested);

            var iframeWithoutIdInnerNested = driver.FindElements(By.TagName("iframe"))[0];    //nested Inner iframe

            driver.SwitchTo().Frame(iframeWithoutIdInnerNested);

            IWebElement nameField = FindElementWithMultipleStrategies(
                By.Name("First Name"),                        // By name
                By.Id("fname"),                              // By ID               
                By.CssSelector("input[placeholder*='Name' i]") // By CSS with case-insensitive placeholder
            );
            
            if (nameField == null)
                throw new Exception("Name field not found");
                
            nameField.Clear();
            nameField.SendKeys("Sahil");
            Console.WriteLine("//----------First Name field test passed!----------//");

            //Going to deafult driver
            driver.SwitchTo().DefaultContent();



            // Test Field 2: Email Field in the iframe with id

            Console.WriteLine("Testing email field in the iframe with id ==>");

            var iframeWithId = driver.FindElement(By.Id("iframeId"));
            driver.SwitchTo().Frame(iframeWithId);

            IWebElement emailField = FindElementWithMultipleStrategies(
                By.Id("email"),
                By.Name("Email"),
                By.CssSelector("input[type='email']")
            );

            if(emailField == null){
                throw new Exception("Email Field not found");
            }

            emailField.Clear();
            emailField.SendKeys("sahilfartyal3@gmail.com");

            Console.WriteLine("//----------Email Field test passed----------//");


            //Going to deafult driver
            driver.SwitchTo().DefaultContent();

            // Test Field 3: About yourself inside Shadow DOM 

            Console.WriteLine("Testing about yourself field in shadow DOM level 6 ==>");

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            string script = @"
                try {
                        const form = document.getElementById('nestedshadowdomautomationtestform6');
                        const firstElement = form.querySelector('nestedshadow-form6');
                        let current = firstElement;
                        
                        current = current.shadowRoot.querySelector('nestedshadow-form5');
                        current = current.shadowRoot.querySelector('nestedshadow-form4');
                        current = current.shadowRoot.querySelector('nestedshadow-form3');
                        current = current.shadowRoot.querySelector('nestedshadow-form');
                        const textarea = current.shadowRoot.querySelector('shadow-form section textarea.form-control');
                        
                        return textarea;
                    } catch(e) {
                        console.error('Shadow DOM traversal failed:', e);
                        return null;
                    }
                ";

            // Execute the script to get the element from the shadow DOM
            IWebElement targetElement = (IWebElement)js.ExecuteScript(script);

            // Now interact with the element
            targetElement.Clear();
            targetElement.SendKeys("Hi There, My name is Sahil and I love programming");

            Console.WriteLine("//----------About yourself Field test passed----------//");

            Console.WriteLine("//----------About yourself Field test passed----------//");
        }
        
        private IWebElement FindElementWithMultipleStrategies(params By[] strategies)
        {
            foreach (var strategy in strategies)
            {
                try
                {
                    // Try to find the element with the current strategy
                    var elements = driver.FindElements(strategy);
                    if (elements.Count > 0 && elements[0].Displayed)
                    {
                        Console.WriteLine($"Element found using strategy: {strategy}");
                        return elements[0];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Strategy {strategy} failed: {ex.Message}");
                    // Continue to the next strategy
                }
            }
            
            return null; // Return null if no strategy worked
        }

        public void Cleanup()
        {
            // Clean up after test execution
            Console.WriteLine("Cleaning up resources...");
            if (driver != null)
            {
                driver.Quit();
                Console.WriteLine("Chrome driver closed");
            }
        }
    }
}