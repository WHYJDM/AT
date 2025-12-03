using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.NUnit.Pages;

public class AboutPage : BasePage
{
    private static readonly By Header = By.TagName("h1");

    public AboutPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
    {
        Wait.Until(d => d.Url == "https://en.ehu.lt/about/");
    }

    public string GetHeaderText()
    {
        return WaitForElement(Header).Text;
    }
} 