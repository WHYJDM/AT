using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.NUnit.Pages;

public class HomePage : BasePage
{
    private static readonly By AboutLink = By.XPath("//ul[@class='header__menu']//a[normalize-space(text())='About']");
    private static readonly By SearchNavElement = By.CssSelector(".header-search");
    private static readonly By SearchBar = By.Name("s");
    private static readonly By LanguageSwitcher = By.CssSelector(".language-switcher");

    public HomePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
    {
        Driver.Navigate().GoToUrl("https://en.ehu.lt/");
    }

    public AboutPage NavigateToAbout()
    {
        WaitForElement(AboutLink).Click();
        return new AboutPage(Driver, Wait);
    }

    public SearchResultsPage Search(string query)
    {
        var searchNav = WaitForElement(SearchNavElement);
        Driver.FindElement(SearchNavElement).Click();
        
        var searchBar = WaitForElement(SearchBar);
        searchBar.SendKeys(query);
        searchBar.SendKeys(Keys.Enter);
        
        return new SearchResultsPage(Driver, Wait);
    }

    public void SwitchLanguage(string languageCode)
    {
        var languageSwitcher = WaitForElement(LanguageSwitcher);
        languageSwitcher.Click();
        
        var languageLink = WaitForElement(By.XPath($"//a[normalize-space(text())='{languageCode}']"));
        languageLink.Click();
    }
} 