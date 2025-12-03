using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.NUnit.Pages;

public class SearchResultsPage : BasePage
{
    private static readonly By SearchResults = By.CssSelector("article");

    public SearchResultsPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
    {
        Wait.Until(d => d.Url.Contains("/?s="));
    }

    public IList<IWebElement> GetSearchResults()
    {
        return WaitForElements(SearchResults);
    }
} 