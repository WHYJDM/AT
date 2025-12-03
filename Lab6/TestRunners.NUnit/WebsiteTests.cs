using NUnit.Framework;
using OpenQA.Selenium;
using TestRunners.NUnit.Pages;
using FluentAssertions;

namespace TestRunners.NUnit;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class WebsiteTests : TestBase
{
    [Test]
    [Category("Navigation")]
    public void TestCase1_VerifyNavigationToAboutEHUPage()
    {
        Logger.Information("Starting TestCase1: Verify Navigation To About EHU Page");
        var configuration = CreateTestConfiguration();
        var homePage = new HomePage(configuration.Driver, configuration.Wait);
        Logger.Debug("Navigating to About page");
        var aboutPage = homePage.NavigateToAbout();

        configuration.Driver.Url.Should().Be("https://en.ehu.lt/about/", "URL should match the About page URL");
        configuration.Driver.Title.Should().Be("About", "Page title should be 'About'");
        aboutPage.GetHeaderText().Should().Contain("About", "Header should contain 'About'");
        Logger.Information("TestCase1 completed successfully");
    }

    [Test]
    [Category("Search")]
    public void TestCase2_VerifySearchFunctionality()
    {
        Logger.Information("Starting TestCase2: Verify Search Functionality");
        var configuration = CreateTestConfiguration();
        var homePage = new HomePage(configuration.Driver, configuration.Wait);
        Logger.Debug("Performing search for 'study programs'");
        var searchResultsPage = homePage.Search("study programs");

        configuration.Driver.Url.Should().Contain("/?s=study+programs", "URL should contain search query");
        searchResultsPage.GetSearchResults().Should().NotBeNull("Search results should be found");
        Logger.Information("TestCase2 completed successfully");
    }
    
    private static List<TestCaseData> _languages =
    [
        new("ru", "Европейский"),
        new("en", "European"),
        new("lt", "Europos")
    ];

    [Test]
    [Category("Language")]
    [TestCaseSource(nameof(_languages))]
    public void TestCase3_VerifyLanguageChangeFunctionality(string lang, string textSample)
    {
        Logger.Information($"Starting TestCase3: Verify Language Change to {lang}");
        var configuration = CreateTestConfiguration();
        var homePage = new HomePage(configuration.Driver, configuration.Wait);
        Logger.Debug($"Switching language to {lang}");
        homePage.SwitchLanguage(lang);

        configuration.Driver.Url.Should().StartWith($"https://{lang}.ehu.lt/", "URL should start with the correct language code");
        configuration.Driver.PageSource.Should().Contain(textSample, "Page should contain text in the selected language");
        Logger.Information($"TestCase3 completed successfully for language {lang}");
    }

    [Test]
    [Category("Contact")]
    public void TestCase4_VerifyContactInformationIsVisible()
    {
        Logger.Information("Starting TestCase4: Verify Contact Information");
        var configuration = CreateTestConfiguration();
        var contactPage = new ContactPage(configuration.Driver, configuration.Wait);

        var emailElement = contactPage.GetEmailElement();
        emailElement.Displayed.Should().BeTrue("Email element should be visible");
        emailElement.Text.Should().Contain("franciskscarynacr@gmail.com", "Email text should match");

        var phoneLtElement = contactPage.GetPhoneLtElement();
        phoneLtElement.Displayed.Should().BeTrue("Phone (LT) element should be visible");
        phoneLtElement.Text.Trim().Should().Contain("+370 68 771365", "Phone (LT) text should match");

        var phoneByElement = contactPage.GetPhoneByElement();
        phoneByElement.Displayed.Should().BeTrue("Phone (BY) element should be visible");
        phoneByElement.Text.Trim().Should().Contain("+375 29 5781488", "Phone (BY) text should match");

        var facebookLink = contactPage.GetFacebookLink();
        facebookLink.Displayed.Should().BeTrue("Facebook link should be visible");

        var telegramLink = contactPage.GetTelegramLink();
        telegramLink.Displayed.Should().BeTrue("Telegram link should be visible");

        var vkLink = contactPage.GetVkLink();
        vkLink.Displayed.Should().BeTrue("VK link should be visible");

        Logger.Information("TestCase4 completed successfully");
    }
}