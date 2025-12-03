using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.NUnit.Pages;

public class ContactPage : BasePage
{
    private static readonly By EmailLink = By.XPath("//a[contains(@href, 'mailto:franciskscarynacr@gmail.com')]");
    private static readonly By PhoneLtElement = By.XPath("//*[text()[contains(.,'+370 68 771365')]]");
    private static readonly By PhoneByElement = By.XPath("//*[text()[contains(.,'+375 29 5781488')]]");
    private static readonly By FacebookLink = By.XPath("//a[contains(@href, 'https://www.facebook.com/groups/434978221124539/')]");
    private static readonly By TelegramLink = By.XPath("//a[contains(@href, 'https://t.me/skaryna_cultural_route')]");
    private static readonly By VkLink = By.XPath("//a[contains(@href, 'https://vk.com/public203605228')]");

    public ContactPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
    {
        Driver.Navigate().GoToUrl("https://en.ehu.lt/contact/");
    }

    public IWebElement GetEmailElement() => WaitForElement(EmailLink);
    public IWebElement GetPhoneLtElement() => WaitForElement(PhoneLtElement);
    public IWebElement GetPhoneByElement() => WaitForElement(PhoneByElement);
    public IWebElement GetFacebookLink() => WaitForElement(FacebookLink);
    public IWebElement GetTelegramLink() => WaitForElement(TelegramLink);
    public IWebElement GetVkLink() => WaitForElement(VkLink);
} 