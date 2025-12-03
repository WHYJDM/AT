using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.NUnit.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;
    protected readonly IWebElement PageRoot;

    protected BasePage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
        PageRoot = Wait.Until(d => d.FindElement(By.TagName("body")));
    }

    protected IWebElement WaitForElement(By locator)
    {
        return Wait.Until(d => d.FindElement(locator));
    }

    protected IList<IWebElement> WaitForElements(By locator)
    {
        return Wait.Until(d => d.FindElements(locator).ToList());
    }
} 