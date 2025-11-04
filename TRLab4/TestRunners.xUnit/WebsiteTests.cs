using OpenQA.Selenium;
using Xunit;

namespace TestRunners.xUnit;

[Collection(nameof(WebsiteTests))]
public class WebsiteTests : TestBase
{
    [Fact]
    public void TestCase1_VerifyNavigationToAboutEHUPage()
    {
        var tools = CreateAutomationTools();

        tools.Driver.Navigate().GoToUrl("https://en.ehu.lt/");

        var aboutLink = tools.Wait.Until(d =>
            d.FindElement(By.XPath("//ul[@class='header__menu']//a[normalize-space(text())='About']")));
        aboutLink.Click();

        tools.Wait.Until(d => d.Url == "https://en.ehu.lt/about/");
        Assert.Equal("https://en.ehu.lt/about/", tools.Driver.Url);
        Assert.Equal("About", tools.Driver.Title);

        var header = tools.Wait.Until(d => d.FindElement(By.TagName("h1")));
        Assert.Contains("About", header.Text);
    }

    [Fact]
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
        Assert.Contains("/?s=study+programs", tools.Driver.Url);

        var searchResults = tools.Wait.Until(d => d.FindElements(By.TagName("article")).ToList());
        Assert.True(searchResults.Count > 0, "No search results were found for 'study programs'.");
    }

    [Theory]
    [InlineData("ru", "Европейский")]
    [InlineData("en", "European")]
    [InlineData("lt", "Europos")]
    public void TestCase3_VerifyLanguageChangeFunctionality(string lang, string textSample)
    {
        var tools = CreateAutomationTools();

        tools.Driver.Navigate().GoToUrl("https://en.ehu.lt/");

        var languageSwitcherNavElement = tools.Wait.Until(d => d.FindElement(By.CssSelector(".language-switcher")));
        tools.Actions.MoveToElement(languageSwitcherNavElement).Perform();

        var language = tools.Wait.Until(d => d.FindElement(By.XPath($"//a[normalize-space(text())='{lang}']")));
        language.Click();

        tools.Wait.Until(d => d.Url.StartsWith($"https://{lang}.ehu.lt/"));
        Assert.StartsWith($"https://{lang}.ehu.lt/", tools.Driver.Url);
        Assert.Contains(textSample, tools.Driver.PageSource);
    }

    [Fact]
    public void TestCase4_VerifyContactInformationIsVisible()
    {
        var tools = CreateAutomationTools();

        tools.Driver.Navigate().GoToUrl("https://en.ehu.lt/contact/");

        var emailElement =
            tools.Driver.FindElement(By.XPath("//a[contains(@href, 'mailto:franciskscarynacr@gmail.com')]"));
        Assert.True(emailElement.Displayed, "Email is not visible on the page.");
        Assert.Contains("franciskscarynacr@gmail.com", emailElement.Text);

        var phoneLtElement = tools.Driver.FindElement(By.XPath("//*[text()[contains(.,'+370 68 771365')]]"));
        Assert.True(phoneLtElement.Displayed, "Phone (LT) is not visible on the page.");
        Assert.Contains("+370 68 771365", phoneLtElement.Text.Trim());

        var phoneByElement = tools.Driver.FindElement(By.XPath("//*[text()[contains(.,'+375 29 5781488')]]"));
        Assert.True(phoneByElement.Displayed, "Phone (BY) is not visible on the page.");
        Assert.Contains("+375 29 5781488", phoneByElement.Text.Trim());

        var facebookLink =
            tools.Driver.FindElement(By.XPath("//a[contains(@href, 'https://www.facebook.com/groups/434978221124539/')]"));
        Assert.True(facebookLink.Displayed, "Facebook link is not visible on the page.");

        var telegramLink =
            tools.Driver.FindElement(By.XPath("//a[contains(@href, 'https://t.me/skaryna_cultural_route')]"));
        Assert.True(telegramLink.Displayed, "Telegram link is not visible on the page.");

        var vkLink = tools.Driver.FindElement(By.XPath("//a[contains(@href, 'https://vk.com/public203605228')]"));
        Assert.True(vkLink.Displayed, "VK link is not visible on the page.");
    }
}
