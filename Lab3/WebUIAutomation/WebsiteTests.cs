using NUnit.Framework;
using OpenQA.Selenium;

namespace WebUIAutomation;

[TestFixture]
public class WebsiteTests : TestBase
{
    [Test]
    public void TestCase1_VerifyNavigationToAboutEHUPage()
    {
        Driver.Navigate().GoToUrl("https://en.ehu.lt/");

        var aboutLink = Wait.Until(d =>
            d.FindElement(By.XPath("//ul[@class='header__menu']//a[normalize-space(text())='About']")));
        aboutLink.Click();

        Wait.Until(d => d.Url == "https://en.ehu.lt/about/");
        Assert.That(Driver.Url, Is.EqualTo("https://en.ehu.lt/about/"), "URL is not 'https://en.ehu.lt/about/'");
        Assert.That(Driver.Title, Is.EqualTo("About"), "Page title is not 'About'.");

        var header = Wait.Until(d => d.FindElement(By.TagName("h1")));
        Assert.That(header.Text, Does.Contain("About"), "Header does not contain 'About'.");
    }

    [Test]
    public void TestCase2_VerifySearchFunctionality()
    {
        Driver.Navigate().GoToUrl("https://en.ehu.lt/");

        var searchNavElement = Wait.Until(d => d.FindElement(By.CssSelector(".header-search")));
        Actions.MoveToElement(searchNavElement).Perform();

        var searchBar = Wait.Until(d => d.FindElement(By.Name("s")));
        searchBar.SendKeys("study programs");
        searchBar.SendKeys(Keys.Enter);

        Wait.Until(d => d.Url.Contains("/?s=study+programs"));
        Assert.That(Driver.Url, Does.Contain("/?s=study+programs"), "URL does not contain 's=study+programs'");

        var searchResults = Wait.Until(d => d.FindElements(By.TagName("article")).ToList());
        Assert.That(searchResults.Count, Is.GreaterThan(0), "No search results were found for 'study programs'.");
    }

    [Test]
    public void TestCase3_VerifyLanguageChangeFunctionality()
    {
        Driver.Navigate().GoToUrl("https://en.ehu.lt/");

        var languageSwitcherNavElement = Wait.Until(d => d.FindElement(By.CssSelector(".language-switcher")));
        Actions.MoveToElement(languageSwitcherNavElement).Perform();

        var language = Wait.Until(d => d.FindElement(By.XPath("//a[normalize-space(text())='lt']")));
        language.Click();

        Wait.Until(d => d.Url.StartsWith("https://lt.ehu.lt/"));
        Assert.That(Driver.Url, Does.StartWith("https://lt.ehu.lt/"), "URL does not start with 'https://lt.ehu.lt/'");
        Assert.That(Driver.PageSource, Does.Contain("Europos"), "Page does not appear to be in Lithuanian.");
    }

    [Test]
    public void TestCase4_VerifyContactInformationIsVisible()
    {
        Driver.Navigate().GoToUrl("https://en.ehu.lt/contact/");

        var emailElement =
            Driver.FindElement(By.XPath("//a[contains(@href, 'mailto:franciskscarynacr@gmail.com')]"));
        Assert.That(emailElement.Displayed, "Email is not visible on the page.");
        Assert.That(emailElement.Text, Does.Contain("franciskscarynacr@gmail.com"), "Email text does not match.");

        var phoneLtElement = Driver.FindElement(By.XPath("//*[text()[contains(.,'+370 68 771365')]]"));
        Assert.That(phoneLtElement.Displayed, "Phone (LT) is not visible on the page.");
        Assert.That(phoneLtElement.Text.Trim(), Does.Contain("+370 68 771365"), "Phone (LT) text does not match.");

        var phoneByElement = Driver.FindElement(By.XPath("//*[text()[contains(.,'+375 29 5781488')]]"));
        Assert.That(phoneByElement.Displayed, "Phone (BY) is not visible on the page.");
        Assert.That(phoneByElement.Text.Trim(), Does.Contain("+375 29 5781488"), "Phone (BY) text does not match.");

        var facebookLink =
            Driver.FindElement(By.XPath("//a[contains(@href, 'https://www.facebook.com/groups/434978221124539/')]"));
        Assert.That(facebookLink.Displayed, "Facebook link is not visible on the page.");

        var telegramLink =
            Driver.FindElement(By.XPath("//a[contains(@href, 'https://t.me/skaryna_cultural_route')]"));
        Assert.That(telegramLink.Displayed, "Telegram link is not visible on the page.");

        var vkLink = Driver.FindElement(By.XPath("//a[contains(@href, 'https://vk.com/public203605228')]"));
        Assert.That(vkLink.Displayed, "VK link is not visible on the page.");
    }
}