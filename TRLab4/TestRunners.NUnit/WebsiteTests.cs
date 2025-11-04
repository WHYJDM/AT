using NUnit.Framework;
using OpenQA.Selenium;

namespace TestRunners.NUnit;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class WebsiteTests : TestBase
{
    [Test]
    [Category("Navigation")]
    public void TestCase1_VerifyNavigationToAboutEHUPage()
    {
        var tools = CreateAutomationTools();
        
        tools.Driver.Navigate().GoToUrl("https://en.ehu.lt/");

        var aboutLink = tools.Wait.Until(d =>
            d.FindElement(By.XPath("//ul[@class='header__menu']//a[normalize-space(text())='About']")));
        aboutLink.Click();

        tools.Wait.Until(d => d.Url == "https://en.ehu.lt/about/");
        Assert.That(tools.Driver.Url, Is.EqualTo("https://en.ehu.lt/about/"), "URL is not 'https://en.ehu.lt/about/'");
        Assert.That(tools.Driver.Title, Is.EqualTo("About"), "Page title is not 'About'.");

        var header = tools.Wait.Until(d => d.FindElement(By.TagName("h1")));
        Assert.That(header.Text, Does.Contain("About"), "Header does not contain 'About'.");
    }

    [Test]
    [Category("Search")]
    public void TestCase2_VerifySearchFunctionality()
    {
        var tools = CreateAutomationTools();

        tools.Driver.Navigate().GoToUrl("https://en.ehu.lt/");

        var searchNavElement = tools.Wait.Until(d => d.FindElement(By.CssSelector(".header-search")));
        tools.Actions.MoveToElement(searchNavElement).Perform();

        var searchBar = tools.Wait.Until(d => d.FindElement(By.Name("s")));
        searchBar.SendKeys("study programs");
        searchBar.SendKeys(Keys.Enter);

        tools.Wait.Until(d => d.Url.Contains("/?s=study+programs"));
        Assert.That(tools.Driver.Url, Does.Contain("/?s=study+programs"), "URL does not contain 's=study+programs'");

        var searchResults = tools.Wait.Until(d => d.FindElements(By.TagName("article")).ToList());
        Assert.That(searchResults.Count, Is.GreaterThan(0), "No search results were found for 'study programs'.");
    }
    
    private static List<TestCaseData> _languages =
    [
        new TestCaseData("ru", "Европейский"),
        new TestCaseData("en", "European"),
        new TestCaseData("lt", "Europos")
    ];

    [Test]
    [Category("Language")]
    [TestCaseSource(nameof(_languages))]
    public void TestCase3_VerifyLanguageChangeFunctionality(string lang, string textSample)
    {
        var tools = CreateAutomationTools();

        tools.Driver.Navigate().GoToUrl("https://en.ehu.lt/");

        var languageSwitcherNavElement = tools.Wait.Until(d => d.FindElement(By.CssSelector(".language-switcher")));
        tools.Actions.MoveToElement(languageSwitcherNavElement).Perform();

        var language = tools.Wait.Until(d => d.FindElement(By.XPath($"//a[normalize-space(text())='{lang}']")));
        language.Click();

        tools.Wait.Until(d => d.Url.StartsWith($"https://{lang}.ehu.lt/"));
        Assert.That(tools.Driver.Url, Does.StartWith($"https://{lang}.ehu.lt/"), $"URL does not start with 'https://{lang}.ehu.lt/'");
        Assert.That(tools.Driver.PageSource, Does.Contain(textSample), "Page does not appear to be in the selected language.");
    }

    [Test]
    [Category("Contact")]
    public void TestCase4_VerifyContactInformationIsVisible()
    {
        var tools = CreateAutomationTools();

        tools.Driver.Navigate().GoToUrl("https://en.ehu.lt/contact/");

        var emailElement =
            tools.Driver.FindElement(By.XPath("//a[contains(@href, 'mailto:franciskscarynacr@gmail.com')]"));
        Assert.That(emailElement.Displayed, "Email is not visible on the page.");
        Assert.That(emailElement.Text, Does.Contain("franciskscarynacr@gmail.com"), "Email text does not match.");

        var phoneLtElement = tools.Driver.FindElement(By.XPath("//*[text()[contains(.,'+370 68 771365')]]"));
        Assert.That(phoneLtElement.Displayed, "Phone (LT) is not visible on the page.");
        Assert.That(phoneLtElement.Text.Trim(), Does.Contain("+370 68 771365"), "Phone (LT) text does not match.");

        var phoneByElement = tools.Driver.FindElement(By.XPath("//*[text()[contains(.,'+375 29 5781488')]]"));
        Assert.That(phoneByElement.Displayed, "Phone (BY) is not visible on the page.");
        Assert.That(phoneByElement.Text.Trim(), Does.Contain("+375 29 5781488"), "Phone (BY) text does not match.");

        var facebookLink =
            tools.Driver.FindElement(By.XPath("//a[contains(@href, 'https://www.facebook.com/groups/434978221124539/')]"));
        Assert.That(facebookLink.Displayed, "Facebook link is not visible on the page.");

        var telegramLink =
            tools.Driver.FindElement(By.XPath("//a[contains(@href, 'https://t.me/skaryna_cultural_route')]"));
        Assert.That(telegramLink.Displayed, "Telegram link is not visible on the page.");

        var vkLink = tools.Driver.FindElement(By.XPath("//a[contains(@href, 'https://vk.com/public203605228')]"));
        Assert.That(vkLink.Displayed, "VK link is not visible on the page.");
    }
}