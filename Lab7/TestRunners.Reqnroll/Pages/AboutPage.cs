using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.Reqnroll.Pages;

public class AboutPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public AboutPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public string GetHeaderText()
    {
        var header = _wait.Until(d => d.FindElement(By.TagName("h1")));
        return header.Text;
    }
} 