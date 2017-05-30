using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

//using OpenQA.Selenium.Firefox;

// Requires reference to WebDriver.Support.dll
using OpenQA.Selenium.Support.UI;
//package Test.Gmail;


    namespace ConsoleApp1 { 
public class Selenium
{
    public static void Main()
    {

        //System.setProperty("webdriver.firefox.marionette", "C:\\Users\\samkitjain\\Downloads\\geckodriver-v0.16.1-win64\\geckodriver.exe");
        //driver =new FirefoxDriver();

        
        IWebDriver driver = new ChromeDriver("C:\\Users\\samkitjain\\Downloads\\chromedriver_win32");

        WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        // And now use this to visit Gmail
        driver.Navigate().GoToUrl("https://www.google.com/gmail/about/#");

        // Alternatively the same thing can be done like this
        // driver.navigate().to("https://www.google.com/gmail/about/#");


        IWebElement signin = driver.FindElement(By.LinkText("SIGN IN"));
        signin.Click();

        IWebElement username = driver.FindElement(By.XPath("//*[@id='identifierId']"));
        //enter username
        username.SendKeys("dummya481@gmail.com");
        username.Submit();
        //next 
        driver.FindElement(By.XPath("//*[@id='identifierNext']/content")).Click();

        // Check the title of the page
        Console.WriteLine("Page title is: " + driver.Title);

        //delay for password to arrive using the visibility of password tag
        myWait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='password']/div[1]/div/div[1]/input")));

        IWebElement password = driver.FindElement(By.XPath("//*[@id='password']/div[1]/div/div[1]/input"));
        password.SendKeys("asdfghjkl@12345");
        password.Submit();
        //Clicking next 
        driver.FindElement(By.XPath("//*[@id='passwordNext']")).Click();

        myWait.Until(ExpectedConditions.TitleContains("Inbox"));
        Console.WriteLine("Page title is: " + driver.Title);
        //getting initial count
        String inbox = driver.Title;
        int count1 = Convert.ToInt32(inbox.Substring(inbox.IndexOf('(') + 1, inbox.IndexOf(')')- inbox.IndexOf('(') - 1));

        Console.WriteLine(count1);

        IWebElement compose = driver.FindElement(By.XPath("//*[(text()='COMPOSE')]"));
        compose.Click();
        //wait till the compose dialog box appears
        myWait.Until(ExpectedConditions.ElementExists(By.XPath("//*[text()='Send']")));
        driver.FindElement(By.Name("to")).SendKeys("dummya481@gmail.com");
        driver.FindElement(By.Name("subjectbox")).SendKeys("Samkit - Test Script ");
        // finding XPath using contains 
        driver.FindElement(By.XPath("//*[contains(@class,'Am Al editable LW-avf')]")).SendKeys("First script in java");

        driver.FindElement(By.XPath("//*[text()='Send']")).Click();

        //wait until value changed to count +1
        myWait.Until(ExpectedConditions.TitleContains(Convert.ToString(count1 + 1)));


        String inbox2 = driver.Title;

        int count2 = Convert.ToInt32(inbox2.Substring(inbox2.IndexOf('(') + 1, inbox2.IndexOf(')')- inbox2.IndexOf('(') - 1));

        Console.WriteLine(inbox2);

        if (count2 - count1 == 1) Console.WriteLine("Successful");
        else Console.WriteLine("Not Successful");
        driver.FindElement(By.XPath("//*[contains(@class,'T-I J-J5-Ji nu T-I-ax7 L3')]")).Click();
            Console.ReadKey();
        // driver.quit();
    }
}
}
