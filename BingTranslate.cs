using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


//using OpenQA.Selenium.Firefox;

// Requires reference to IWebDriver.Support.dll
using OpenQA.Selenium.Support.UI;
using System.IO;
//package Test.Gmail;


public class BingTranslate
{

    public static IWebDriver chromeDriver()
    {

        IWebDriver driver = new ChromeDriver("C:\\Users\\samkitjain\\Downloads\\chromedriver_win32");

        WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        // And now use this to visit Gmail
        
        return driver;
    }

    public static void ChkInput(IWebDriver driver, String input)
    {
        IWebElement inputBox = driver.FindElement(By.CssSelector("textarea.srcTextarea"));
        String text = inputBox.GetAttribute("value");

        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Input field is empty");
        }
        else
        {
            Console.WriteLine("Input field is not empty");
        }

        if (text.Equals(input))
        {
            Console.WriteLine("Input Fields match");
        }
        else
        {
            Console.WriteLine("Error! Different Input field");
        }
    }

    public static void ChkOutput(IWebDriver driver, String result)
    {
        IWebElement outputBox = driver.FindElement(By.CssSelector("div#destText"));
        String text = outputBox.Text;

        if (text.Equals(result))
        {
            Console.WriteLine("Test Result is same");
        }
        else
        {
            Console.WriteLine("Error! Wrong Test Result");
        }
    }


    public static void GoogleSearch(IWebDriver driver, YamlReader read)

    {
        driver.Navigate().GoToUrl(read.Url);
        IWebElement element = driver.FindElement(By.CssSelector("input[name='q']"));
        element.SendKeys(read.SearchKey);
        if (driver.Title.Equals(read.PageTitles[0]["title1"]))
        {
            Console.WriteLine("Title 1 successful-" + driver.Title);
        }
        else
        {
            Console.WriteLine("Error! Title one is wrong");
        }
        element.Submit();

        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(By.LinkText(read.SearchKey)));

    }

    public static void BingTranslator(IWebDriver driver, YamlReader read)
    {
        if (driver.Title.Equals(read.PageTitles[1]["title2"]))
        {
            Console.WriteLine("Title 2 successful-" + driver.Title);
        }
        else
        {
            Console.WriteLine("Error! Title 2 is wrong");
        }
        driver.FindElement(By.LinkText("Bing Translator")).Click();
        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(By.XPath("//*[contains(@class,'srcTextarea')]")));
    }

    public static void InputText(IWebDriver driver, YamlReader read)
    {
        if (driver.Title.Equals(read.PageTitles[2]["title3"]))
        {
            Console.WriteLine("Title 3 successful-" + driver.Title);
        }
        else
        {
            Console.WriteLine("Error! Title 3 is wrong");
        }
        String s = read.TestData;
        driver.FindElement(By.XPath("//*[contains(@class,'srcTextarea')]")).SendKeys(s);
        ChkInput(driver, s);
    }

    public static void SelectLanguage(IWebDriver driver, YamlReader read)

    {
        driver.FindElement(By.CssSelector("div.col.translationContainer.destinationText div.LS_Header")).Click();
    driver.FindElement(By.CssSelector("div.col.translationContainer.destinationText td[value='"+read.Language+"'] ")).Click();
    Thread.Sleep(1000);
        ChkOutput(driver, read.TestResult);
}


    public static YamlReader Read()    {

        var input = File.OpenText("C:/Users/samkitjain/Documents/Visual Studio 2017/Projects/ConsoleApp2/ConsoleApp2/Yaml.yml");
        var deserializerBuilder = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention());
        var deserializer = deserializerBuilder.Build();

      YamlReader result =  deserializer.Deserialize<YamlReader>(input);
        return result; }

    public static void Main() {

        YamlReader read = Read();
	     
	    IWebDriver d = chromeDriver();

        GoogleSearch(d, read);

        BingTranslator(d, read);

        InputText(d, read);

        SelectLanguage(d, read);
    	     
    
    	
	}

}
