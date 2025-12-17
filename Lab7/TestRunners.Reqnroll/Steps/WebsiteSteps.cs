using FluentAssertions;
using OpenQA.Selenium;
using Reqnroll;
using TestRunners.Reqnroll.Pages;

namespace TestRunners.Reqnroll.Steps;

[Binding]
public class WebsiteSteps : TestBase
{
    private HomePage? _homePage;
    private AboutPage? _aboutPage;
    private SearchResultsPage? _searchResultsPage;
    private ContactPage? _contactPage;
    private readonly ScenarioContext _scenarioContext;

    public WebsiteSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"I am on the EHU website homepage")]
    public void GivenIAmOnTheEHUWebsiteHomepage()
    {
        _homePage = new HomePage(Driver, Wait);
        _homePage.NavigateToHome();
    }

    [When(@"I click on the About link")]
    public void WhenIClickOnTheAboutLink()
    {
        _aboutPage = _homePage!.NavigateToAbout();
    }

    [Then(@"I should be redirected to the About page")]
    public void ThenIShouldBeRedirectedToTheAboutPage()
    {
        Driver.Url.Should().Be("https://en.ehu.lt/about/");
    }

    [Then(@"the page title should be ""(.*)""")]
    public void ThenThePageTitleShouldBe(string title)
    {
        Driver.Title.Should().Be(title);
    }

    [Then(@"the page header should contain ""(.*)""")]
    public void ThenThePageHeaderShouldContain(string headerText)
    {
        _aboutPage!.GetHeaderText().Should().Contain(headerText);
    }

    [When(@"I search for ""(.*)""")]
    public void WhenISearchFor(string searchQuery)
    {
        _searchResultsPage = _homePage!.Search(searchQuery);
    }

    [Then(@"the URL should contain the search query")]
    public void ThenTheURLShouldContainTheSearchQuery()
    {
        Driver.Url.Should().Contain("/?s=study+programs");
    }

    [Then(@"search results should be displayed")]
    public void ThenSearchResultsShouldBeDisplayed()
    {
        _searchResultsPage!.GetSearchResults().Should().NotBeEmpty();
    }

    [When(@"I switch the language to ""(.*)""")]
    public void WhenISwitchTheLanguageTo(string languageCode)
    {
        _scenarioContext["language"] = languageCode;
        _homePage!.SwitchLanguage(languageCode);
    }

    [Then(@"the URL should start with the correct language code")]
    public void ThenTheURLShouldStartWithTheCorrectLanguageCode()
    {
        var languageCode = _scenarioContext["language"].ToString();
        Driver.Url.Should().StartWith($"https://{languageCode}.ehu.lt/");
    }

    [Then(@"the page should contain text in the selected language")]
    public void ThenThePageShouldContainTextInTheSelectedLanguage()
    {
        var expectedText = _scenarioContext["text_sample"].ToString();
        Driver.PageSource.Should().Contain(expectedText);
    }

    [When(@"I navigate to the Contact page")]
    public void WhenINavigateToTheContactPage()
    {
        _contactPage = _homePage!.NavigateToContact();
    }

    [Then(@"the email address should be visible")]
    public void ThenTheEmailAddressShouldBeVisible()
    {
        var emailElement = _contactPage!.GetEmailElement();
        emailElement.Displayed.Should().BeTrue();
        emailElement.Text.Should().Contain("franciskscarynacr@gmail.com");
    }

    [Then(@"the Lithuanian phone number should be visible")]
    public void ThenTheLithuanianPhoneNumberShouldBeVisible()
    {
        var phoneElement = _contactPage!.GetPhoneLtElement();
        phoneElement.Displayed.Should().BeTrue();
        phoneElement.Text.Trim().Should().Contain("+370 68 771365");
    }

    [Then(@"the Belarusian phone number should be visible")]
    public void ThenTheBelarusianPhoneNumberShouldBeVisible()
    {
        var phoneElement = _contactPage!.GetPhoneByElement();
        phoneElement.Displayed.Should().BeTrue();
        phoneElement.Text.Trim().Should().Contain("+375 29 5781488");
    }

    [Then(@"social media links should be visible")]
    public void ThenSocialMediaLinksShouldBeVisible()
    {
        _contactPage!.GetFacebookLink().Displayed.Should().BeTrue();
        _contactPage.GetTelegramLink().Displayed.Should().BeTrue();
        _contactPage.GetVkLink().Displayed.Should().BeTrue();
    }
} 