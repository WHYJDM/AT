using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.Reqnroll.Pages;

public class HomePage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public HomePage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public void NavigateToHome()
    {
        _driver.Navigate().GoToUrl("https://en.ehu.lt/");
    }

    public AboutPage NavigateToAbout()
    {
        var aboutLink = _wait.Until(d => d.FindElement(By.LinkText("About")));
        aboutLink.Click();
        return new AboutPage(_driver, _wait);
    }

    public SearchResultsPage Search(string query)
    {
        var searchInput = _wait.Until(d => d.FindElement(By.Name("s")));
        searchInput.SendKeys(query);
        searchInput.Submit();
        return new SearchResultsPage(_driver, _wait);
    }

    public void SwitchLanguage(string languageCode)
    {
        var languageLink = _wait.Until(d => d.FindElement(By.LinkText(languageCode.ToUpper())));
        languageLink.Click();
    }

    public ContactPage NavigateToContact()
    {
        var contactLink = _wait.Until(d => d.FindElement(By.LinkText("Contact")));
        contactLink.Click();
        return new ContactPage(_driver, _wait);
    }
} 