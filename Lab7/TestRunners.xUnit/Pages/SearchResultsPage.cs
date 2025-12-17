using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.xUnit.Pages;

public class SearchResultsPage : BasePage
{
    private static readonly By SearchResults = By.TagName("article");

    public SearchResultsPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
    {
        Wait.Until(d => d.Url.Contains("/?s="));
    }

    public IList<IWebElement> GetSearchResults()
    {
        return WaitForElements(SearchResults);
    }
} 