using OpenQA.Selenium;
using TestRunners.xUnit.Pages;
using Xunit;

namespace TestRunners.xUnit;

public class WebsiteTests : TestBase, IDisposable
{
    [Fact]
    [Trait("Category", "Navigation")]
    public void TestCase1_VerifyNavigationToAboutEHUPage()
    {
        var configuration = CreateTestConfiguration();
        var homePage = new HomePage(configuration.Driver, configuration.Wait);
        var aboutPage = homePage.NavigateToAbout();

        Assert.Equal("https://en.ehu.lt/about/", configuration.Driver.Url);
        Assert.Equal("About", configuration.Driver.Title);
        Assert.Contains("About", aboutPage.GetHeaderText());
    }

    [Fact]
    [Trait("Category", "Search")]
    public void TestCase2_VerifySearchFunctionality()
    {
        var configuration = CreateTestConfiguration();
        var homePage = new HomePage(configuration.Driver, configuration.Wait);
        var searchResultsPage = homePage.Search("study programs");

        Assert.Contains("/?s=study+programs", configuration.Driver.Url);
        searchResultsPage.GetSearchResults();
    }
    
    public static TheoryData<string, string> Languages => new()
    {
        { "ru", "Европейский" },
        { "en", "European" },
        { "lt", "Europos" }
    };

    [Theory]
    [Trait("Category", "Language")]
    [MemberData(nameof(Languages))]
    public void TestCase3_VerifyLanguageChangeFunctionality(string lang, string textSample)
    {
        var configuration = CreateTestConfiguration();
        var homePage = new HomePage(configuration.Driver, configuration.Wait);
        homePage.SwitchLanguage(lang);

        Assert.StartsWith($"https://{lang}.ehu.lt/", configuration.Driver.Url);
        Assert.Contains(textSample, configuration.Driver.PageSource);
    }

    [Fact]
    [Trait("Category", "Contact")]
    public void TestCase4_VerifyContactInformationIsVisible()
    {
        var configuration = CreateTestConfiguration();
        var contactPage = new ContactPage(configuration.Driver, configuration.Wait);

        var emailElement = contactPage.GetEmailElement();
        Assert.True(emailElement.Displayed, "Email is not visible on the page.");
        Assert.Contains("franciskscarynacr@gmail.com", emailElement.Text);

        var phoneLtElement = contactPage.GetPhoneLtElement();
        Assert.True(phoneLtElement.Displayed, "Phone (LT) is not visible on the page.");
        Assert.Contains("+370 68 771365", phoneLtElement.Text.Trim());

        var phoneByElement = contactPage.GetPhoneByElement();
        Assert.True(phoneByElement.Displayed, "Phone (BY) is not visible on the page.");
        Assert.Contains("+375 29 5781488", phoneByElement.Text.Trim());

        var facebookLink = contactPage.GetFacebookLink();
        Assert.True(facebookLink.Displayed, "Facebook link is not visible on the page.");

        var telegramLink = contactPage.GetTelegramLink();
        Assert.True(telegramLink.Displayed, "Telegram link is not visible on the page.");

        var vkLink = contactPage.GetVkLink();
        Assert.True(vkLink.Displayed, "VK link is not visible on the page.");
    }
}
