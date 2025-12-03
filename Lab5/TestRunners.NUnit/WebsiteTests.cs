using NUnit.Framework;
using OpenQA.Selenium;
using TestRunners.NUnit.Pages;

namespace TestRunners.NUnit;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class WebsiteTests : TestBase
{
    [Test]
    [Category("Navigation")]
    public void TestCase1_VerifyNavigationToAboutEHUPage()
    {
        var configuration = CreateTestConfiguration();
        var homePage = new HomePage(configuration.Driver, configuration.Wait);
        var aboutPage = homePage.NavigateToAbout();

        Assert.That(configuration.Driver.Url, Is.EqualTo("https://en.ehu.lt/about/"), "URL is not 'https://en.ehu.lt/about/'");
        Assert.That(configuration.Driver.Title, Is.EqualTo("About"), "Page title is not 'About'.");
        Assert.That(aboutPage.GetHeaderText(), Does.Contain("About"), "Header does not contain 'About'.");
    }

    [Test]
    [Category("Search")]
    public void TestCase2_VerifySearchFunctionality()
    {
        var configuration = CreateTestConfiguration();
        var homePage = new HomePage(configuration.Driver, configuration.Wait);
        var searchResultsPage = homePage.Search("study programs");

        Assert.That(configuration.Driver.Url, Does.Contain("/?s=study+programs"), "URL does not contain 's=study+programs'");
        Assert.DoesNotThrow(() => searchResultsPage.GetSearchResults(), "No search results were found for 'study programs'.");
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
        var configuration = CreateTestConfiguration();
        var homePage = new HomePage(configuration.Driver, configuration.Wait);
        homePage.SwitchLanguage(lang);

        Assert.That(configuration.Driver.Url, Does.StartWith($"https://{lang}.ehu.lt/"), $"URL does not start with 'https://{lang}.ehu.lt/'");
        Assert.That(configuration.Driver.PageSource, Does.Contain(textSample), "Page does not appear to be in the selected language.");
    }

    [Test]
    [Category("Contact")]
    public void TestCase4_VerifyContactInformationIsVisible()
    {
        var configuration = CreateTestConfiguration();
        var contactPage = new ContactPage(configuration.Driver, configuration.Wait);

        var emailElement = contactPage.GetEmailElement();
        Assert.That(emailElement.Displayed, "Email is not visible on the page.");
        Assert.That(emailElement.Text, Does.Contain("franciskscarynacr@gmail.com"), "Email text does not match.");

        var phoneLtElement = contactPage.GetPhoneLtElement();
        Assert.That(phoneLtElement.Displayed, "Phone (LT) is not visible on the page.");
        Assert.That(phoneLtElement.Text.Trim(), Does.Contain("+370 68 771365"), "Phone (LT) text does not match.");

        var phoneByElement = contactPage.GetPhoneByElement();
        Assert.That(phoneByElement.Displayed, "Phone (BY) is not visible on the page.");
        Assert.That(phoneByElement.Text.Trim(), Does.Contain("+375 29 5781488"), "Phone (BY) text does not match.");

        var facebookLink = contactPage.GetFacebookLink();
        Assert.That(facebookLink.Displayed, "Facebook link is not visible on the page.");

        var telegramLink = contactPage.GetTelegramLink();
        Assert.That(telegramLink.Displayed, "Telegram link is not visible on the page.");

        var vkLink = contactPage.GetVkLink();
        Assert.That(vkLink.Displayed, "VK link is not visible on the page.");
    }
}