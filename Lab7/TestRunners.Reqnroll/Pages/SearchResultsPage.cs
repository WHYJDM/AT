using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.Reqnroll.Pages;

public class SearchResultsPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public SearchResultsPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public IReadOnlyList<IWebElement> GetSearchResults()
    {
        return _wait.Until(d => d.FindElements(By.CssSelector("article")));
    }
} 