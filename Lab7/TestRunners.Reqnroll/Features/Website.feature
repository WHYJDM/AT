Feature: EHU Website Functionality
    As a user of the EHU website
    I want to be able to navigate, search, and access information
    So that I can find the information I need

    @navigation
    Scenario: Navigate to About page
        Given I am on the EHU website homepage
        When I click on the About link
        Then I should be redirected to the About page
        And the page title should be "About"
        And the page header should contain "About"

    @search
    Scenario: Search for study programs
        Given I am on the EHU website homepage
        When I search for "study programs"
        Then the URL should contain the search query
        And search results should be displayed

    @language
    Scenario Outline: Change website language
        Given I am on the EHU website homepage
        When I switch the language to "<language>"
        Then the URL should start with the correct language code
        And the page should contain text in the selected language

        Examples:
            | language | text_sample |
            | ru       | Европейский |
            | en       | European    |
            | lt       | Europos     |

    @contact
    Scenario: Verify contact information
        Given I am on the EHU website homepage
        When I navigate to the Contact page
        Then the email address should be visible
        And the Lithuanian phone number should be visible
        And the Belarusian phone number should be visible
        And social media links should be visible 