using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.Reqnroll.Pages;

public class ContactPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public ContactPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public IWebElement GetEmailElement()
    {
        return _wait.Until(d => d.FindElement(By.CssSelector("a[href^='mailto:']")));
    }

    public IWebElement GetPhoneLtElement()
    {
        return _wait.Until(d => d.FindElement(By.XPath("//*[contains(text(), '+370')]")));
    }

    public IWebElement GetPhoneByElement()
    {
        return _wait.Until(d => d.FindElement(By.XPath("//*[contains(text(), '+375')]")));
    }

    public IWebElement GetFacebookLink()
    {
        return _wait.Until(d => d.FindElement(By.CssSelector("a[href*='facebook.com']")));
    }

    public IWebElement GetTelegramLink()
    {
        return _wait.Until(d => d.FindElement(By.CssSelector("a[href*='t.me']")));
    }

    public IWebElement GetVkLink()
    {
        return _wait.Until(d => d.FindElement(By.CssSelector("a[href*='vk.com']")));
    }
} 